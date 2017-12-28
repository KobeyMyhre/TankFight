using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathMatchGameMode : MonoBehaviour {

    public string GameType;
    public Text startTimer;
    public float matchDelay;
    public GameObject[] Players;
    public Transform[] spawnPoints;
    AudioSource countDown;
    public Statics.GameMode gameMode;
	// Use this for initialization
	void Start ()
    {
        countDown = GetComponent<AudioSource>();
        countDown.Play();
	}

    
    

    void spawnPlayer(GameObject player, Transform spawnPos)
    {
        var spawnedPlayer = Instantiate(player);
        spawnedPlayer.transform.position = spawnPos.position;
        spawnedPlayer.GetComponentInChildren<TankScore>().gameMode = gameMode;
       // spawnedPlayer.transform.rotation = spawnPos.rotation;
    }

	// Update is called once per frame
	void Update ()
    {
        matchDelay -= Time.deltaTime;
        if(matchDelay > 6)
        {
            startTimer.text = GameType;
        }
        else
        {
            int intTime = ((int)matchDelay);
            startTimer.text = intTime.ToString();
        }
        
        
        if(matchDelay <= 0)
        {
            for(int i = 0; i < Players.Length; i++)
            {
                spawnPlayer(Players[i], spawnPoints[i]);
            }
            startTimer.text = "";
            gameObject.SetActive(false);
        }
	}
}
