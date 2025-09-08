using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Platforms")]
    public GameObject[] platformPrefabs;

    [SerializeField]
    private float levelWidth = 5f;
    [SerializeField]
    private float x;
    [SerializeField]
    private float miny;
    [SerializeField]
    private float maxy;
    [SerializeField]
    private int spawnedPlatforms = 0;

    private int spawnedkeys = 0;
    private Vector2 spawnPosition;
    private GameObject player;
    


    void Start()
    {
        player = GameObject.Find("Player");
        miny = player.transform.position.y + 1f;
        maxy = miny + 1f;
        x = player.transform.position.x;
        GenerateLevels();
    }

    private void GenerateLevels()
    {
        while (spawnedkeys < 3 || spawnedPlatforms < 20)
        {
            spawnPosition.y = Random.Range(miny, maxy);

            if (x > 0)
            {
                spawnPosition.x = Random.Range(-levelWidth, x - 2f); ;
            }
            else
            {
                spawnPosition.x = Random.Range(x + 3f, levelWidth); ;
            }

            GameObject platform = Instantiate(
                platformPrefabs[Random.Range(0, platformPrefabs.Length)],
                spawnPosition,
                Quaternion.identity);
            
            Transform firstChild = platform.transform.GetChild(0);

            if (firstChild != null && firstChild.name == "Jumping pad")
            {
                miny = spawnPosition.y + 4f;
                maxy = spawnPosition.y + 4.5f;
            }
            else
            {
                miny = spawnPosition.y + 2.5f;
                maxy = spawnPosition.y + 3f;
            }

            x = spawnPosition.x;

            spawnedPlatforms++;

            if (firstChild != null && firstChild.name == "Gold 0")
            {
                spawnedkeys++;
            }
        }

    }
}
