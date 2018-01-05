using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumberOfPlayers : MonoBehaviour {

    public Text displayNumOfPlayers;
    public int numOfPlayers;
    public int numOfAi;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this);
        numOfPlayers = 4;
        numOfAi = 0;
        displayNumOfPlayers.text = numOfPlayers.ToString();
	}
	

    public void addPlayer()
    {
        numOfPlayers+= 1;
        numOfAi = 4 - numOfPlayers;
        maintainNum();
        displayNumOfPlayers.text = numOfPlayers.ToString();
    }

    public void removePlayer()
    {
        numOfPlayers-= 1;
        numOfAi = 4 - numOfPlayers;
        maintainNum();
        displayNumOfPlayers.text = numOfPlayers.ToString();
    }

    void maintainNum()
    {
        numOfPlayers = Mathf.Clamp(numOfPlayers,1, 4);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
