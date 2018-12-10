using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;
  public bool GameIsActive = false;
  private int score = 0;
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

  public void startGame() {
    GameIsActive = true;
  }

  public void gameOver() {
    GameIsActive = false;
  }

  public void updateScore(int score)
  {
    this.score = score;
  }

  public void setLevel(int level)
  {
    this.level = level;
  }

  void Update() {

  }
}
