using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ArrowEventController
{
  Dictionary<string, ArrowEventAction> beatEventMap;
  private string[] oldStrokes;
  private string[] strokes;
  private int nextIndex = 0;

  private Up upAction;
  private Down downAction;
  private Left leftAction;
  private Right rightAction;

  public void init(string[] strokes)
  {
    beatEventMap = new Dictionary<string, ArrowEventAction>();

    upAction = new Up();
    downAction = new Down();
    leftAction = new Left();
    rightAction = new Right();

    beatEventMap.Add("W", upAction);
    beatEventMap.Add("A", leftAction);
    beatEventMap.Add("S", downAction);
    beatEventMap.Add("D", rightAction);
    oldStrokes = new string[strokes.Length];
    this.strokes = strokes;
  }

  public ArrowEventAction getNextAction()
  {
    string nextKey = strokes[nextIndex];
    oldStrokes[nextIndex] = nextKey;
    nextIndex++;
    return beatEventMap[nextKey];
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
    textContainer.text = "Next: UP";
  }
}
public class Left : ArrowEventAction
{
  public override void Execute(Text textContainer)
  {
    textContainer.text = "Next: LEFT";
  }
}
public class Right : ArrowEventAction
{
  public override void Execute(Text textContainer)
  {
    textContainer.text = "Next: RIGHT";
  }
}
public class Down : ArrowEventAction
{
  public override void Execute(Text textContainer)
  {
    textContainer.text = "Next: Down";
  }
}