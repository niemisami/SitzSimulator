/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviourScript: MonoBehaviour {
    public Text balanceText;
    public Slider balanceSlider;
    public Slider centerPointSlider;

    private float rotationAmount = 0F; //The rotation of the player
    private float centerPoint = 0F;
    private float centerPointEdges = 20F;
    private float centerPointOscillationAmount;
    public float centerPointSpeed = 1F;
    public float difficulty = 1F; //Value that changes the difficulty
    public float centerPiontWidth = 4F; 
    public float playerSpeed = 0.4F;


    private GameManagerScript GMS;


    // Use this for initialization
    void Start () {

        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        balanceText.text = "";
        centerPointOscillationAmount = 0.1F * centerPointSpeed;
}
    // Update is called once per frame
    void Update()
    {

        if (GMS.GameIsActive == true)
        {



            //Sets the balance center point
            if (centerPointSpeed == 0)
            {
                centerPointOscillationAmount = 0.1F * centerPointSpeed;
            }
            if (centerPointOscillationAmount == 0)
            {
                centerPointOscillationAmount = 0.1F * centerPointSpeed;
            }
            if (centerPoint >= centerPointEdges)
            {
                centerPointOscillationAmount = -0.1F * centerPointSpeed;
            }
            else if (centerPoint < -centerPointEdges)
            {
                centerPointOscillationAmount = 0.1F * centerPointSpeed;
            }
            centerPoint = centerPoint + centerPointOscillationAmount;
            //Calculates rotation
            if (rotationAmount >= centerPoint)
            {
                if (rotationAmount < centerPiontWidth)
                {
                    rotationAmount = rotationAmount + difficulty / centerPiontWidth;
                }
                else
                {
                    rotationAmount = rotationAmount + difficulty / rotationAmount;
                }
            }
            if (rotationAmount < centerPoint)
            {
                if (rotationAmount > -centerPiontWidth)
                {
                    rotationAmount = rotationAmount + difficulty / -centerPiontWidth;
                }
                else
                {
                    rotationAmount = rotationAmount + difficulty / rotationAmount;
                }
            }

            //Player movement
            if (Input.GetKey(KeyCode.A))
            {
                rotationAmount = rotationAmount + playerSpeed * difficulty;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rotationAmount = rotationAmount - playerSpeed * difficulty;
            }

            //Set rotation boundaries
            if (rotationAmount > centerPointEdges + 0.1F)
            {
                rotationAmount = centerPointEdges + 0.1F;
            }
            if (rotationAmount < -centerPointEdges - 0.1F)
            {
                rotationAmount = -centerPointEdges - 0.1F;
            }

            //Set values
            balanceText.text = "Balance: " + rotationAmount.ToString();
            transform.rotation = Quaternion.Euler(0, 0, rotationAmount);
            balanceSlider.value = -rotationAmount;
            centerPointSlider.value = -centerPoint;
        }

    }

    
}
*/