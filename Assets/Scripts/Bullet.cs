using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    //speed of bullet
    [SerializeField]
    float speed;

    //reference to rigidbody
    Rigidbody rb;

    //reference to orientation
    Transform playerCam;

    //time until bullet is deleted
    [SerializeField]
    float lifeSpan;

    float timeAlive;

    public override void OnNetworkSpawn()
    {
         rb = GetComponent<Rigidbody>();
         playerCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
         rb.AddForce(playerCam.forward * speed, ForceMode.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb.AddForce(playerCam.forward * speed, ForceMode.Impulse);
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
        if(timeAlive > lifeSpan)
        {
            Destroy(gameObject);
        }
    }
}
