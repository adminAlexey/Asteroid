using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public AudioSource shootSound;
    public AudioSource accelerationSound;

    public float speed = 1.0f;
    public bool move = false;
    public float rotateSpeed = 50f;

    Vector3 direction;

    private bool shooting = true;

    public float moveSpeed = 1f;
    public float turnSpeed = 1f;

    GameObject bullet;

    private void Update()
    {
        if (transform.position.x <= -9f)
        {
            transform.position = new Vector3 (8.9f, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= 9f)
        {
            transform.position = new Vector3(-8.9f, transform.position.y, transform.position.z);
        }
        if (transform.position.y <= -5f)
        {
            transform.position = new Vector3(transform.position.x, 4.9f, transform.position.z);
        }
        if (transform.position.y >= 5f)
        {
            transform.position = new Vector3(transform.position.x, -4.9f, transform.position.z);
        }

        if (Input.GetKey(KeyCode.W)) move = true; else move = false;
        if (move) direction += transform.up * speed * Time.deltaTime;
        transform.position += direction * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) { transform.Rotate(0, 0f, rotateSpeed * Time.deltaTime, 0f); }
        else if (Input.GetKey(KeyCode.D)) { transform.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime, 0f); }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (shooting)
            {
                Shoot();
                StartCoroutine(ShootRate());
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
            accelerationSound.Play();
        if (Input.GetKeyUp(KeyCode.W))
            accelerationSound.Stop();
    }

    private void Shoot()
    {
        shootSound.Play();

        bullet = ObjectPool.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            bullet.transform.rotation = transform.rotation;
        }
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 500f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "RedBullet" || collision.gameObject.tag == "Alien") 
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            //GetComponent<Rigidbody>().angularVelocity = 0;

            gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }

    IEnumerator ShootRate()
    {
        shooting = false;
        yield return new WaitForSeconds(0.333f);
        shooting = true;
    }
}
 