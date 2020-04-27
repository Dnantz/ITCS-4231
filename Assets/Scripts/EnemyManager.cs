using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    int health = 100;
    GameObject player;
    PauseMenuControls pm;
    Transform ptrans;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PauseMenuControls>();
        ptrans = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(ptrans);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Enemy Hit");
            health -= 10;
        }
    }
    */

    public void kill()
    {
            Debug.Log("Enemy Killed!");
            pm.addTime(10);
            Destroy(this.gameObject);
    }
}
