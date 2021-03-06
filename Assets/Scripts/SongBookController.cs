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
  private GameObject songTimer;

  public Text keyStrokeText;
  public Text nextNotesText;

  private ArrowEventController arrowController;
  private float startTime;
  private float elapsedTime;
  public char[] songMappedCharacters;
  public KeyCode[] allowedKeys;
  private KeyCode lastKey;

  private Vector3 rowPadding = new Vector3(0F, 0.63f, 0F);
  private Vector3 arrowStartPosition = new Vector3(-2f, 3.53f, 0f);
  private Vector3 rowPositionVector = new Vector3(0f, 0f, 0f);

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
    int arrowsPerRow = 4;
    int rowIndex = 0;
    for (int i = 0; i < arrows.Length; i++)
    {
      char mappedChar = songMappedCharacters[i];
      // Don't instatiate new arrow on pause
      if (mappedChar != '0')
      {
        GameObject arrow = GameObject.Instantiate(arrowPrefab);
        arrow.transform.SetParent(transform);
        // Set arrows to start from top left corner and
        arrow.transform.localPosition = arrow.transform.position + new Vector3(rowIndex - 1.75f, 3.52f, 0f) + rowPositionVector;
        arrow.transform.localRotation = transform.rotation;
        arrow.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
        if (mappedChar == 'A')
        {
          arrow.GetComponent<Arrow>().Direction = Direction.Left;
          arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.LeftArrow;
        }
        else if (mappedChar == 'W')
        {
          arrow.GetComponent<Arrow>().Direction = Direction.Up;
          arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.UpArrow;
        }
        else if (mappedChar == 'D')
        {
          arrow.GetComponent<Arrow>().Direction = Direction.Right;
          arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.RightArrow;
        }
        else if (mappedChar == 'S')
        {
          arrow.GetComponent<Arrow>().Direction = Direction.Down;
          arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.DownArrow;
        }
        //        else
        //        {
        //          arrow.GetComponent<Arrow>().isActive = false;
        //          arrow.GetComponent<Arrow>().isSuccess = false;
        //        }
        arrows[i] = arrow;
      }

      // move position vector down when row ends
      rowIndex++;
      if (rowIndex == arrowsPerRow)
      {
        rowPositionVector -= rowPadding;
        rowIndex = 0;
      }
    }

    int maxScore = 0;
    foreach (var arrow in arrows)
    {
      if (arrow != null)
      {
        maxScore++;
      }
    }
    GameManager.instance.setMaxScore(maxScore);
  }

  void Update()
  {
  }
}


