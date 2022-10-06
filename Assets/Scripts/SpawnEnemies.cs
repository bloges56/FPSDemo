using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    GameObject spawn;

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    float spawnTime;

    float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        float xPosition = Random.Range(spawn.GetComponent<BoxCollider>().size.x * -0.5f, spawn.GetComponent<BoxCollider>().size.x * 0.5f) + spawn.transform.position.x;
        float zPosition = Random.Range(spawn.GetComponent<BoxCollider>().size.z * -0.5f, spawn.GetComponent<BoxCollider>().size.z * 0.5f) + spawn.transform.position.z;
        GameObject.Instantiate(enemy, new Vector3(xPosition, spawn.transform.position.y, zPosition), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > spawnTime)
        {
            float xPosition = Random.Range(spawn.GetComponent<BoxCollider>().size.x * -0.5f, spawn.GetComponent<BoxCollider>().size.x * 0.5f) + spawn.transform.position.x;
            float zPosition = Random.Range(spawn.GetComponent<BoxCollider>().size.z * -0.5f, spawn.GetComponent<BoxCollider>().size.z * 0.5f) + spawn.transform.position.z;
            GameObject.Instantiate(enemy, new Vector3(xPosition, spawn.transform.position.y, zPosition), Quaternion.identity);
            elapsedTime = 0;
        }
    }
}
