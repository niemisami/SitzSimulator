using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScoreText : MonoBehaviour {

    private GameManager _gameManager;
    private int maxScore;
    private int score;
    public Text scoreText;

    public void Init(GameManager injected)
    {
        _gameManager = injected;
    }

    void Start()
    {
        maxScore = _gameManager.maxScore;
        score = _gameManager.score;
        scoreText.text = score + "/"+ maxScore;

    }
}
