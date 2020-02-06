using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    /*
     * Room Manager: Bookkeeping for every room spawned into the game
     */

    public int maxRooms; //How many rooms will be rendered at any given time
    public List<Object> roomList = new List<Object>(); //List of all rooms that exist
    private Object oldRoom; //Room that should be de-rendered

    public void addRoom(Object newRoom)
    {
        /*
         * This part adds all rooms to a basic List, and destroys the old rooms once the list gets too long.
         * It ideally should keep it in memory, but for now it doesn't. 
         * A new method of tracking how many rooms are in memory should be implemented eventually.
         */

        roomList.Add(newRoom); //Adds room to the end of the list
        Debug.Log("ROOM #" + roomList.IndexOf(newRoom));
        if (roomList.IndexOf(newRoom) > maxRooms - 1) //If there are now too many rooms loaded
        {
            oldRoom = roomList[0]; //Oldest room in the list is at position 0
            roomList.Remove(roomList[0]); //All rooms automatically shift over
            Object.Destroy(oldRoom); //Clear room from memory
        }
    }
}
