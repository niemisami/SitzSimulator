using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine.UI;

public class ArrowEventController
{
  Dictionary<KeyCode, ArrowEventAction> keyActionMap;
  Dictionary<string, KeyCode> characterKeyMap;
  private string[] oldStrokes;
  private string[] songCorrectCharacters;
  private int nextIndex = 0;
  private Text nextNotesText;

  private Up upAction;
  private Down downAction;
  private Left leftAction;
  private Right rightAction;
  private Fail failAction;
  private End endAction;

  private StringBuilder sb;

  public void init(string[] songCorrectCharacters, Text nextNotesText)
  {


    // Map characters to correct Input KeyCodes
    characterKeyMap = new Dictionary<string, KeyCode>();
    characterKeyMap.Add("W", KeyCode.UpArrow);
    characterKeyMap.Add("A", KeyCode.LeftArrow);
    characterKeyMap.Add("S", KeyCode.DownArrow);
    characterKeyMap.Add("D", KeyCode.RightArrow);

    // Arrays of song chars; "beats" and directions
    oldStrokes = new string[songCorrectCharacters.Length];
    this.songCorrectCharacters = songCorrectCharacters;

    // Initialize commands objects which updates the UI
    upAction = new Up();
    leftAction = new Left();
    downAction = new Down();
    rightAction = new Right();
    failAction = new Fail();
    endAction = new End();

    // Map Input KeyCode to a certain command
    keyActionMap = new Dictionary<KeyCode, ArrowEventAction>();
    keyActionMap.Add(KeyCode.UpArrow, upAction);
    keyActionMap.Add(KeyCode.LeftArrow, leftAction);
    keyActionMap.Add(KeyCode.DownArrow, downAction);
    keyActionMap.Add(KeyCode.RightArrow, rightAction);

    // Text element for displaying next notes
    this.nextNotesText = nextNotesText;
    displayNextNotes(6);
  }

  /**
  * Evaluate whether passed KeyCode matches the correct one next on the line
  **/
  public ArrowEventAction getNextAction(KeyCode pressedKey)
  {
    // Show Game Over if no notes available
    if (nextIndex == songCorrectCharacters.Length)
    {
      return endAction;
    }
    string nextCharacter = songCorrectCharacters[nextIndex];
    nextIndex++;
    displayNextNotes(3);

    KeyCode expectedKeyCode = characterKeyMap[nextCharacter];
    if (pressedKey == expectedKeyCode)
    {
      oldStrokes[nextIndex] = nextCharacter;
      return keyActionMap[expectedKeyCode];
    }
    return failAction;
  }

  /*
  * Visual aid for displaying what should be pressed next
  **/
  void displayNextNotes(int display)
  {
    string nextCharacter = songCorrectCharacters[nextIndex];
    string nextText = "";
    int hintIndex = nextIndex - 1;
    try
    {
      for (int i = 0; i < display; i++)
      {
        nextText += characterKeyMap.ContainsKey(songCorrectCharacters[++hintIndex])
          ? characterKeyMap[songCorrectCharacters[hintIndex]].ToString()
          : " Pause" + " ";
      }
    }
    catch (KeyNotFoundException e)
    {
      Debug.LogError(e.ToString());
      nextText += " Pause";
    }
    catch (IndexOutOfRangeException e)  // CS0168
    {
      nextText += " END";
    }

    nextText += "...";
    nextNotesText.text = nextText;
  }

}

public abstract class ArrowEventAction
{
  public abstract void Execute(Text textContainer);
}

public class Up : ArrowEventAction
{
  public override void Execute(Text textContainer)
  {
    textContainer.text = "UP";
  }
}
public class Left : ArrowEventAction
{
  public override void Execute(Text textContainer)
  {
    textContainer.text = "LEFT";
  }
}
public class Right : ArrowEventAction
{
  public override void Execute(Text textContainer)
  {
    textContainer.text = "RIGHT";
  }
}
public class Down : ArrowEventAction
{
  public override void Execute(Text textContainer)
  {
    textContainer.text = "Down";
  }
}
public class Fail : ArrowEventAction
{
  public override void Execute(Text textContainer)
  {
    textContainer.text = "FAIL!";
  }
}
public class End : ArrowEventAction
{
  public override void Execute(Text textContainer)
  {
    textContainer.text = "GAME OVER!";
  }
}