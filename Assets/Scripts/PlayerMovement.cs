using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    //variables related to movement
    [Header("Movement")]
    [SerializeField]
    float groundSpeed;
    float moveSpeed;
    [SerializeField]
    float wallRunSpeed;
    [SerializeField]
    float groundDrag;
    
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

    WallRunning wr;
    public bool wallrunning;
    //input vars
    float horizontalInput;
    float verticalInput;

    //var to track move direction
    Vector3 moveDirection;
    
    //var to reference rigidbody
    Rigidbody rb;

    [SerializeField]
    GameObject camPrefab;
    public override void OnNetworkSpawn()
    {
        if(!IsOwner) 
        {
            this.enabled = false;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject camera  = Instantiate(camPrefab, new Vector3(0,0,0), Quaternion.identity);
        camera.GetComponent<Camera>().GetComponent<CameraMovement>().player = transform.GetChild(0).transform;
        camera.GetComponent<Camera>().GetComponent<CameraMovement>().orientation = transform.GetChild(1).transform;
        camera.GetComponent<Camera>().GetComponent<MoveCamera>().cameraPosition = transform.GetChild(2).transform;
        transform.gameObject.GetComponent<Shoot>().bulletSpawn = camera.GetComponent<Camera>().transform.GetChild(0).transform;
        
        if(IsServer)
        {   
            camera.GetComponent<NetworkObject>().Spawn();
        }

       //get rigidBody
        rb = GetComponent<Rigidbody>();

        //freeze rotation
        rb.freezeRotation = true;

        //start ready to jump
        readyToJump = true;
        wr = GetComponent<WallRunning>();

        orientation = transform.GetChild(1).transform;

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
        if(wallrunning)
        {
            moveDirection = orientation.forward * verticalInput;
        }
        else
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        }
        
        
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

        if(!wallrunning)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            Vector3 wallNormal = wr.wallRight ? wr.rightWallhit.normal : wr.leftWallhit.normal;
            rb.AddForce((transform.up - wallNormal).normalized * jumpForce, ForceMode.Impulse);
        }
        //add force in vertical direction
        
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
        if(wallrunning)
        {
            moveSpeed = wallRunSpeed;
        }
        else
        {
            moveSpeed = groundSpeed;
            rb.useGravity = true;
        }
    }

    void FixedUpdate() 
    {
        MovePlayer();
    }
}
