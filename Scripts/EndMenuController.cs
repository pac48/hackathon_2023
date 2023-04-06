using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuController : MonoBehaviour
{
    public TimerSlider timerSlider;
    public GameObject menuPanel;
    public GameObject scoreDisplay;
    public GameObject ControlMenu;
    private bool gamePaused = false;
    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerSlider.currentTime <= 0 && !gamePaused) {
            gamePaused = true;
            Time.timeScale = 0; // pause the game
            ControlMenu.SetActive(false);
            scoreDisplay.SetActive(false);
            menuPanel.SetActive(true); // show the menu
        }
    }
}
