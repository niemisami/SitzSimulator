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
	public GameObject ReferenceDistance;
	public GameObject EndLine;
	public GameObject TopLine;
	public GameObject BottomLine;
	public TextAsset SongInputFileName;
	public float Bpm;

	private GameManager _gameManager;
	

	private float _totalDistance;
	private float _cursorVelocity;
	private Rigidbody2D _cursorBody;
	
	private float _rowDistance;
	private float _songLengthSeconds;
	private float _worldDistanceBetweenRows;
	private float _offset = 0;
	
  public Sprite spriteLeft;
  public Sprite spriteUp;
  public Sprite spriteRight;
  public Sprite spriteDown;

	private Dictionary<float, char> _mappedInputs;
	private List<GameObject> _arrows = new List<GameObject>();
	
	private const int BeatsPerDownBeat = 4;
	private float _inputTimeDelta; //Duration between the downbeats, used to align the beat inputs
	private float _downBeatDelta; //Relative distance between four beats in the song

	private int points;
	private float _startTime;

	public void PlaySong() {
		//Play the song 
		GetComponent<AudioSource>().PlayOneShot(Song);
		_startTime = Time.time;
		_gameManager.score = 0;
	}

	public void StopSong() {
		GetComponent<AudioSource>().Stop();
	}

	public void Init(GameManager injected) {
		_gameManager = injected;
	}
	
	// Use this for initialization
	void Start () {

//		print(SongInputFileName.name);
		
		var inputFileHandler = new InputFileHandler(SongInputFileName.name);
		_songLengthSeconds = Song.length;
//		print("Song length: " + _songLengthSeconds);
		
		_mappedInputs = inputFileHandler.MappingsDict;

		_worldDistanceBetweenRows =
			ReferenceArrow.transform.position.y - ReferenceDistance.transform.position.y;
	
		//World distance is used because the local distance is not changed when the songbook is resized.
		_rowDistance = (EndLine.transform.position.x - Cursor.transform.position.x);

		_inputTimeDelta = BeatsPerDownBeat - (Bpm / 60);
		_downBeatDelta = _inputTimeDelta / _songLengthSeconds;
//		print(_inputTimeDelta);
		
		_totalDistance =  _rowDistance / _downBeatDelta;
//		print(_totalDistance);

		// (Average Velocity = Total Distance / Total Time)
		_cursorVelocity = _totalDistance / _songLengthSeconds;
		_cursorBody = Cursor.GetComponent<Rigidbody2D>();	
	
		//Create all the input arrows
		foreach (var item in _mappedInputs) {
			CreateArrow(item.Key, item.Value);
		}
		
		_gameManager.setMaxScore(_mappedInputs.Count);

		
		//Hide the reference arrow
		ReferenceArrow.gameObject.SetActive(false);
		ReferenceDistance.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (_gameManager.GameIsActive == false) return;
			
		//Move cursor to the right using the cursors velocity
		Cursor.transform.Translate(Vector3.right * _cursorVelocity * Time.deltaTime);

		if (Cursor.transform.position.x >= EndLine.transform.position.x + _offset) {
			
			//Move cursor to the start
			_cursorBody.transform.Translate(Vector3.left * _rowDistance);

			//Move all arrows one line up
			foreach (var arrow in _arrows) {
				arrow.transform.Translate(Vector3.up * _worldDistanceBetweenRows, ReferenceArrow.transform);

				if (arrow.transform.position.y < TopLine.transform.position.y &&
				    arrow.transform.position.y > BottomLine.transform.position.y) {
					arrow.gameObject.SetActive(true);
				}
				else {
					arrow.gameObject.SetActive(false);
				}
			}
		}

		if (Time.time - _startTime >= _songLengthSeconds) {
			_gameManager.gameOver();
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
		var xVector = Vector3.right * ((arrowDistanceFromStart % _rowDistance) + _offset);
		
		//Arrow y position row number * padding
		var yVector = Vector3.down * rowToPlaceArrow * _worldDistanceBetweenRows;
		
		var arrow = Instantiate(ReferenceArrow, transform);
		arrow.GetComponent<Arrow>().Init(_gameManager);
		
		var body = arrow.GetComponent<Rigidbody2D>();
		
		body.transform.Translate(xVector, Cursor.transform); //Move new arrow right relative to cursor start pos
		body.transform.Translate(yVector, ReferenceArrow.transform); //Move new arrow down relative to the reference

		arrow.gameObject.SetActive(false);


		switch (directionCharacter) {

			case 'W':
				arrow.GetComponent<Arrow>().direction = Direction.Up;
				arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.UpArrow;
				arrow.GetComponent<SpriteRenderer>().sprite = spriteLeft;
				break;
			case 'A':
				arrow.GetComponent<Arrow>().direction = Direction.Left;
				arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.LeftArrow;
				arrow.GetComponent<SpriteRenderer>().sprite = spriteUp;
				break;
			case 'S':
				arrow.GetComponent<Arrow>().direction = Direction.Down;
				arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.DownArrow;
				arrow.GetComponent<SpriteRenderer>().sprite = spriteRight;
				break;
			case 'D':
				arrow.GetComponent<Arrow>().direction = Direction.Right;
				arrow.GetComponent<Arrow>().CorrectKeyCode = KeyCode.RightArrow;
				arrow.GetComponent<SpriteRenderer>().sprite = spriteDown;
				break;
			default:
				//Debug.Log("Unknown direction character");
				arrow.SetActive(false);
				break;
		}		

		if (arrow.transform.position.y >= BottomLine.transform.position.y) {
			arrow.gameObject.SetActive(true);
		}
		

		_arrows.Add(arrow);
	}

}
