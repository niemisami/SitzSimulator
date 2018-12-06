using System;
using UnityEngine;
using System.Collections;

public enum Direction {

	Left = 0,
	Up = -90,
	Right = 180,
	Down = 90

};

public class Arrow : MonoBehaviour {

	public Direction Direction;
	public Vector2 Position;
	public KeyCode CorrectKeyCode;

	private Animator _anim;
	private GameManagerScript _gms;

	private bool _isActive = false;
	private bool _isSuccess = false;

	private float _startTime;

	void Start() {
		_anim = GetComponent<Animator>();
		transform.rotation = Quaternion.Euler(0, 0, (float) Direction);
		_gms = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
		_startTime = Time.time;

	}

	void Update() {
		if (_gms.GameIsActive != true || _isActive == false) {
			return;
		}

	}

	void OnTriggerEnter2D(Collider2D col) {
		_isActive = true;
//		Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + (Time.time - _startTime));

	}

	private void OnTriggerStay2D(Collider2D other) {

		if (Input.GetKeyDown(CorrectKeyCode)) {

			_isSuccess = true;
			_anim.SetTrigger("success");
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if (!_isSuccess) {
			_anim.SetTrigger("fail");
		}

//		Debug.Log("EXIT" + col.gameObject.name + " : " + gameObject.name + " : " + (Time.time - _startTime));
	}

}