using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void reloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
