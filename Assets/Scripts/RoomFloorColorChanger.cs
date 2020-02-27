using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFloorColorChanger : MonoBehaviour
{
    /*
     * Room Floor Color Changer: Simply makes an object a random color at spawn
     */
    void Start()
    {
        Renderer rend = GetComponent<Renderer>(); //Will render texture
        int rng = Random.Range(0, 7);
        switch (rng)
        {
            case 0:
                rend.material.color = UnityEngine.Color.red;
                break;
            case 1:
                rend.material.color = UnityEngine.Color.yellow;
                break;
            case 2:
                rend.material.color = UnityEngine.Color.green;
                break;
            case 3:
                rend.material.color = UnityEngine.Color.blue;
                break;
            case 4:
                rend.material.color = UnityEngine.Color.cyan;
                break;
            case 5:
                rend.material.color = UnityEngine.Color.magenta;
                break;
            case 6:
                rend.material.color = UnityEngine.Color.black;
                break;
            case 7:
                rend.material.color = UnityEngine.Color.white;
                break;
        }
                
    }
}
