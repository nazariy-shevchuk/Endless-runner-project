using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstalcePrefabs;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Spawn obstacles
    void SpawnObstacle()
    {
        // Randomize obstacle
        int index = Random.Range(0, obstalcePrefabs.Length);

        // If game is still active, spawn new object
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstalcePrefabs[index], spawnPos, obstalcePrefabs[index].transform.rotation);
        }
    }
}
