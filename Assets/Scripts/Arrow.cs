using UnityEngine;
using System.Collections;

public enum Direction { Left = 0, Up = -90, Right = 180, Down = 90 };

public class Arrow : MonoBehaviour
{

  public Direction direction;
  public Vector2 position;
  public KeyCode correctKeyCode;
  public bool isVisited = false;
  public bool isFailed = false;

  private Animator anim;

  private GameManagerScript GMS;
  // private Rigidbody2D arrowRb2d;  

  void Start()
  {
    anim = GetComponent<Animator>();
    transform.rotation = Quaternion.Euler(0, 0, (float)direction);
    // arrowRb2d = GetComponent<Rigidbody2D>();
    GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

  }

  void Update()
  {
    if (GMS.GameIsActive != true)
    {
      return;
    }

    if (isVisited == false)
    {
      //Look for input to trigger a "flap".
      if (Input.GetKeyDown(correctKeyCode))
      {
        isVisited = true;
        anim.SetTrigger("success");
      }
      else
      {
        isVisited = false;
        anim.SetTrigger("fail");
      }
    }
  }
}