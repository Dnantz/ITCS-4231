using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vShooter;

public class EnemyManager : MonoBehaviour
{
    GameObject player;
    GameObject mainCanvas;
    PauseMenuControls pm;
    FloorManager fm;
    GameObject playerRoom;
    [SerializeField] GameObject physicalRifle;
    [SerializeField] GameObject physicalShotgun;
    vShooterWeapon rifle;
    public GameObject room; //Passed in through RoomIdentifier
    vShooterManager gun;
    [SerializeField] GameObject rifleModel;
    [SerializeField] GameObject shotgunModel;
    Transform ptrans;
    Vector3 aimOffset;
    Vector3 aim;
    float shootCount;
    int shotsTaken;
    float randomWepNum;
    bool isRifle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCanvas = GameObject.FindGameObjectWithTag("Canvas");
        pm = mainCanvas.GetComponent<PauseMenuControls>();
        fm = mainCanvas.GetComponent<FloorManager>();
        playerRoom = fm.currentRoom;
        ptrans = player.transform;
        gun = GetComponent<vShooterManager>();
        aimOffset = new Vector3(0f, 1f, 0f);
        aim = ptrans.position + aimOffset;
        shootCount = Random.Range(5f, 10f);
        //shotsTaken = 0;

        

        randomWepNum = Random.Range(0.0f, 100.0f);

        if (randomWepNum >= 75)
        {
            //25% chance enemy has a shotgun
            physicalShotgun.SetActive(true);
            rifle = physicalShotgun.GetComponent<vShooterWeapon>();
            isRifle = false;
        } else
        {
            physicalRifle.SetActive(true);
            rifle = physicalRifle.GetComponent<vShooterWeapon>();
            isRifle = true;
        }

        gun.rWeapon = rifle;
        rifle.isInfinityAmmo = true;
        rifle.dontUseReload = true;
    }

    // Update is called once per frame
    void Update()
    {
            transform.LookAt(ptrans);
            aim = ptrans.position + aimOffset;

        if (!pm.paused)
        {

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
        if (isRifle)
        {
            Instantiate(rifleModel, this.gameObject.transform.position, this.gameObject.transform.rotation);
        } else
        {
            Instantiate(shotgunModel, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }
        Destroy(this.gameObject);

    }

    public void damage()
    {
        Debug.Log("Nice Shot!");
        pm.addTime(1);
    }
}
