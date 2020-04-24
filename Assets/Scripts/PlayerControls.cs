using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    /*
     * Player Controls: Basic, placeholder controls for moving the player with WASD and Space
     */

    [Header("Defaults to object script is placed on")]
    public Transform player; //What is being moved
    private Vector3 newPosition; //Where it is moving to
    public float xSpeed = 1; //Customizable speed in X direction
    public float ySpeed = 1; //Customizable speed in Y direction
    public float zSpeed = 1; //Customizable speed in Z direction
    void Start()
    {
        if (player == null)
        {
            player = transform; //Default player
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            newPosition = new Vector3(player.position.x, player.position.y, player.position.z + zSpeed); // Z+
            player.position = newPosition;
        }

        if (Input.GetKey(KeyCode.A))
        {
            newPosition = new Vector3(player.position.x - xSpeed, player.position.y, player.position.z); // X-
            player.position = newPosition;
        }

        if (Input.GetKey(KeyCode.S))
        {
            newPosition = new Vector3(player.position.x, player.position.y, player.position.z - zSpeed); // Z-
            player.position = newPosition;
        }

        if (Input.GetKey(KeyCode.D))
        {
            newPosition = new Vector3(player.position.x + xSpeed, player.position.y, player.position.z); // X+
            player.position = newPosition;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            newPosition = new Vector3(player.position.x, player.position.y + ySpeed, player.position.z); // Y+
            player.position = newPosition;
        }
    }

}