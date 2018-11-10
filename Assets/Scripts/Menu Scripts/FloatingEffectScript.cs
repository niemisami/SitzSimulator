using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffectScript : MonoBehaviour {
    private float sinOutput;
    private float counter = 0;
    public float effectAmount = 0.03f;
    public float verticalMovementAmount = 1f;
    public float verticalRotationAmount = 7;
    public float horisontalRotationAmount = 7;
    public bool verticalMovement = true;
    private GameManagerScript GMS;
    // Use this for initialization
    void Start () {
        sinOutput = 0;

        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }
	// Update is called once per frame
	void FixedUpdate () {

        sinOutput = Mathf.Sin(counter);
        if (verticalMovement)
        {
            transform.position += new Vector3(0, sinOutput* verticalMovementAmount, 0);
        }
        transform.rotation = Quaternion.Euler(0, sinOutput * horisontalRotationAmount, sinOutput * verticalRotationAmount);
        counter = counter + effectAmount;

    }
}
