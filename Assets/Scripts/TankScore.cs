using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TankScore : MonoBehaviour {

    public Text scoreText;
    public int score;
    public string textName;

	// Use this for initialization
	void Start ()
    {
        scoreText = GameObject.Find(textName).GetComponent<Text>();
        score = 0;
        scoreText.text = score.ToString();
	}
	
    public void updateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
