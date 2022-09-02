using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    Transform bulletSpawn;

    [SerializeField]
    Bullet bullet;

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

    private void MyInput()
    {
        if(Input.GetKey(shootButton)  && readyToShoot)
        {
            readyToShoot = false;
            Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
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
