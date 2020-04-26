using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    /*
     * rooms will be assigned a number based on the rooms surroinding them.
     * The final room number will be a composite of the numbers assigned from each entrance
     * 
     * S = 1
     * E = 2
     * N = 4
     * W = 8
     * 
     * So if there are entrances to the north and east, it will be room type 4 + 2 = 6
     * this is handled by a corner room rotated appropriately
     */

    [SerializeField] private GameObject room_4Way;
    [SerializeField] private GameObject room_3Way;
    [SerializeField] private GameObject room_2Way;
    [SerializeField] private GameObject room_1Way;
    [SerializeField] private GameObject room_Corner;
    [SerializeField] private Transform playerTrans;
    private const int roomOffset = 50; //total side length of a room

    public GameObject[,] floor;
    public GameObject currentRoom;
    public GameObject oldCurrentRoom;
    public GameObject mainCanvas;
    public MinimapManager mmManager;

    int gridsize;
    int numOfRoomsCreated = 0;
    Vector3 startingLocation;

    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        gridsize = 9;
        startingLocation = new Vector3(playerTrans.position.x, playerTrans.position.y - 2, playerTrans.position.z);
        floor = generateFloor(gridsize);

        mainCanvas = GameObject.FindGameObjectWithTag("Canvas");
        mmManager = mainCanvas.GetComponent<MinimapManager>();
        mmManager.generate();

        Debug.Log("Floor Manager initialized");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCurrentRoom(GameObject cRoom)
    {
        Debug.Log("Setting Current Room");

        if (cRoom != currentRoom)
        {
            if (oldCurrentRoom != null)
            {
                oldCurrentRoom = currentRoom;
                currentRoom = cRoom;
                mmManager.updateMap(currentRoom, "red");
                mmManager.updateMap(oldCurrentRoom, "white");
            }
            else
            {
                oldCurrentRoom = cRoom;
                currentRoom = cRoom;
                mmManager.updateMap(currentRoom, "red");
            }
        }
    }

    GameObject[,] generateFloor(int size, int maxRooms = 10)
    {
        GameObject[,] rooms = new GameObject[size + 1, size + 1];
        bool[,] rooms_b = new bool[size + 1, size + 1];
        //start with nothing
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                rooms[i, j] = null;
                rooms_b[i,j] = false;
            }
        }

        int[] currentLoc = {size / 2, size / 2 };
        int[] center = { size / 2, size / 2 };

        //create starting room
        rooms_b[currentLoc[0],currentLoc[1]] = true;
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
                        if(currentLoc[0] < size - 1)
                            currentLoc[0]++;
                        break;
                    case 1:
                        if (currentLoc[0] > 1)
                            currentLoc[0]--;
                        break;
                    case 2:
                        if (currentLoc[1] < size - 1)
                            currentLoc[1]++;
                        break;
                    case 3:
                        if (currentLoc[1] > 1)
                            currentLoc[1]--;
                        break;
                }
                if (++loops >= 100)
                    break;
            } while (rooms_b[currentLoc[0], currentLoc[1]]);
            Debug.Log("deciding there is a room at " + currentLoc[0].ToString() + "," + currentLoc[1].ToString());
            if (!rooms_b[currentLoc[0], currentLoc[1]])
            {
                //there should be a room here
                rooms_b[currentLoc[0], currentLoc[1]] = true;
                numOfRoomsCreated++;
            }
        }
        //set room to the proper type
        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int j = 0; j < rooms.GetLength(1); j++)
            {
                //if there should be a room here
                if (rooms_b[i,j])
                {
                    byte roomtype = 0; //dat efficiency

                    //is there a room to the south
                    if (rooms_b[i, j - 1])
                        roomtype += 1;
                    //is there a room to the east
                    if (rooms_b[i + 1, j])
                        roomtype += 2;
                    //is there a room to the north
                    if (rooms_b[i, j + 1])
                        roomtype += 4;
                    //is there a room to the west
                    if (rooms_b[i - 1, j])
                        roomtype += 8;

                    //spawn the rooms
                    switch (roomtype)
                    {
                        case 1: //S
                            rooms[i,j] = Instantiate(room_1Way, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x - 25, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z);
                            Debug.Log("making S room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 2: //E
                            rooms[i, j] = Instantiate(room_1Way, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.Rotate(new Vector3(0, 270, 0));
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z - 25);
                            Debug.Log("making E room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 3: //SE
                            rooms[i, j] = Instantiate(room_Corner, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x - 25, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z);
                            Debug.Log("making SE room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 4: //N
                            rooms[i, j] = Instantiate(room_1Way, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.Rotate(new Vector3(0, 180, 0));
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x + 25, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z);
                            Debug.Log("making N room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 5: //NS
                            rooms[i, j] = Instantiate(room_2Way, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x - 25, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z);
                            Debug.Log("making NS room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 6: //NE
                            rooms[i, j] = Instantiate(room_Corner, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.Rotate(new Vector3(0, 270, 0));
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z - 25);
                            Debug.Log("making NE room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 7: //NSE
                            rooms[i,j] = Instantiate(room_3Way, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.Rotate(new Vector3(0, 270, 0));
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z - 25);
                            Debug.Log("making NSE room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 8: //W
                            rooms[i,j] = Instantiate(room_1Way, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.Rotate(new Vector3(0,90,0));
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z + 25);
                            Debug.Log("making W room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 9: //SW
                            rooms[i,j] = Instantiate(room_Corner, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.Rotate(new Vector3(0, 90, 0));
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z + 25);
                            Debug.Log("making SW room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 10: //EW
                            rooms[i,j] = Instantiate(room_2Way, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.Rotate(new Vector3(0, 90, 0));
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z + 25);
                            Debug.Log("making EW room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 11: //SEW
                            rooms[i,j] = Instantiate(room_3Way, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x - 25, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z);
                            Debug.Log("making SEW room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 12: //NW
                            rooms[i,j] = Instantiate(room_Corner, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.Rotate(new Vector3(0, 180, 0));
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x + 25, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z);
                            Debug.Log("making NW room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 13: //NSW
                            rooms[i,j] = Instantiate(room_3Way, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.Rotate(new Vector3(0, 90, 0));
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z + 25);
                            Debug.Log("making NSW room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 14: //NEW
                            rooms[i,j] = Instantiate(room_3Way, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.Rotate(new Vector3(0, 180, 0));
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x + 25, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z);
                            Debug.Log("making NEW room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        case 15: //NSEW
                            rooms[i,j] = Instantiate(room_4Way, new Vector3((i - center[0]) * roomOffset + startingLocation.x, startingLocation.y, startingLocation.z + (j - center[1]) * roomOffset), Quaternion.identity);
                            rooms[i, j].transform.position = new Vector3(rooms[i, j].transform.position.x - 25, rooms[i, j].transform.position.y, rooms[i, j].transform.position.z);
                            Debug.Log("making NSEW room at " + i.ToString() + "," + j.ToString() + " with coordinates " + rooms[i,j].transform.position.x.ToString() + "," + rooms[i,j].transform.position.z.ToString());
                            break;
                        default:
                            Debug.Log("Invalid rotation: " + roomtype.ToString());
                            break;
                    }
                }
            }
        }
        Debug.Log("Number of Total Rooms: " + numOfRoomsCreated.ToString());
        return rooms;
    }
}
