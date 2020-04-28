using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCollider : MonoBehaviour
{
    /*
     * Currently placed in the floor, checks which room the player is in
     */

    public GameObject room; //Passed in from RoomIdentifier
    public GameObject mainCanvas;
    public FloorManager fm;
    public GameObject currentRoom;

    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = GameObject.FindGameObjectWithTag("Canvas");
        fm = mainCanvas.GetComponent<FloorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        currentRoom = fm.currentRoom;

        if (other.gameObject.tag == "Player" && currentRoom != room)
        {
            Debug.Log("Entered New Room" + room);
            fm.setCurrentRoom(room);
        }
    }
}
