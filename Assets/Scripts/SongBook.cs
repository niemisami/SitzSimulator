using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.UIElements;

public class SongBook : MonoBehaviour {

	public AudioClip Song;
	public float Volume;

	public GameObject Cursor;
	public GameObject ReferenceArrow;
	public Transform StartLine;
	public Transform EndLine;
	public float CursorVelocity;
	public int RowCount = 6;
	public float DistanceToNextRow = 1.28f;

//	private GameObject _cursor;
	private float _bpm;

	private Transform _cursorTransform;
	private Vector3 _cursorStartPosition;

	private int _jumpCount;
	private float _totalCursorTravelDistance;
	private float _rowWorldDistance;
	private float _songLengthSeconds;
	private float _startTime;

	private List<GameObject> _arrows = new List<GameObject>();
	private Dictionary<float, char> _mappedInputs;

	// Use this for initialization
	void Start() {

		var inputFileHandler = new InputFileHandler("Internationalen.txt");
		_songLengthSeconds = Song.length;
		_mappedInputs = inputFileHandler.MappingsDict;

		//Save the cursors transform and start position for easier referencing
		_cursorTransform = Cursor.transform;
		_cursorStartPosition = _cursorTransform.localPosition;

		//World distance is needed because the local distance is not changed when the songbook is resized.
		_rowWorldDistance = EndLine.position.x - StartLine.position.x;
		_totalCursorTravelDistance = _rowWorldDistance * RowCount;

		// (Average Velocity = Total Distance / Total Time)
		CursorVelocity = (_totalCursorTravelDistance / _songLengthSeconds);

		//For Debugging
		Debug.Log("Song duration: " + _songLengthSeconds);
		Debug.Log("World Distance: " + _totalCursorTravelDistance);
		Debug.Log("Cursor speed: " + CursorVelocity);

		//Create all the input arrows
		foreach (var item in _mappedInputs) {
			CreateArrow(item.Key, item.Value);
		}
		
		//Hide the reference arrow
		ReferenceArrow.SetActive(false);

		//Save the time when script is started
		_startTime = Time.time;
		
		//Play the song 		
		GetComponent<AudioSource>().PlayOneShot(Song, Volume);

	}

	// Update is called once per frame
	void FixedUpdate() {

		//If the cursor is on or over the end line then it should jump
		if (_cursorTransform.localPosition.x >= (EndLine.localPosition.x)) {
			HandleCursorJump(++_jumpCount);
		}

		//Move cursor to the right using the CursorSpeed
		_cursorTransform.Translate(Vector3.right * CursorVelocity * Time.deltaTime);

		//For debugging, Print time when cursor is on the position of a arrow
//		foreach (var arrow in _arrows) {
//			if (Math.Abs(_cursorTransform.localPosition.x - arrow.transform.localPosition.x) < 0.005 &&
//			    Math.Abs(_cursorTransform.localPosition.y - arrow.transform.localPosition.y) < 0.05 ) {
//				print(Time.time - _startTime);
//			}
//		}
	}

	private void HandleCursorJump(int count) {

		//Jump to the first row
		if (count % RowCount == 0) {
			_cursorTransform.localPosition = _cursorStartPosition;
			print(Time.time - _startTime); //Should be equal to song length
			_startTime = Time.time;
		}
		//jump to next row
		else {
			_cursorTransform.localPosition = new Vector3(
				StartLine.localPosition.x,
				_cursorTransform.localPosition.y - DistanceToNextRow
			);
		}
	}

	// Create a new input arrow and place it at the right position
	private void CreateArrow(float timeStamp, char directionCharacter) {

		var arrow = Instantiate(ReferenceArrow);
		arrow.transform.SetParent(transform);
		arrow.transform.localPosition = new Vector3(StartLine.localPosition.x, _cursorTransform.localPosition.y);

		var totalDistanceFromStart = CursorVelocity * timeStamp;
		var rowToPlaceArrow = (int) (totalDistanceFromStart / _rowWorldDistance);

		var xVector = Vector3.right * (totalDistanceFromStart % _rowWorldDistance); //vertical vector from start
		var yVector = Vector3.down * (DistanceToNextRow * rowToPlaceArrow);			//horizontal vector from first row

		arrow.transform.Translate(xVector, StartLine.transform);		//Move new arrow right relative to start line
		arrow.transform.Translate(yVector, ReferenceArrow.transform);	//Move new arrow down relative to the reference

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