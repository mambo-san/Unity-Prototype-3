using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject [] obstaclePrefabs;
    private Vector3 spawnPos = new Vector3 (25, 0, 0);
    private float initialDelay = 5;
    private float spawnRate = 2;
    private PlayerControl playerControlScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnObstacle", initialDelay, spawnRate);
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnObstacle()
    {
        if (playerControlScript.isGameOver() == false)
        {
            int index = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[index], spawnPos, obstaclePrefabs[index].transform.rotation);
        }
    }
}
