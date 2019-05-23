using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour {

	public void StartGame()
    {
        SceneManager.LoadScene("Game Screen");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over Screen");
;   }

    public void BackToStart()
    {
        SceneManager.LoadScene("Start Screen");
    }
}
