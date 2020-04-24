using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLine : MonoBehaviour
{
    /*
     * Starting Line: When a Player/object crosses the starting line, a room/object spawns
     */

    [Header("Defaults to GameObject named 'Player'")]
    [Tooltip("What needs to cross the starting line")]
    public Transform player; //What needs to cross the starting line
    [Header("Defaults to Prefabs/CenteredWalkway")]
    [Tooltip("Path to what needs to be spawned")]
    public string spawnObject; //Path to what needs to be spawned
    private Vector3 spawnPoint; //Where the item spawns
    Renderer rend; //Will render the texture of the starting line
    bool started = false;
    Object newObject; //Holds what was spawned


    void Start()
    {
        spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5); //Item will spawn 5 units behind starting line
        rend = GetComponent<Renderer>(); //Will render texture for starting line
        rend.material.color = UnityEngine.Color.red; //Starts the startling line as being red
        if (player == null)
        {
            player = GameObject.Find("Player").transform; //Default player
        }
        if (spawnObject == null)
        {
            spawnObject = "Prefabs/CenteredWalkway"; //Default spawnObject
        }
    }

    void Update()
    {
        if (player.position.z >= transform.position.z && !started) //If player walks past starting line and it has not been activated yet
        {
            Debug.Log("START");
            started = true;
            rend.material.color = UnityEngine.Color.green; //Makes starting line green
            newObject = Instantiate(Resources.Load("Prefabs/CenteredWalkway"), spawnPoint, Quaternion.identity); //Creates a new object at specified spawnPoint at same rotation as object this script is on
            player.GetComponent<RoomManager>().addRoom(newObject); //Adds the new room/object to player's RoomManager script which keeps track of all the rooms spawned
        }
    }
}
