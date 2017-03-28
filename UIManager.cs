using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Image gameOverPanel;

	// Use this for initialization
	void Start () {

        GameState.ChangeState(GameState.States.Start);

    }
	
	// Update is called once per frame
	void Update () {
        if (GameState.IsGameOver)
        {
            print("UI Manager running Gameover State");
            gameOverPanel.gameObject.SetActive(true);
        }
        else
            gameOverPanel.gameObject.SetActive(false);
    }

    public void RestartGame()
    {       
        SceneManager.LoadScene(0);
    }
}
