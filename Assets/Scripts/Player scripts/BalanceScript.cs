using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BalanceScript : MonoBehaviour {

	public Text BalanceText;
	public Slider BalanceSlider;
	public Slider CenterPointSlider;

	public float PlayerSpeed;
	public float OscillationSpeed;
	public float ScaledDegree; //scales 0-90 to 0-1
	public float RotationDegree;
	private float _fallingVelocity = 4f;

	//Constants
	private const float RotationBoundary = 90; //Maximum rotation in degrees
	private const float MaxSpeed = 3F;
	private const float MinSpeed = 0.5F;
	private const float Slope = 0.25F; //Adjusts the gravity slope, higher is steeper
	private const float CorrectionSpeed = 0.5F; //Adjusts players ability to correct 
	private const float OscFrequency = 2; //Character oscillation frequency

	private PlayerFallScript PFS;
	private GameManager _gameManager;
	private bool _isFalling = false;


	public void Init(GameManager injected) {
		_gameManager = injected;
	}

	// Use this for initialization
	void Start() {

		BalanceSlider.value = 0;
		CenterPointSlider.value = 0;
		RotationDegree = 0;

	}

	// Update is called once per frame
	// TODO: These updates should be independent of frame rate
	void FixedUpdate() {
		if (!_gameManager.GameIsActive) return;

		if (_isFalling) {
			transform.localPosition += Vector3.down * _fallingVelocity * Time.deltaTime;

			RotationDegree += 2 * Math.Sign(RotationDegree);
			transform.rotation = Quaternion.Euler(0, 0, -RotationDegree);

			if (Math.Abs(transform.parent.position.y - transform.position.y) > 10) {
				_gameManager.gameOver();
			}	
			return;
		}

		ScaledDegree = RotationDegree * (0.011F); //scales 0-90 to 0-1

		//PlayerSpeed should change according to the distance from center. Formula: slope * x^2 + minSpeed
		PlayerSpeed = Slope * Mathf.Pow(ScaledDegree, 2) + MinSpeed;

		//Oscillation should be a sinusoid, time.deltaTime did not work
		OscillationSpeed = Mathf.Sin(OscFrequency * Time.time * Mathf.PI);

		//Apply the OscillationSpeed
		RotationDegree += OscillationSpeed;

		//Update the rotation by adding PlayerSpeed
		if (RotationDegree < 0) RotationDegree -= PlayerSpeed;
		if (RotationDegree >= 0) RotationDegree += PlayerSpeed;


		//Apply CorrectionSpeed according to input key
		if (Input.GetKey(KeyCode.D)) RotationDegree += PlayerSpeed + CorrectionSpeed;
		if (Input.GetKey(KeyCode.A)) RotationDegree -= PlayerSpeed + CorrectionSpeed;


		//Keep rotation within limits
		if (PlayerSpeed > MaxSpeed) PlayerSpeed = MaxSpeed;

		if (Math.Abs(RotationDegree) >= RotationBoundary) {
			_isFalling = true;

		}

		//Update the transform
		transform.rotation = Quaternion.Euler(0, 0, -RotationDegree);
		BalanceSlider.value = RotationDegree;
		BalanceText.text = PlayerSpeed.ToString();

	}

}