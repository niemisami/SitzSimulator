using UnityEngine;

public class CreateArrowSprites : MonoBehaviour
{
  public Texture2D leftTexture;
  public Texture2D upTexture;
  public Texture2D rightTexture;
  public Texture2D downTexture;
  private Sprite leftArrowSprite;
  private Sprite upArrowSprite;
  private Sprite rightArrowSprite;
  private Sprite downArrowSprite;
  private SpriteRenderer srLeft;
  private SpriteRenderer srUp;
  private SpriteRenderer srRight;
  private SpriteRenderer srDown;

  void Awake()
  {
    srLeft = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
    srUp = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
    srRight = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
    srDown = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;

    // sr.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);

    transform.position = new Vector3(1.5f, 1.5f, 0.0f);
  }

  void Start()
  {
    leftArrowSprite = Sprite.Create(leftTexture, new Rect(0.0f, 0.0f, leftTexture.width, leftTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    upArrowSprite = Sprite.Create(upTexture, new Rect(0.0f, 0.0f, upTexture.width, upTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    rightArrowSprite = Sprite.Create(rightTexture, new Rect(0.0f, 0.0f, rightTexture.width, rightTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    downArrowSprite = Sprite.Create(downTexture, new Rect(0.0f, 0.0f, downTexture.width, downTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
  }

  void OnGUI()
  {
    Debug.Log("UI CLICKED");
    if (GUI.Button(new Rect(10, 10, 100, 30), "Add sprite"))
    {
      srLeft.sprite = leftArrowSprite;
      srUp.sprite = upArrowSprite;
      srRight.sprite = rightArrowSprite;
      srDown.sprite = downArrowSprite;
    }
  }
}