using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffectScript : MonoBehaviour {
    private float sinOutput;
    private float counter = 0;
	// Use this for initialization
	void Start () {
        sinOutput = 0;
    }
	// Update is called once per frame
	void FixedUpdate () {
        sinOutput = Mathf.Sin(counter);
        transform.position += new Vector3(0f, sinOutput, 0f);
        transform.rotation = Quaternion.Euler(0, 0, sinOutput*2);
        counter = counter + 0.05f;

    }
}
