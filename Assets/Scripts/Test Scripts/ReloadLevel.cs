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


	public void NextLevel() {
		//SceneManager.LoadScene("Internationalen");
        GameObject.Find("level changer").GetComponent<LevelCreaterScript>().FadeToLevel("Internationalen");
    }
	
	
    public void reloadLevel()
    {
//        Application.LoadLevel(Application.loadedLevel);
	    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameObject.Find("level changer").GetComponent<LevelCreaterScript>().FadeToLevel(SceneManager.GetActiveScene().name);
    }
    public void backToMenu()
    {
        //SceneManager.LoadScene("Menu");
        GameObject.Find("level changer").GetComponent<LevelCreaterScript>().FadeToLevel("Menu");
    }
}
