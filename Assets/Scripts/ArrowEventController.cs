using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine.UI;

public class ArrowEventController
{
  Dictionary<KeyCode, ArrowEventAction> keyActionMap;
  Dictionary<char, KeyCode> characterKeyMap;
  private char[] oldStrokes;
  private char[] songCorrectCharacters;
  private int nextIndex = 0;
  private Text nextNotesText;

  private Up upAction;
  private Down downAction;
  private Left leftAction;
  private Right rightAction;
  private Fail failAction;
  private End endAction;

  private StringBuilder sb;

  public void init(char[] songCorrectCharacters, Text nextNotesText)
  {
    // Map characters to correct Input KeyCodes
    characterKeyMap = new Dictionary<char, KeyCode>();
    characterKeyMap.Add('W', KeyCode.UpArrow);
    characterKeyMap.Add('A', KeyCode.LeftArrow);
    characterKeyMap.Add('S', KeyCode.DownArrow);
    characterKeyMap.Add('D', KeyCode.RightArrow);

    // Arrays of song chars; "beats" and directions
    oldStrokes = new char[songCorrectCharacters.Length];
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
    char nextCharacter = songCorrectCharacters[nextIndex];
    nextIndex++;
    displayNextNotes(3);
    try
    {
      KeyCode expectedKeyCode = characterKeyMap[nextCharacter];
      if (pressedKey == expectedKeyCode)
      {
        return keyActionMap[expectedKeyCode];
      }
    }
    catch (KeyNotFoundException)
    {
      Debug.LogWarning("Pause");
    }
    oldStrokes[nextIndex] = nextCharacter;
    return failAction;
  }

  /*
  * Visual aid for displaying what should be pressed next
  **/
  void displayNextNotes(int noteCount)
  {
    char nextCharacter = songCorrectCharacters[nextIndex];
    string nextText = "";
    int hintIndex = nextIndex - 1;
    try
    {
      for (int i = 0; i < noteCount; i++)
      {
        nextText += characterKeyMap.ContainsKey(songCorrectCharacters[++hintIndex])
          // FIXME: Really dirty way of removing word Arrow from KeyCode string
          ? characterKeyMap[songCorrectCharacters[hintIndex]].ToString().Substring(0, characterKeyMap[songCorrectCharacters[hintIndex]].ToString().Length - 5)
          : "Pause";
        nextText += " ";
      }
    }
    catch (KeyNotFoundException e)
    {
      Debug.LogError(e.ToString());
      nextText += " Pause";
    }
    catch (IndexOutOfRangeException)  // CS0168
    {
      nextText += "END";
    }

    if (hintIndex == songCorrectCharacters.Length)
    {
      nextText = "END";
    }
    else
    {
      nextText += "...";
    }
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
    textContainer.text = "DOWN";
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