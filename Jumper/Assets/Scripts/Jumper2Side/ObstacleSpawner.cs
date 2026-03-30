using UnityEngine;
using System.Collections;

public class ObstacleSpawner2Side : MonoBehaviour
{
    public GameObject Obstacle;
    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 2f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            if (GameObject.FindGameObjectWithTag("Obstacle") == null)
            {
                bool fromLeft = Random.value < 0.5f;
                Vector3 spawnPos = fromLeft ? new Vector3(-10, 0.3f, 0) : new Vector3(10, 0.3f, 0);
                Quaternion spawnRot = fromLeft ? Quaternion.identity : Quaternion.Euler(0, 180f, 0);
                Instantiate(Obstacle, spawnPos, spawnRot);
            }
        }
    }
}