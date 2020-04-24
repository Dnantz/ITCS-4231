﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenuControls: MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject timeDisplayObj;
    TextMeshProUGUI timeDisplay;
    bool paused = false;
    float timeLeft = 100.0f;

    // Start is called before the first frame update
    void Start()
    {

        if (timeDisplayObj != null)
        {
            timeDisplay = timeDisplayObj.GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Place-holder timer countdown
        timeLeft -= Time.deltaTime;
        if (timeDisplay != null && timeLeft >= 0f)
        {
            timeDisplay.text = "Time Left: " + timeLeft.ToString("0");
        }

        if (timeLeft < 0)
        {
            Debug.Log("TIME'S UP");
            SceneManager.LoadScene("EndScene");
        }

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pause Game");
            paused = !paused;
        }

            pauseMenu.SetActive(paused);
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
        SceneManager.LoadScene("TitleScene");
    }

    /*
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Time Left: " + timeLeft);
    }
    */
}