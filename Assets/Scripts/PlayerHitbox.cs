using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    GameObject mainCanvas;
    PauseMenuControls pm;
    FloorManager fm;

    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = GameObject.FindGameObjectWithTag("Canvas");
        pm = mainCanvas.GetComponent<PauseMenuControls>();
        fm = mainCanvas.GetComponent<FloorManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("SHOT");
            pm.subtractTime(2);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<RoomCollider>() != null)
        {
            //Debug.Log("Player Notices They Are In A New Room" + collision.gameObject.GetComponent<RoomCollider>().room);
            //fm.setCurrentRoom(collision.gameObject.GetComponent<RoomCollider>().room);
        }
    }
}
