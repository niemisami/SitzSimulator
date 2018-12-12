using System;
using UnityEngine;
using System.Collections;

public enum Direction
{

  Left = 0,
  Up = -90,
  Right = 180,
  Down = 90

};

public class Arrow : MonoBehaviour
{

  public Direction direction;
  public Vector2 Position;
  public KeyCode CorrectKeyCode;

  private Animator _anim;
  private GameManager _gameManager;

  private bool _isActive = false;
  public bool isSuccess = false;

  private float _startTime;

  public void Init(GameManager injected) {
    _gameManager = injected;
  }

  void Start()
  {
    _anim = GetComponent<Animator>();
    transform.rotation = Quaternion.Euler(0, 0, (float)direction);
    _startTime = Time.time;

  }

  void Update()
  {
    if (!_isActive)
    {
      return;
    }

    if (Input.GetKeyDown(CorrectKeyCode))
    {
      isSuccess = true;
      _gameManager.score++;
      _anim.SetTrigger("success");
      
      //Needs to be deactivated to ensure only one point per arrow
      _isActive = false;
      
    }
    else if (Input.anyKeyDown && !Input.GetKeyDown("w") && !Input.GetKeyDown("a") && !Input.GetKeyDown("s") && !Input.GetKeyDown("d"))
    {
      isSuccess = false;
      _anim.SetTrigger("fail");
    }
  }

  void OnTriggerEnter2D(Collider2D col)
  {
    _isActive = true;
  }

  void OnTriggerExit2D(Collider2D col)
  {

    if (!isSuccess)
    {
      _anim.SetTrigger("fail");
    }
    _isActive = false;
  }

}