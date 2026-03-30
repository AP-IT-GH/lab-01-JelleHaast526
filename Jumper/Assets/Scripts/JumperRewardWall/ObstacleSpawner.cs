using UnityEngine;
using System.Collections;

public class ObstacleSpawnerWallR : MonoBehaviour
{
    public GameObject Obstacle;
    public GameObject Reward;
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
            if ((GameObject.FindGameObjectWithTag("Obstacle") == null) && (GameObject.FindGameObjectWithTag("Reward") == null))
            {
                if (Random.value > 0.3f)
                {
                    Instantiate(Obstacle, new Vector3(-10, 0.3f, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(Reward, new Vector3(-10, 0.3f, 0), Quaternion.identity);
                }
            }
        }
    }
}