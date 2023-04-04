using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
    public Button Quit;
    public Button start;


    public void Start() {
        Quit.onClick.AddListener(QuitGame);
        start.onClick.AddListener(PlayGame);
    }

    public void QuitGame() {
        if(SceneManager.GetActiveScene().buildIndex == 1) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        } else {
            Application.Quit();

        }
    }

    public void PlayGame() {
        if(SceneManager.GetActiveScene().buildIndex == 1) {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
             
        } else {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
    
}