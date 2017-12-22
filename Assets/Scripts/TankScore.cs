using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TankScore : MonoBehaviour {

    public Text scoreText;
    public int score;
    public string textName;
    ScoreManager scoreManager;
    public int playerNum;
	// Use this for initialization
	void Start ()
    {
        scoreManager = Statics.scoreManager;
        scoreText = GameObject.Find(textName).GetComponent<Text>();
        score = 0;
        scoreText.text = score.ToString();
	}
	
    public void updateScore()
    {
        score++;
        scoreText.text = score.ToString();
        if(scoreManager.scoreToWin == score)
        {
            scoreManager.winTheGame(playerNum);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
