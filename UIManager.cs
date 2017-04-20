using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Image gameOverPanel;
    public Text planetsJumpedText, distanceTravelledText, coinsCountText;

    ScoreManager sm;

	// Use this for initialization
	void Start () {
        sm = FindObjectOfType<ScoreManager>().GetComponent<ScoreManager>();
        GameState.ChangeState(GameState.States.Start);

    }
	
	// Update is called once per frame
	void Update () {
        if (GameState.IsGameOver)
        {

            gameOverPanel.gameObject.SetActive(true);
        }
        else
            gameOverPanel.gameObject.SetActive(false);

        planetsJumpedText.text = "Planets: " + sm.AddToPlanetsJumped;
        distanceTravelledText.text = "Distance: " + Mathf.Round(sm.AddToDistance);
        coinsCountText.text = "Coins " + sm.AddToCoin;

    }

    public void RestartGame()
    {       
        SceneManager.LoadScene(0);
    }
}
