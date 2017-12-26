using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathMatchGameMode : MonoBehaviour {

    public Text startTimer;
    public float matchDelay;
    public GameObject[] Players;
    public Transform[] spawnPoints;
    AudioSource countDown;
	// Use this for initialization
	void Start ()
    {
        countDown = GetComponent<AudioSource>();
        countDown.Play();
	}

    int counter = 5;
    

    void spawnPlayer(GameObject player, Transform spawnPos)
    {
        var spawnedPlayer = Instantiate(player);
        spawnedPlayer.transform.position = spawnPos.position;
       // spawnedPlayer.transform.rotation = spawnPos.rotation;
    }

	// Update is called once per frame
	void Update ()
    {
        matchDelay -= Time.deltaTime;
        if(matchDelay > 6)
        {
            startTimer.text = "DeathMatch";
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
