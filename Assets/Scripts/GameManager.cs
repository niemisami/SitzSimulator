using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
//  public static GameManager instance = null;
  public GameObject CountDown;
  public GameObject EndGame;
  public GameObject SongBook;
  public GameObject Player;
  public GameObject scoreText;
  public bool GameIsActive = false;
  
  public int score { get; set; }
  public int maxScore = 0;
  
  private int level = 1;
  

//  void Awake()
//  {
//    if (instance == null)
//    {
//      instance = this;
//    }
//    else if (instance != this)
//    {
//      Destroy(gameObject);
//    }
//    
//    DontDestroyOnLoad(gameObject);
//
//    SongBook = GameObject.Find("Song book (1)");
//    EndGame = GameObject.Find("EndGameCanvas");
//    EndGame.SetActive(false);
//  }

  private void Awake() {
    
    SongBook.GetComponent<ScrollingSongBook>().Init(this);
    Player.GetComponent<BalanceScript>().Init(this);
    CountDown.GetComponent<SetCountDownDone>().Init(this);
    scoreText.GetComponent<SetScoreText>().Init(this);
    EndGame.SetActive(false);
    
  }

  private void Start() {
  }

  public void startGame()
  {
    GameIsActive = true;
    SongBook.GetComponent<ScrollingSongBook>().PlaySong();
    }

  public void gameOver()
  {
    GameIsActive = false;
    print("Score: " + score + "/" + maxScore);
    SongBook.GetComponent<ScrollingSongBook>().StopSong();

    EndGame.SetActive(true);
    
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
