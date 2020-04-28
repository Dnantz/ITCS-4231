using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Invector.vCharacterController;

public class PauseMenuControls: MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject timeDisplayObj;
    GameObject player;
    vThirdPersonController v3p;
    TextMeshProUGUI timeDisplay;
    public bool paused = false;
    public float timeLeft = 100.0f;

    // Start is called before the first frame update
    void Start()
    {

        if (timeDisplayObj != null)
        {
            timeDisplay = timeDisplayObj.GetComponent<TextMeshProUGUI>();
        }

        player = GameObject.FindGameObjectWithTag("Player");
        v3p = player.GetComponent<vThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pause Game");
            paused = !paused;
        }

        pauseMenu.SetActive(paused);
        Cursor.visible = paused;
        player.SetActive(!paused);

        //Place-holder timer countdown
        if (!paused)
        {
            timeLeft -= Time.deltaTime;
            
        }

        if (timeDisplay != null && timeLeft >= 0f)
        {
            timeDisplay.text = "Time Left: " + timeLeft.ToString("0");
        }

        if (timeLeft < 0)
        {
            Debug.Log("TIME'S UP");
            SceneManager.LoadScene("EndScene");
            Cursor.visible = true;
        }

        
    }

    //Called during Fullscreen Toggle
    public void fullscreenToggle(bool toggle)
    {
        Debug.Log("Fullscreen Toggled");
        Screen.fullScreen = toggle;
    }

    //Called on Exit Button Click
    public void exitGame()
    {
        Application.Quit();
    }

    public void toMain()
    {
        Debug.Log("Exiting To Main Menu...");
        SceneManager.LoadScene("TitleScene");
    }

    public void startGame()
    {
        SceneManager.LoadScene("NDIA_Character_Controller_Scene");
    }

    public void subtractTime(int time)
    {
        Debug.Log("Time Lost");
        timeLeft -= time;
    }

    public void addTime(int time)
    {
        Debug.Log("Time Extended");
        timeLeft += time;
    }

    



    /*
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Time Left: " + timeLeft);
    }
    */
}
