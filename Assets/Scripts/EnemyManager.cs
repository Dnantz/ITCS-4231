using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vShooter;

public class EnemyManager : MonoBehaviour
{
    GameObject player;
    PauseMenuControls pm;
    FloorManager fm;
    GameObject playerRoom;
    vShooterWeapon rifle;
    public GameObject room; //Passed in through RoomIdentifier
    vShooterManager gun;
    Transform ptrans;
    Vector3 aimOffset;
    Vector3 aim;
    float shootCount;
    int shotsTaken;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PauseMenuControls>();
        fm = player.GetComponent<FloorManager>();
        playerRoom = fm.currentRoom;
        ptrans = player.transform;
        gun = GetComponent<vShooterManager>();
        rifle = GetComponentInChildren<vShooterWeapon>();
        aimOffset = new Vector3(0f, 1f, 0f);
        aim = ptrans.position + aimOffset;
        shootCount = Random.Range(5f, 10f);
        //shotsTaken = 0;

        gun.rWeapon = rifle;
        rifle.isInfinityAmmo = true;
        rifle.dontUseReload = true;
    }

    // Update is called once per frame
    void Update()
    {
            transform.LookAt(ptrans);
            aim = ptrans.position + aimOffset;

            if (shootCount <= 0 && !gun.isShooting)
            {
                gun.Shoot(aim);
                //shotsTaken++;
                //Debug.Log("Enemy Shoot #" + shotsTaken);
                shootCount = Random.Range(5f, 10f);
            }
            else
            {
                shootCount -= Time.deltaTime;
                gun.UpdateShotTime();
            }
        
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
        pm.addTime(30);
        Destroy(this.gameObject);

    }

    public void damage()
    {
        Debug.Log("Nice Shot!");
        pm.addTime(1);
    }
}
