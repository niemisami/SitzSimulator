using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCountDownDone : MonoBehaviour {

    private GameManagerScript GMS;

    public void setCountDown()
    {
        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        GMS.countDownDone = true;
    }
}
