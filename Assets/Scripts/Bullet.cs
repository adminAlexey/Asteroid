using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 500f;
    public float lifeTime = 2f;

    public bool active = false;

    private void Update()
    {
        if (!active)
        {
            StartCoroutine(TimeToDie());
            active = true;
        }

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }

    IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
        active = false;
    }
}
