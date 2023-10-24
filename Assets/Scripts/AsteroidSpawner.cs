using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public bool trueOrNot = false;

    public int count = 0;

    public float[] sizeArr = { 0.5f, 1f, 1.5f };

    GameObject asteroid;

    public float trajectoryVariance = 15f;

    public float spawnDistance = 10f;

    public float minSpeed = 2f;
    public float maxSpeed = 3f;
    public static float speed = 1.5f;

    public static float size;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAsteroid();
        }
        //trueOrNot = ObjectPool.trueOrNot;
        if (trueOrNot) { SpawnLevel(); }
    }

    public void SpawnLevel()
    {
        speed = Random.Range(minSpeed, maxSpeed);//указал скорость
        size = sizeArr[2];

        asteroid = ObjectPool.instance.GetPooledObject();//запулил объект   

        asteroid.transform.localScale = Vector3.one * size;//указал размер

        Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 spawnPoint = transform.position + spawnDirection;

        float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        //выбираем позицию и поворот

        if (asteroid != null)
        {
            asteroid.transform.position = spawnPoint;//размещаем
            asteroid.transform.rotation = rotation;//устанавливаем поворт
            asteroid.SetActive(true);//устанавливаю активным

            asteroid.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
        }

        speed = Random.Range(minSpeed, maxSpeed);//указал скорость
        size = sizeArr[2];

        asteroid = ObjectPool.instance.GetPooledObject();//запулил объект   

        asteroid.transform.localScale = Vector3.one * size;//указал размер

        spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
        spawnPoint = transform.position + spawnDirection;

        variance = Random.Range(-trajectoryVariance, trajectoryVariance);
        rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        //выбираем позицию и поворот

        if (asteroid != null)
        {
            asteroid.transform.position = spawnPoint;//размещаем
            asteroid.transform.rotation = rotation;//устанавливаем поворт
            asteroid.SetActive(true);//устанавливаю активным

            asteroid.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
        }

        for (int i = count; i > 0; i--)
        {
            SpawnAsteroid();
        }
        count += 1;
    }

    private void SpawnAsteroid()
    {
        speed = Random.Range(minSpeed, maxSpeed);//указал скорость
        size = sizeArr[Random.Range(0, 3)];

        asteroid = ObjectPool.instance.GetPooledObject();//запулил объект   

        asteroid.transform.localScale = Vector3.one * size;//указал размер

        Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 spawnPoint = transform.position + spawnDirection;

        float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        //выбираем позицию и поворот

        if (asteroid != null)
        {
            asteroid.transform.position = spawnPoint;//размещаем
            asteroid.transform.rotation = rotation;//устанавливаем поворт
            asteroid.SetActive(true);//устанавливаю активным

            asteroid.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
        }
    }
}
