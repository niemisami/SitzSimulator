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

  public Direction Direction;
  public Vector2 Position;
  public KeyCode CorrectKeyCode;

  private Animator _anim;

  private bool _isActive = false;
  public bool isSuccess = false;

  private float _startTime;

  void Start()
  {
    _anim = GetComponent<Animator>();
    transform.rotation = Quaternion.Euler(0, 0, (float)Direction);
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

      int score = GameManager.instance.score + 1;
      GameManager.instance.updateScore(score);
      _anim.SetTrigger("success");
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