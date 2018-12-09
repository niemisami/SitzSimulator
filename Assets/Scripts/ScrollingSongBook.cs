using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingSongBook : MonoBehaviour {

	public AudioClip Song;
	public GameObject Cursor;
	public GameObject ReferenceArrow;
	public GameObject EndLine;
	public int TotalDistance = 100;

	private float _cursorVelocity;
	private float _rowDistance;
	private float _songLengthSeconds;
	private List<GameObject> _arrows = new List<GameObject>();
	private Dictionary<float, char> _mappedInputs;
	private const float LocalDistanceBetweenRows = 1.28f;


	// Use this for initialization
	void Start () {
		
		var inputFileHandler = new InputFileHandler("Internationalen.txt");
		_songLengthSeconds = Song.length;
		print("Song length: " + _songLengthSeconds);
		
		_mappedInputs = inputFileHandler.MappingsDict;
		
		// (Average Velocity = Total Distance / Total Time)
		_cursorVelocity = TotalDistance / _songLengthSeconds;
		
		//World distance is needed because the local distance is not changed when the songbook is resized.
		_rowDistance = (EndLine.transform.position.x - Cursor.transform.position.x);
	
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
		
		//Move cursor to the right using the cursors velocity
		Cursor.transform.Translate(Vector3.right * _cursorVelocity * Time.deltaTime);

		if (Cursor.transform.position.x >= EndLine.transform.position.x) {
			
			//Move cursor to the start
			Cursor.transform.Translate(Vector3.left * _rowDistance);

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
		var arrowDistanceFromStart = relativeSongPosition * TotalDistance;
		
		var rowToPlaceArrow = (int)(arrowDistanceFromStart / _rowDistance);
		
		//Arrow x position
		var xVector = Vector3.right * (arrowDistanceFromStart % _rowDistance);
		
		//Arrow y position
		var yVector = Vector3.down * rowToPlaceArrow * LocalDistanceBetweenRows;
				
		var arrow = Instantiate(ReferenceArrow, transform);
		
		arrow.transform.Translate(xVector, Cursor.transform); //Move new arrow right relative to cursor start pos
		arrow.transform.Translate(yVector, ReferenceArrow.transform); //Move new arrow down relative to the reference
		
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
