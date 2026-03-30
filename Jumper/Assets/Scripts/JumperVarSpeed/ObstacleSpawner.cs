using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Obstacle;
    public float speedObstacle = 2f;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 4f;

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
                Instantiate(Obstacle, new Vector3(-10, 0.3f, 0), Quaternion.identity);
            }
        }
    }
}