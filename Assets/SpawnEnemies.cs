using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    Transform spawn;

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    float spawnTime;

    float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Instantiate(enemy, spawn.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > spawnTime)
        {
            GameObject.Instantiate(enemy, spawn.position, Quaternion.identity);
            elapsedTime = 0;
        }
    }
}
