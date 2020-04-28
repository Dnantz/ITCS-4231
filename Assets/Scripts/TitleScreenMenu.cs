using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreenMenu : MonoBehaviour
{
    string sceneName;
    [SerializeField] GameObject displayObj;
    TextMeshProUGUI display;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        sceneName = SceneManager.GetActiveScene().name;

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
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return))
        {
            if (sceneName.Equals("TitleScene") || sceneName.Equals("NextLevelScene"))
            {
                startGame();
            }

            if (sceneName.Equals("EndScene"))
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
}
