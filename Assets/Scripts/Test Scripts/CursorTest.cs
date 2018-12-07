using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTest : MonoBehaviour {

	public AudioClip Song;
	public GameObject Cursor;
	public GameObject Arrow2;
	public float CursorVelocity;
	private float TotalDistance = 200;
	
	private float _songLengthSeconds;
	private List<GameObject> _arrows = new List<GameObject>();
	private Dictionary<float, char> _mappedInputs;



	// Use this for initialization
	void Start () {
		
		var inputFileHandler = new InputFileHandler("Internationalen.txt");
		_songLengthSeconds = Song.length;
		print("Song length: " + _songLengthSeconds);
		
		_mappedInputs = inputFileHandler.MappingsDict;
		
		foreach (var item in _mappedInputs) {
			CreateArrow(item.Key, item.Value);
		}
		
		GetComponent<AudioSource>().PlayOneShot(Song);

		CursorVelocity = TotalDistance / _songLengthSeconds;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Cursor.transform.Translate(Vector3.right * CursorVelocity * Time.deltaTime);

		if (Cursor.transform.position.x >= TotalDistance) {
			Cursor.transform.Translate(Vector3.left * TotalDistance);
		}

	}
	
	private void CreateArrow(float timeStamp, char directionCharacter) {

		var relativeSongPosition = timeStamp / _songLengthSeconds;

		Vector3 spawnPosition = Vector3.right * relativeSongPosition * TotalDistance;
		
		var arrow = Instantiate(Arrow2, spawnPosition, Quaternion.identity);
		
	
		switch (directionCharacter) {

			case 'W':
				arrow.GetComponent<Arrow>().Direction = Direction.Up;
				arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.UpArrow;
				break;
			case 'A':
				arrow.GetComponent<Arrow>().Direction = Direction.Left;
				arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.LeftArrow;
				break;
			case 'S':
				arrow.GetComponent<Arrow>().Direction = Direction.Down;
				arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.DownArrow;
				break;
			case 'D':
				arrow.GetComponent<Arrow>().Direction = Direction.Right;
				arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.RightArrow;
				break;
			default:
				Debug.Log("Unknown direction character");
				break;
		}

		_arrows.Add(arrow);
	}

}
