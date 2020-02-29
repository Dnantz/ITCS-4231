using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    /*
     * Camera Controller: Has camera follow player and flip to show behind player
     */

    [Header("Defaults to GameObject named 'Player'")]
    public Transform target;
    [Header("Camera offset from Player")]
    public float offsetX = 0f; //Customizable camera distance from player
    public float offsetY = 2f; //Customizable camera distance from player
    public float offsetZ = -4f; //Customizable camera distance from player
    [Header("Camera speed")]
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private Vector3 offset;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private bool isMirror = false;
    private Vector3 mirrorOffset;
    private Quaternion normalRot;
    // Start is called before the first frame update
    void Start()
    {
        normalRot = transform.rotation; //Using starting camera rotation to return to normal after mirror
        offset = new Vector3(offsetX, offsetY, offsetZ); //Will be used to offset camera from character
        mirrorOffset = new Vector3(0f,0f,offsetZ + (offsetZ * -3)); //Offset on top of previous offet, moves the camera to the opposite Z position from the player
        
        if (target == null)
        {
            target = GameObject.Find("Player").transform; //Default target
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; //Offsets camera from the player

        //Moves camera with mouse (Ideal for first-person but not the best for third-person)
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        
        //transform.rotation = normalRot; //Returns camera rotation to normal after mirror
        if (isMirror)
        {
            transform.position += mirrorOffset; //Adds mirror offset on top of normal offset
            
            transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up); //All I know is this makes the camera rotate 180 degrees to face the player from the front
        }
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.M)) //Mirror
        {
            isMirror = true;
        } else
        {
            isMirror = false;
        }

        if (Input.GetKey(KeyCode.R)) //Resets current scenee
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }


}
