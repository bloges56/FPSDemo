using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Shoot : NetworkBehaviour
{
    public Transform bulletSpawn;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    KeyCode shootButton = KeyCode.Mouse0;

    [SerializeField]
    float fireRate;
    bool readyToShoot;
    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
    }

    public override void OnNetworkSpawn()
    {
        if(!IsOwner)
        {
            this.enabled = false;
        }
    }

    private void MyInput()
    {
        if(Input.GetKey(shootButton)  && readyToShoot)
        {
            readyToShoot = false;
            Instantiate(bullet, bulletSpawn.position, Quaternion.identity).GetComponent<NetworkObject>().Spawn();
            Invoke(nameof(ResetBullet), fireRate);
        }
    }

    private void ResetBullet()
    {
        readyToShoot = true;
    }
    // Update is called once per frame
    void Update()
    {
        MyInput();
    }
}
