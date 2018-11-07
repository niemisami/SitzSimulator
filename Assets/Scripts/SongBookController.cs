using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SongBookController : MonoBehaviour
{

  [SerializeField]
  private GameObject arrowPrefab;
  private GameObject[] arrows;

  public Text keyStrokeText;
  public Text nextNotesText;

  private ArrowEventController arrowController;
  private float startTime;
  private float elapsedTime;
  public char[] songMappedCharacters;
  public KeyCode[] allowedKeys;
  private KeyCode lastKey;

  private GameManagerScript GMS;

  void Awake()
  {
    arrowController = new ArrowEventController();
  }
  void Start()
  {
    songMappedCharacters = new char[] { 'A', 'W', 'S', '0', 'D', 'W', 'S', 'W', 'A', 'W', 'S', '0', 'D', 'W', 'S', '0', 'A', 'S', 'W', 'S', 'D', 'S', 'W', 'S', 'A', 'W', 'S', '0', 'D', 'W', 'S', 'W' };
    allowedKeys = new KeyCode[] { KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow };

    arrowController.init(songMappedCharacters, nextNotesText);
    startTime = Time.time;

    arrows = new GameObject[songMappedCharacters.Length];
    for (int i = 0; i < arrows.Length; i++)
    {
      GameObject songBook = GameObject.Find("Song book");
      Vector3 songBookOrigo = new Vector3(songBook.transform.position.x, songBook.transform.position.y, songBook.transform.position.z);
      Vector3 initialArrowPosition = new Vector3(0f, 0f, 0f);
      GameObject arrow = GameObject.Instantiate(arrowPrefab, initialArrowPosition, songBook.transform.rotation);
      arrow.transform.SetParent(songBook.transform, true);
      arrow.transform.position = new Vector3(i -1.05f, 3.52f, 0f);
      if (songMappedCharacters[i] == 'A')
      {
        arrow.GetComponent<Arrow>().direction = Direction.Left;
        arrow.GetComponent<Arrow>().correctKeyCode = KeyCode.LeftArrow;
      }
      else if (songMappedCharacters[i] == 'W')
      {
        arrow.GetComponent<Arrow>().direction = Direction.Up;
        arrow.GetComponent<Arrow>().correctKeyCode = KeyCode.UpArrow;
      }
      else if (songMappedCharacters[i] == 'D')
      {
        arrow.GetComponent<Arrow>().direction = Direction.Right;
        arrow.GetComponent<Arrow>().correctKeyCode = KeyCode.RightArrow;
      }
      else if (songMappedCharacters[i] == 'S')
      {
        arrow.GetComponent<Arrow>().direction = Direction.Down;
        arrow.GetComponent<Arrow>().correctKeyCode = KeyCode.DownArrow;
      }
      else
      {
        arrow.GetComponent<Arrow>().isVisited = true;
        arrow.GetComponent<Arrow>().isFailed = true;
      }

      arrows[i] = arrow;
    }


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


