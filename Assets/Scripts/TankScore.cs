using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TankScore : MonoBehaviour {

    public Text scoreText;
    public float score;
    public string textName;
    ScoreManager scoreManager;
    public int playerNum;
    public Statics.GameMode gameMode;
	// Use this for initialization
	void Start ()
    {
        scoreManager = Statics.scoreManager;
        scoreText = GameObject.Find(textName).GetComponent<Text>();
        score = 0;
        scoreText.text = score.ToString();
	}
	
    public void onKillScore()
    {
        score++;
        int wholeScore = (int) score;
        scoreText.text = wholeScore.ToString();
        if (scoreManager.scoreToWin == wholeScore)
        {
            scoreManager.winTheGame(playerNum);
        }
    }

    public void kingOfTheHillScore()
    {
        score += Mathf.Lerp(0, scoreManager.scoreToWin, scoreManager.KOTHPointSpeed * Time.deltaTime);
        int WholeScore = (int)score;
        scoreText.text = WholeScore.ToString();
        if (scoreManager.scoreToWin == WholeScore)
        {
            scoreManager.winTheGame(playerNum);
        }
    }

    public void updateScore()
    {
       switch(gameMode)
        {
            case Statics.GameMode.DeathMatch:
                onKillScore();
                break;
            case Statics.GameMode.KingOfTheHill:
                kingOfTheHillScore();
                break;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
