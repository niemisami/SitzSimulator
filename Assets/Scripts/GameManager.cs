using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;
  public bool GameIsActive = false;
  public int score = 0;
  public int maxScore = 0;
  private int level = 1;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
  }

  public void startGame()
  {
    GameIsActive = true;
  }

  public void gameOver()
  {
    GameIsActive = false;
    print("Score: " + score + "/" + maxScore);  
  }

  public void updateScore(int score)
  {
    this.score = score;
  }
  public void setMaxScore(int maxScore)
  {
    this.maxScore = maxScore;
  }

  public void setLevel(int level)
  {
    this.level = level;
  }

  void Update()
  {

  }
}
