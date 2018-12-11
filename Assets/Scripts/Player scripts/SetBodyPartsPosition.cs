using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBodyPartsPosition : MonoBehaviour
{
    Quaternion rotation;

    private float sinOutput;
    private float counter = 0;
    public float effectAmount = 0.03f;
    public float verticalRotationAmount = 7;
    public float horisontalRotationAmount = 7;
    public bool rotationON;

    // Use this for initialization
    void Start()
    {
        sinOutput = 0;
    }




    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if (GameManager.instance.GameIsActive != true)
        {
            return;
        }
        */
        rotation = GameObject.Find("Player").transform.rotation;

        if (rotationON)
        {
            transform.rotation = Quaternion.Euler(0, sinOutput * horisontalRotationAmount, sinOutput * verticalRotationAmount) * rotation * rotation;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, sinOutput * horisontalRotationAmount, sinOutput * verticalRotationAmount)*rotation;
        }
        sinOutput = Mathf.Sin(counter);
        counter = counter + effectAmount;
    }
}