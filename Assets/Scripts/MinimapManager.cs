using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour
{

    /*
     * Draws minimap
     */

    [SerializeField] GameObject player;
    FloorManager fm;
    [SerializeField] Texture2D mapSquare;
    [SerializeField] Canvas mapCanvas;
    float squareSize = 10;
    GameObject[,] floor;
    int floorLength = 10;
    Texture2D emptySquare;
    Texture2D centerSquare;
    Texture2D blankSquare;
    Texture2D[,] mapGrid;
    int mapRStart = -1;
    int mapREnd = -1;
    int mapCStart = -1;
    int mapCEnd = -1; //[r,c]
    int mapR;
    int mapC;

    // Start is called before the first frame update
    void Start()
    {
        fm = player.GetComponent<FloorManager>();
        floor = fm.floor;
        emptySquare = Resources.Load("minimapSquare") as Texture2D;
        centerSquare = Resources.Load("centerSquare") as Texture2D;
        bool boolC = false;
        bool boolR = false;

        Debug.Log("FLOOR ARRAY:");
        //Print floor array
        for (int i=0; i < 10; i++)
        {
            string rowString = null;
            for (int j=0; j < 10; j++)
            {
                if (floor[i,j] != null)
                {
                    rowString += "X ";
                } else
                {
                    rowString += "- ";
                }
            }
            Debug.Log(rowString);
        }

        /*
        //Count how many occupied rows and columns there are
        //Debug.Log("Floor Length: " + 10);

        for (int i=0; i < floorLength; i++)
        {
            for (int j=0; j < floorLength; j++)
            {
                //Debug.Log("floor[" + i + "," + j + "]");
                if (floor[i,j] != null)
                {
                    if (mapRStart < 0)
                    {
                        mapRStart = i;
                    }
                    else
                    {
                        mapREnd = i;
                    }
                }
            }
        }

        for (int j = 0; j < floorLength; j++)
        {
            for (int i = 0; i < floorLength; i++)
            {
                //Debug.Log("floor[" + i + "," + j + "]");
                if (floor[i, j] != null)
                {
                    if (mapCStart < 0)
                    {
                        mapCStart = j;
                    }
                    else
                    {
                        mapCEnd = j;
                    }
                }
            }
        }

        if (mapREnd < 0)
        {
            Debug.Log("R Reaches End");
            mapREnd = floorLength - 1;
        }

        if (mapCEnd < 0)
        {
            Debug.Log("C Reaches End");
            mapCEnd = floorLength - 1;
        }

        mapR = mapREnd - mapRStart;
        mapC = mapCEnd - mapCStart;
        Debug.Log("R Start: " + mapRStart + " R End: " + mapREnd);
        Debug.Log("C Start: " + mapCStart + " C End: " + mapCEnd);
        Debug.Log("R: " + mapR + " C: " + mapC);
        mapGrid = new Texture2D[mapR, mapC];

        for (int i=0; i <= mapR; i++)
        {
            for (int j=0; j <= mapC; j++)
            {
                Debug.Log("floor[" + (i + mapRStart) + "," + (j + mapCStart) + "]");
                if (floor[i+mapRStart,j+mapCStart] != null)
                {
                    Debug.Log("Trying to put in mapGrid[" + i + "," + j + "]");
                    mapGrid[i, j] = emptySquare;
                }
            }
        }

        /*
        //start with nothing
        for (int i = 0; i < floor.Length; i++)
        {
            for (int j = 0; j < floor.Length; j++)
            {
                mapGrid[i, j] = emptySquare;
            }
        }
        */

        mapGrid = new Texture2D[10,10];

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (floor[i,j] != null)
                {
                    Debug.Log("Added minimap square");
                        mapGrid[i, j] = emptySquare;
                }
            }
        }

        Debug.Log("Minimap Done");
    }

    void drawMinimap()
    {
        //GUI.DrawTexture(new Rect(1, 1, squareSize, squareSize), emptySquare);

        //int minimapSquareNum = 0;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (mapGrid[i, j] != null)
                {
                    //minimapSquareNum++;
                    GUI.DrawTexture(new Rect(i * squareSize, j * squareSize, squareSize, squareSize), mapGrid[i, j]);
                    //Change color to black if there is a room
                    //mapGrid[i, j].SetPixel(0, 0, Color.black);
                    //mapGrid[i, j].Apply();

                }

            }
        }

        //Debug.Log(minimapSquareNum);
    }
    
    void OnGUI()
    {
            drawMinimap();
    }

    UnityEngine.Color randomColor()
    {
        int rng = Random.Range(0, 7);
        switch (rng)
        {
            case 0:
                return UnityEngine.Color.red;
            case 1:
                return UnityEngine.Color.yellow;
            case 2:
                return UnityEngine.Color.green;
            case 3:
                return UnityEngine.Color.blue;
            case 4:
                return UnityEngine.Color.cyan;
            case 5:
                return UnityEngine.Color.magenta;
            case 6:
                return UnityEngine.Color.black;
            case 7:
                return UnityEngine.Color.white;
            default:
                return UnityEngine.Color.white;
        }
    }

    public void updateMap(GameObject room, string color)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (floor[i, j] == room)
                {
                    if (color.Equals("red"))
                    {
                        mapGrid[i, j] = centerSquare;
                        Debug.Log("Updated Minimap");
                        break;
                    }

                    if (color.Equals("white"))
                    {
                        mapGrid[i, j] = emptySquare;
                        Debug.Log("Updated Minimap");
                        break;
                    }
                }
            }
        }
    }
}
