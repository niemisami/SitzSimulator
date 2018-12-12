using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCountDownDone : MonoBehaviour {

    private GameManager _gameManager;

    public void Init(GameManager injected) {
        _gameManager = injected;
    }

    public void setCountDown()
    {
        _gameManager.startGame();
        
    }
}
