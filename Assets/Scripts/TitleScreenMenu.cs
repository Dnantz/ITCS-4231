using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreenMenu : MonoBehaviour
{
    string sceneName;
    bool tutUp = false;
    [SerializeField] GameObject displayObj;
    [SerializeField] GameObject tutPanel;
    TextMeshProUGUI display;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Equals("TitleScene"))
        {
            tutPanel.SetActive(false);
        }

        if (sceneName.Equals("NextLevelScene"))
        {
            display = displayObj.GetComponent<TextMeshProUGUI>();
            display.text += Globals.getScore() + "\nNext Timescale: " + Time.timeScale;
        }

        if (sceneName.Equals("EndScene"))
        {
            display = displayObj.GetComponent<TextMeshProUGUI>();
            display.text += Globals.getScore() + "\nHighest Timescale: " + Time.timeScale;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tutUp && Input.GetMouseButton(0))
        {
            tutPanel.SetActive(false);
            tutUp = false;
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return))
        {
            if (tutUp)
            {
                tutPanel.SetActive(false);
                tutUp = false;
            }
            else if (sceneName.Equals("TitleScene") || sceneName.Equals("NextLevelScene"))
            {
                startGame();
            }

            else if (sceneName.Equals("EndScene"))
            {
                Globals.resetScore();
                toMain();
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            fullscreenToggle(!Screen.fullScreen);
        }
    }

    public void startGame()
    {
        SceneManager.LoadScene("NDIA_Character_Controller_Scene");
        
    }

    //Called during Fullscreen Toggle
    public void fullscreenToggle(bool toggle)
    {
        Debug.Log("Fullscreen Toggled");
        Screen.fullScreen = toggle;
    }

    public void toMain()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void tutScreen(bool open)
    {
        tutPanel.SetActive(open);
        tutUp = open;
    }
}
