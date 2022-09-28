using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //speed of enemy
    [SerializeField]
    float speed;

    //track players position
    Vector3 playerPosition;

    //track player
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.name);
    }

    // Update is called once per frame
    void Update()
    {
        //get player's current position
        playerPosition = player.transform.position;
    }

    void FixedUpdate()
    {
        //move enemy towards player
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPosition.x, transform.position.y, playerPosition.z), speed * Time.deltaTime);
    }
}
