using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAlphaForBackground : MonoBehaviour {

    public float fadeSpeed = 0.001f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, fadeSpeed);
    }
}
