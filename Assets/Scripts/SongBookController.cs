using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SongBookController : MonoBehaviour
{

  public int alpha = 5;

  public Text keyStrokeText;

  private ArrowEventController arrowController;
  private float startTime;
  private float elapsedTime;
  public string[] beats;

  void Awake()
  {
  }
  void Start()
  {
    beats = new string[] { "A", "W", "S", "0", "D", "W", "S", "W", "A", "W", "S", "0", "D", "W", "S", "0", "A", "S", "W", "S", "D", "S", "W", "S", "A", "W", "S", "0", "D", "W", "S", "W" };

    arrowController = new ArrowEventController();

    arrowController.init(beats);
    startTime = Time.time;
    displayText("Nothing");
  }

  void Update()
  {
    // if (Input.GetKeyUp(KeyCode.UpArrow))
    // {
    //   displayText("UP");
    // }
    // if (Input.GetKeyUp(KeyCode.DownArrow))
    // {
    //   displayText("DOWN");
    // }
    // if (Input.GetKeyUp(KeyCode.LeftArrow))
    // {
    //   displayText("LEFT");
    // }
    // if (Input.GetKeyUp(KeyCode.RightArrow))
    // {
    //   displayText("RIGHT");
    // }
    elapsedTime = Time.time - startTime;
    Debug.Log(elapsedTime);
    Console.WriteLine(elapsedTime > 1);
    if (elapsedTime > 1.0)
    {
      Debug.Log("HERE");
      ArrowEventAction nextBeat = arrowController.getNextAction();
      nextBeat.Execute(keyStrokeText);
      startTime = Time.time;
    }
  }

  void displayText(string stroke)
  {
    keyStrokeText.text = "Next: " + stroke;
  }
}


