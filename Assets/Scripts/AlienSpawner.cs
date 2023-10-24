using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public Alien alienPrefab;
    public float timeToSpawn;

    public void Awake()
    {
        StartToStart();
    }

    public void StartToStart()
    {
        StartCoroutine(TimeToSpawn());
    }

    private void SpawnAlien()
    {
        Alien alien = Instantiate(alienPrefab);
    }

    public IEnumerator TimeToSpawn()
    {
        for (timeToSpawn = Random.Range(20f, 40f); timeToSpawn > 0; timeToSpawn -= 1) { yield return new WaitForSeconds(1f); }
        SpawnAlien();
        
    }
}
