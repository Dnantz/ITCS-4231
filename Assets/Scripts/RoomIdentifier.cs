using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomIdentifier : MonoBehaviour
{
    /*
     * This script goes on a room's prefab to pass it's reference down to it's colliders
     */
    Component[] components;

    // Start is called before the first frame update
    void Start()
    {
        components = GetComponentsInChildren<RoomCollider>();

        foreach (RoomCollider collider in components)
        {
            collider.room = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Entered New Room");
        }
    }
}
