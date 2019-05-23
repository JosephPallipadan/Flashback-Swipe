using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = CommandSpawner.score + "";

        if(PlayerPrefs.HasKey("High Score") )
        {
            if (PlayerPrefs.GetInt("High Score") < CommandSpawner.score)
                PlayerPrefs.SetInt("High Score", CommandSpawner.score);
        }

        else
        {
            PlayerPrefs.SetInt("High Score", CommandSpawner.score);
        }

        GameObject.Find("Best Score").GetComponent<Text>().text = PlayerPrefs.GetInt("High Score") + "";

        string scoreAdjective = "";

        int score = CommandSpawner.score;
        if (score < 1000)
            scoreAdjective = "Primitive";
        else if(score<3000)
            scoreAdjective = "Pityful";
        else if(score<6000)
            scoreAdjective = "Tolerable";
        else if(score<9000)
            scoreAdjective = "Respectable";
        else
            scoreAdjective = "Spectacular";

        GameObject.Find("Score Adjective").GetComponent<Text>().text = scoreAdjective;

    }
	
	
}
