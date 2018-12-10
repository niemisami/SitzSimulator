using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAlphaForBackground : MonoBehaviour {

    public float fadeSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        

        GetComponent<Image>().color += new Color(0, 0, 0, fadeSpeed);
    }
}
