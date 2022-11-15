using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MoveCamera : NetworkBehaviour
{
    public Transform cameraPosition;

    void Start()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        if(!IsOwner)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
