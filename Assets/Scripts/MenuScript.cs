using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour {

    public string LoadLevel;


	// Use this for initialization
	void Start () {
		
	}
	

    public void Play()
    {
        SceneManager.LoadScene(LoadLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
