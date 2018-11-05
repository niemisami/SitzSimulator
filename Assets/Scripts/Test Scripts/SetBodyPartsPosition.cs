using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBodyPartsPosition : MonoBehaviour
{

    Vector3 startPos;
    Vector3 playerStartPos;
    float startX;
    float startY;
    float playerX;
    float playerY;
    float playerRotation;
    float test = 100f;
    Quaternion rotation;


    private float sinOutput;
    private float counter = 0;
    public float effectAmount = 0.03f;
    public float verticalMovementAmount = 1f;
    public float verticalRotationAmount = 7;
    public float horisontalRotationAmount = 7;
    public bool verticalMovement = true;


    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        startX = transform.position.x;
        startY = transform.position.y;
        playerX = GameObject.Find("Player").transform.position.x;
        playerY = GameObject.Find("Player").transform.position.y;
        print(GameObject.Find("Player").transform.rotation);
        playerStartPos = GameObject.Find("Player").transform.position;
        sinOutput = 0;


    }




    // Update is called once per frame
    void Update()
    {

        rotation = new Quaternion(GameObject.Find("Player").transform.rotation.x* test, GameObject.Find("Player").transform.rotation.y* test, GameObject.Find("Player").transform.rotation.z* test, GameObject.Find("Player").transform.rotation.w* test);
        
        //transform.localRotation = rotation;

        //transform.position = startPos + GameObject.Find("Player").transform.position- playerStartPos;
        sinOutput = Mathf.Sin(counter);
        if (verticalMovement)
        {
            transform.position += new Vector3(0, sinOutput * verticalMovementAmount, 0);
        }
        transform.rotation = Quaternion.Euler(0, sinOutput * horisontalRotationAmount, sinOutput * verticalRotationAmount)* rotation* rotation;
        counter = counter + effectAmount;


    }
}