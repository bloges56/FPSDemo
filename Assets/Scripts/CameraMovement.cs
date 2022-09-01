using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //exposed variables for horizontal and vertical sensitivity
    [SerializeField]
    float sensX;
    [SerializeField]
    float sensY;

    //get reference to orientation
    public Transform orientation;

    //variables to track current rotation of camera
    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;

        //set x and y Rotation
        yRotation += mouseX;
        xRotation -= mouseY;

        //prevent rotating past 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate camera and orientation
        transform.rotation = (Quaternion.Euler(xRotation, yRotation, 0f));
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
