using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathMatchGameMode : MonoBehaviour {

    public Text startTimer;
    public float matchDelay;
    public GameObject[] Players;
    public Transform[] spawnPoints;

	// Use this for initialization
	void Start ()
    {
		
	}
	
    void spawnPlayer(GameObject player, Transform spawnPos)
    {
        var spawnedPlayer = Instantiate(player);
        spawnedPlayer.transform.position = spawnPos.position;
        spawnedPlayer.transform.rotation = spawnPos.rotation;
    }

	// Update is called once per frame
	void Update ()
    {
        matchDelay -= Time.deltaTime;
        startTimer.text = ((int)matchDelay).ToString();
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
