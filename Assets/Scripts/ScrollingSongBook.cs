using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ScrollingSongBook : MonoBehaviour {

	public AudioClip Song;
	public GameObject Cursor;
	public GameObject ReferenceArrow;
	public GameObject EndLine;
	public TextAsset SongInputFileName;
	public float Bpm;
	

	private float _totalDistance;
	private float _cursorVelocity;
	private Rigidbody2D _cursorBody;
	
	private float _rowDistance;
	private float _songLengthSeconds;
	private List<GameObject> _arrows = new List<GameObject>();
	private Dictionary<float, char> _mappedInputs;
	
	private const int BeatsPerDownBeat = 4;
	private float _inputTimeDelta; //Duration between the downbeats, used to align the beat inputs
	private float _downBeatDelta; //Relative distance between four beats in the song
	private const float LocalDistanceBetweenRows = 1.28f;


	// Use this for initialization
	void Start () {
		
		print(SongInputFileName.name);
		
		var inputFileHandler = new InputFileHandler(SongInputFileName.name);
		_songLengthSeconds = Song.length;
		print("Song length: " + _songLengthSeconds);
		
		_mappedInputs = inputFileHandler.MappingsDict;
		
		//World distance is used because the local distance is not changed when the songbook is resized.
		_rowDistance = (EndLine.transform.position.x - Cursor.transform.position.x);

		_inputTimeDelta = BeatsPerDownBeat - (Bpm / 60);
		_downBeatDelta = _inputTimeDelta / _songLengthSeconds;
		print(_inputTimeDelta);
		
		_totalDistance =  _rowDistance / _downBeatDelta;
		print(_totalDistance);

		// (Average Velocity = Total Distance / Total Time)
		_cursorVelocity = _totalDistance / _songLengthSeconds;
		_cursorBody = Cursor.GetComponent<Rigidbody2D>();	
	
		//Create all the input arrows
		foreach (var item in _mappedInputs) {
			CreateArrow(item.Key, item.Value);
		}

		//Hide the reference arrow
		ReferenceArrow.gameObject.SetActive(false);

		//Play the song 
		GetComponent<AudioSource>().PlayOneShot(Song);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Cursor.transform.Translate(Vector3.right * _cursorVelocity * Time.deltaTime);
		
		//Move cursor to the right using the cursors velocity
//		_cursorBody.velocity = Vector2.right * _cursorVelocity;

		if (Cursor.transform.position.x >= EndLine.transform.position.x) {
			
			//Move cursor to the start
			_cursorBody.transform.Translate(Vector3.left * _rowDistance);

			//Move all arrows one line up
			foreach (var arrow in _arrows) {
				arrow.transform.Translate(Vector3.up * LocalDistanceBetweenRows, ReferenceArrow.transform);
			}
		}
	}

	private void CreateArrow(float timeStamp, char directionCharacter) {

		//Position of timestamp in percent
		var relativeSongPosition = timeStamp / _songLengthSeconds;
		
		//Total cursor travel distance to where the arrow should be
		var arrowDistanceFromStart = relativeSongPosition * _totalDistance;
		
		//Row number is (total distance / row distance)
		var rowToPlaceArrow = (int)(arrowDistanceFromStart / _rowDistance);
		
		//Arrow x position is the remainder
		var xVector = Vector3.right * (arrowDistanceFromStart % _rowDistance);
		
		//Arrow y position row number * padding
		var yVector = Vector3.down * rowToPlaceArrow * LocalDistanceBetweenRows;
				
		var arrow = Instantiate(ReferenceArrow, transform);
		var body = arrow.GetComponent<Rigidbody2D>();
		
		body.transform.Translate(xVector, Cursor.transform); //Move new arrow right relative to cursor start pos
		body.transform.Translate(yVector, ReferenceArrow.transform); //Move new arrow down relative to the reference
		
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
				//Debug.Log("Unknown direction character");
				//arrow.SetActive(false);
				break;
		}

		_arrows.Add(arrow);
	}

}
