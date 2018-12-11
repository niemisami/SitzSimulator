using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCreaterScript : MonoBehaviour {

    public Animator animator;
    public string LevelToLoad;
	
	// Update is called once per frame
	void Update () {
        

    }

    public void FadeToLevel (string levelName)
    {
        animator.SetTrigger("Fade out");
        LevelToLoad = levelName;
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
