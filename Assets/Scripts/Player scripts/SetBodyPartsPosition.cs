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
    private GameManagerScript GMS;

    // Use this for initialization
    void Start()
    {

        sinOutput = 0;

        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }




    // Update is called once per frame
    void Update()
    {
        if (GMS.GameIsActive != true)
        {
            return;
        }
        rotation = GameObject.Find("Player").transform.rotation;
        sinOutput = Mathf.Sin(counter);
        transform.rotation = Quaternion.Euler(0, sinOutput * horisontalRotationAmount, sinOutput * verticalRotationAmount)* rotation* rotation;
        counter = counter + effectAmount;
    }
}