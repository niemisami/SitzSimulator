using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SongBookController : MonoBehaviour
{

  public int alpha = 5;

  public Text keyStrokeText;
  public Text nextNotesText;

  private ArrowEventController arrowController;
  private float startTime;
  private float elapsedTime;
  public string[] songMappedCharacters;
  public KeyCode[] allowedKeys;
  private KeyCode lastKey;

  private GameManagerScript GMS;

  void Awake()
  {
    arrowController = new ArrowEventController();
  }
  void Start()
  {
    songMappedCharacters = new string[] { "A", "W", "S", "0", "D", "W", "S", "W", "A", "W", "S", "0", "D", "W", "S", "0", "A", "S", "W", "S", "D", "S", "W", "S", "A", "W", "S", "0", "D", "W", "S", "W" };
    allowedKeys = new KeyCode[] { KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow };

    arrowController.init(songMappedCharacters, nextNotesText);
    startTime = Time.time;
    displayText("Nothing");


    GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
  }

  void Update()
  {

    if (GMS.GameIsActive != true)
    {
      return;
    }

    foreach (KeyCode code in allowedKeys)
    {
      if (Input.GetKeyUp(code))
      {
        // TODO: use actual beats from sheet
        // elapsedTime = Time.time - startTime;
        // if (elapsedTime > 1.0)
        // {
        try
        {
          ArrowEventAction nextBeat = arrowController.getNextAction(code);
          nextBeat.Execute(keyStrokeText);
        }
        catch (KeyNotFoundException)
        {
          Debug.LogWarning("Pause");
        }
        startTime = Time.time;
      }
      // }
    }

  }

  void displayText(string stroke)
  {
    keyStrokeText.text = "Next: " + stroke;
  }
}


