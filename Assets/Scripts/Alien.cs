using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public AudioSource shootSound;

    public Transform targetLook;

    public GameObject alien;
    GameObject bullet;

    public int side;

    private Rigidbody2D rigidbody;

    public float[] spawnDotX = { -9.5f, 9.5f };
    public float spawnY;
    public float spawnX;

    private Vector3 vectorDifference;
    private float atan2;

    private void Rotation()
    {
        vectorDifference = (targetLook.transform.position - transform.position);
        atan2 = Mathf.Atan2(vectorDifference.y, vectorDifference.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg - 90f);
    }
    private void Awake()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        targetLook = GameObject.Find("Player").GetComponent<Transform>();

        spawnY = Random.Range(-2.5f, 2.5f);
        spawnX = spawnDotX[Random.Range(0, 2)];
        alien.transform.position = new Vector3(8.768f, spawnY, 0);
        side = Random.Range(0, 2);
        if (side == 1) { rigidbody.AddForce(transform.right * 40f); }
        else { rigidbody.AddForce(transform.right * -40f); }
        StartCoroutine(ShootRate());
    }

    void Update()
    {
        Rotation();
        if (transform.position.x <= -9f)
        {
            transform.position = new Vector3(8.99f, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= 9f)
        {
            transform.position = new Vector3(-8.99f, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        { 
            Destroy(gameObject);
            FindObjectOfType<AlienSpawner>().StartToStart();
        }
        if (collision.gameObject.tag == "GreenBullet" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            FindObjectOfType<AlienSpawner>().StartToStart();
            GameManager.score += 200;
        }
    }

    IEnumerator ShootRate()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        shootSound.Play();
        bullet = ObjectPool2.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            bullet.transform.rotation = transform.rotation;
        }

        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 500f);

        StartCoroutine(ShootRate());
    }
}
