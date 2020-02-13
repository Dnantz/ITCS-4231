using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private GameObject room;
    [SerializeField] private Transform playerTrans;
    private const int roomOffset = 30;

    int gridsize; 

    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        gridsize = 9;
        GameObject[,] floor = generateFloor(gridsize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject[,] generateFloor(int size, int maxRooms = 100)
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

        //create starting room in the center
        rooms[currentLoc[0],currentLoc[1]] = Instantiate(room, new Vector3(0, 0, -roomOffset), Quaternion.identity);
        Debug.Log("Center at " + center[0].ToString() + "," + center[1].ToString());

        //make more rooms
        for (int i=0; i<maxRooms; i++)
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
                        if (currentLoc[0] > 1)
                            currentLoc[0]--;
                        break;
                    case 2:
                        if (currentLoc[1] < size)
                            currentLoc[1]++;
                        break;
                    case 3:
                        if (currentLoc[1] > 1)
                            currentLoc[1]--;
                        break;
                }
                if (++loops >= 100)
                    break;
            } while (rooms[currentLoc[0], currentLoc[1]] != null);
            Debug.Log("making room at" + currentLoc[0].ToString() + "," + currentLoc[1].ToString());
            rooms[currentLoc[0], currentLoc[1]] = Instantiate(room, new Vector3((currentLoc[0] - center[0]) * roomOffset, 0, -roomOffset + (currentLoc[1] - center[1]) * roomOffset), Quaternion.identity);
        }
        return rooms;
    }
}
