using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCountDownDone : MonoBehaviour {

    public void setCountDown()
    {
        GameManager.instance.startGame();
    }
}
