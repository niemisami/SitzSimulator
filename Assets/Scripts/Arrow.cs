using UnityEngine;
using System.Collections;

public enum Direction { Left = 0, Up = -90, Right = 180, Down = 90 };

public class Arrow : MonoBehaviour
{

  public Direction direction;
  public Vector2 position;
  public KeyCode correctKeyCode;
  public bool isActive = false;
  public bool isSuccess = false;

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
    if (GMS.GameIsActive != true || isActive == false)
    {
      return;
    }

    if (Input.GetKeyDown(correctKeyCode))
    {
      isSuccess = true;
    }
  }

  void OnTriggerEnter2D(Collider2D col)
  {
    isActive = true;
    Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
  }

  void OnTriggerExit2D(Collider2D col)
  {
    isActive = false;
    if(isSuccess == false) {
      anim.SetTrigger("fail");
    } else {
      anim.SetTrigger("success");
    }

  }
}