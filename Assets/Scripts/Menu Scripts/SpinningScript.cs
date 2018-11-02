using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningScript : MonoBehaviour {
    public float spinningAmount = 0f;
	// Update is called once per frame
	void FixedUpdate () {
        spinningAmount += 0.5f;
        transform.rotation = Quaternion.Euler(0,  0, spinningAmount);
    }
}
