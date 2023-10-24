using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float speed;

    private SpriteRenderer spriteRender;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        spriteRender.sprite = sprites[Random.Range(0,sprites.Length)];
    }

    private void Update()
    {
        speed = AsteroidSpawner.speed;
        transform.position += transform.up * Time.deltaTime * speed;

        if (transform.position.x <= -9f)
        {
            transform.position = new Vector3(8.9f, transform.position.y, transform.position.z);
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
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "GreenBullet")
    //    {
    //        if (size == minSize)
    //        {
    //            GameManager.score+=100;
    //        }
    //        if (size == 0.75f)
    //        {
    //            GameManager.score += 50;
    //        }
    //        if (size == maxSize)
    //        {
    //            GameManager.score += 20;
    //        }

    //        if (size > minSize)
    //        {
    //            currentDirection = transform.up;
    //            //CreateSplit();
    //        }

    //        gameObject.SetActive(false);
    //    }
    //    if (collision.gameObject.tag == "Alien")
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}

    /*private void CreateSplit()
    {        
        Vector2 position = transform.position;

        Asteroid half = Instantiate(this, position, transform.rotation);
        
        Asteroid half2 = Instantiate(this, position, transform.rotation);

        half.size = half2.size = minSize;
        half.speed = half2.speed = Random.Range(minSpeed, maxSpeed);
        half.rigidbody.mass = half2.rigidbody.mass = size;

        direction = Quaternion.Euler(0f, 0f, 135f) * currentDirection;
        half.rigidbody.AddForce(direction * speed * speed);
        
        direction = Quaternion.Euler(0f, 0f, -135f) * currentDirection;
        half2.rigidbody.AddForce(direction * speed * speed);
    }*/
}