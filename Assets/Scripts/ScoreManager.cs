using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour {

    public int scoreToWin;
    public List<RespawnManager> players;
    public GameObject gameOverUI;
    public float sendToMenuDelay;
    public bool gameOver = false;
	// Use this for initialization
	void Start ()
    {
        players = new List<RespawnManager>();
        
	}


    public void winTheGame(int selfIdx)
    {
        gameOver = true;
        gameOverUI.SetActive(true);
        for(int i =0; i < players.Count;i++)
        {
            
            Debug.Log(players[i].gameObject.name);
            
            if (i != selfIdx - 1)
            {
                players[i].gameObject.SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(gameOver)
        {
            sendToMenuDelay -= Time.deltaTime;
            if(sendToMenuDelay <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
	}
}
