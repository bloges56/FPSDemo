using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variables related to movement
    [Header("Movement")]
    [SerializeField]
    float moveSpeed;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    [SerializeField]
    Transform orientation;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //get rigidBody
        rb = GetComponent<Rigidbody>();

        //freeze rotation
        rb.freezeRotation = true;

    }

    //method to track player inputs
    private void MyInput()
    {
        //get player inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    //method to move player
    private void MovePlayer()
    {
        //determine direction to move
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //add force in move direction
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    void FixedUpdate() 
    {
        MovePlayer();
    }
}
