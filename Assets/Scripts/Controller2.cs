using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2 : MonoBehaviour
{
    public AudioSource shootSound;
    public AudioSource accelerationSound;

    public float t;

    public float speed = 1f;
    public bool move = false;
    public float rotateSpeed = 0.06f;
    
    Quaternion rotation;

    Vector3 direction;

    public Bullet bulletPrefab;

    public float offset = 90f;

    private void Update()
    {
        float movement = Input.GetAxis("Vertical");

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        rotation = Quaternion.Euler(0f, 0f, rotateZ - offset);//где находится мышь
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);

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

        transform.position = Vector3.MoveTowards(transform.position, direction, speed);

        if (Input.GetKey(KeyCode.W)) move = true; else move = false;
        if (move)
        {
            direction += transform.up * speed * Time.deltaTime;
        }//transform.position += direction * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,direction * speed * Time.deltaTime, speed*Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.W))
            accelerationSound.Play();
        if (Input.GetKeyUp(KeyCode.W))
            accelerationSound.Stop();


    }
}