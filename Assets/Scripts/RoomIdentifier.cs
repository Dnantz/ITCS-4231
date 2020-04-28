using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomIdentifier : MonoBehaviour
{
    /*
     * This script goes on a room's prefab to pass it's reference down to it's colliders
     */
    Component[] components;
    [SerializeField] GameObject[] enemies;
    GameObject mainCanvas;
    MinimapManager mm;
    FloorManager fm;
    public int enemiesLeft = 0;
    public bool cleared = false;
    public bool currentRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        components = GetComponentsInChildren<RoomCollider>();
        mainCanvas = GameObject.FindGameObjectWithTag("Canvas");
        mm = mainCanvas.GetComponent<MinimapManager>();

        foreach (RoomCollider collider in components)
        {
            collider.room = this.gameObject;
        }

        foreach (GameObject enemy in enemies)
        {
            EnemyManager em = enemy.GetComponent<EnemyManager>();
            em.room = this.gameObject;
            enemy.SetActive(false);
            enemiesLeft++;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!cleared && enemiesLeft > 0)
        {
            enemiesLeft = 0;
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                    enemiesLeft++;
            }
        } else if (!cleared)
        {
            Debug.Log("Room Cleared!");
            mm.updateMap(this.gameObject, "green");
            cleared = true;
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Entered New Room");
        }
    }
    */

    public void spawn()
    {
        currentRoom = true;
        
        Debug.Log("Spawning Enemies");
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            enemy.SetActive(true);
        }
    }

    public void despawn()
    {
        currentRoom = false;
        Debug.Log("Despawning Enemies");
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            enemy.SetActive(false);
        }
    }
}
