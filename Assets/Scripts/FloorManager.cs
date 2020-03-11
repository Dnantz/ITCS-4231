using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    /*
     * Potential way to handle rooms with different number of doors:
     * Create the randomized pathway in a seperate 2D array and use
     * that to determine what number of doors are needed. And that
     * way, the rooms can even hold off from being loaded!
     * 
     * The 2D array can hold an int that increments when it counts
     * surrounding rooms:
     * -1 = No room
     * 0 = Undetermined (for when we first populate the pathway)
     * 1 = One Way
     * 2 = Two Way
     * 3 = Three Way (may need more values to determine rotation)
     * 4 = Four Way
     * 5 = Corner (may need more values to determine rotation)
     */

    [SerializeField] private GameObject room_4Way;
    [SerializeField] private GameObject room_3Way;
    [SerializeField] private GameObject room_2Way;
    [SerializeField] private GameObject room_1Way;
    [SerializeField] private GameObject room_Corner;
    [SerializeField] private Transform playerTrans;
    private const int roomOffset = 50;

    int gridsize;
    int numOfRoomsCreated = 0;
    Vector3 startingLocation;

    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        gridsize = 9;
        startingLocation = new Vector3(playerTrans.position.x - roomOffset / 2, playerTrans.position.y - 2, playerTrans.position.z + roomOffset / 2);
        GameObject[,] floor = generateFloor(gridsize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject[,] generateFloor(int size, int maxRooms = 10)
    {
        GameObject[,] rooms = new GameObject[size + 1, size + 1];
        //start with nothing
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                rooms[i, j] = null;
            }
        }

        int[] currentLoc = {size / 2, size / 2 };
        int[] center = { size / 2, size / 2 };

        //create starting room
        rooms[currentLoc[0],currentLoc[1]] = Instantiate(room_4Way, startingLocation, Quaternion.identity);
        Debug.Log("Center at " + center[0].ToString() + "," + center[1].ToString());
        numOfRoomsCreated++;


        //make more rooms
        for (int i=0; i<maxRooms - 1; i++)
        {
            //move 1 unit in a random direction until we are at an empty spot
            int loops = 0;
            do{
                int rng = Random.Range(0, 3);
                switch (rng)
                {
                    case 0:
                        if(currentLoc[0] < size)
                            currentLoc[0]++;
                        break;
                    case 1:
                        if (currentLoc[0] > 0)
                            currentLoc[0]--;
                        break;
                    case 2:
                        if (currentLoc[1] < size)
                            currentLoc[1]++;
                        break;
                    case 3:
                        if (currentLoc[1] > 0)
                            currentLoc[1]--;
                        break;
                }
                if (++loops >= 100)
                    break;
            } while (rooms[currentLoc[0], currentLoc[1]] != null);
            Debug.Log("making room at " + currentLoc[0].ToString() + "," + currentLoc[1].ToString());
            if (rooms[currentLoc[0], currentLoc[1]] == null) {
                rooms[currentLoc[0], currentLoc[1]] = Instantiate(room_4Way, new Vector3((currentLoc[0] - center[0]) * roomOffset + startingLocation.x, startingLocation.y, -roomOffset + startingLocation.z + (currentLoc[1] - center[1]) * roomOffset), Quaternion.identity);
                numOfRoomsCreated++;
            }
            
        }
        Debug.Log("Number of Total Rooms: " + numOfRoomsCreated.ToString());
        return rooms;
    }
}
