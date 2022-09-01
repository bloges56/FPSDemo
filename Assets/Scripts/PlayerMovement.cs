using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variables related to movement
    [Header("Movement")]
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float groundDrag;
    [SerializeField]
    Transform orientation;

    //vars related to jumping
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float jumpCooldown;
    [SerializeField]
    float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    //variables related to ground check
    [Header("GroundCheck")]
    [SerializeField] 
    float playerHeight;
    [SerializeField]
    LayerMask whatIsGround;
    bool grounded;

    //input vars
    float horizontalInput;
    float verticalInput;

    //var to track move direction
    Vector3 moveDirection;
    
    //var to reference rigidbody
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //get rigidBody
        rb = GetComponent<Rigidbody>();

        //freeze rotation
        rb.freezeRotation = true;

        //start ready to jump
        readyToJump = true;

    }

    //method to track player inputs
    private void MyInput()
    {
        //get player inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //check if player hit space
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {  
            Debug.Log("Jump");
            //make player jump
            readyToJump = false;
            Jump();

            //holder enter will continously jump
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    //method to move player
    private void MovePlayer()
    {
        //determine direction to move
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //add force in move direction
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

     private void SpeedControl()
    {
        //get the velocity in x and y
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //check if the velocity magnitude is greater than speed
        if(flatVel.magnitude > moveSpeed)
        {
            //get direction of flat velocity with the magnitude of max speed
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    //method to make player jump
    private void Jump()
    {
        //reset vertical velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //add force in vertical direction
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    //method to reset jump
    private void ResetJump()
    {
        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        if(grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    void FixedUpdate() 
    {
        MovePlayer();
    }
}
