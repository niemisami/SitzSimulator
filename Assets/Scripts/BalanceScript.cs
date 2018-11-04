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
	public float ScaledDegree;					//scales 0-90 to 0-1
	public float RotationDegree;

	//Constants
	private const float RotationBoundary = 90;	//Maximum rotation in degrees
	private const float MaxSpeed = 0.2F;
	private const float MinSpeed = 0.1F;
	private const float Slope = 0.25F; 			//Adjusts the gravity slope, higher is steeper
	private const float CorrectionSpeed = 1F; //Adjusts players ability to correct 
	private const float OscFrequency = 4;		//Character oscillation frequency

	// Use this for initialization
	void Start () {

		//Chair should not rotate
		transform.DetachChildren();
		
		BalanceSlider.value = 0;
		CenterPointSlider.value = 0;
		RotationDegree = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

		ScaledDegree = RotationDegree * (0.011F);	//scales 0-90 to 0-1

		//PlayerSpeed should change according to the distance from center. Formula: slope * x^2 + minSpeed
		PlayerSpeed = Slope * Mathf.Pow(ScaledDegree, 2) + MinSpeed;
		
		//Oscillation should be a sinusoid, time.deltaTime did not work
		OscillationSpeed = Mathf.Sin(OscFrequency * Time.time);

		//Apply the OscillationSpeed
		RotationDegree += OscillationSpeed;
		
		//Update the rotation by adding PlayerSpeed
		if (RotationDegree < 0) RotationDegree -= PlayerSpeed;		
		if (RotationDegree >= 0) RotationDegree += PlayerSpeed;
		
		
		//Apply CorrectionSpeed according to input key
		if (Input.GetKey(KeyCode.D)) RotationDegree +=  PlayerSpeed + CorrectionSpeed;			
		if (Input.GetKey(KeyCode.A)) RotationDegree -= PlayerSpeed + CorrectionSpeed;
		

		//Keep rotation within limits
		if (PlayerSpeed >= MaxSpeed) PlayerSpeed = MaxSpeed;
		if (RotationDegree >= RotationBoundary) RotationDegree = RotationBoundary;
		if (RotationDegree <= -RotationBoundary) RotationDegree = -RotationBoundary;
		
		//Update the transform
		transform.rotation = Quaternion.Euler(0, 0, -RotationDegree*0.5f);
		BalanceSlider.value = RotationDegree;
		BalanceText.text = PlayerSpeed.ToString();

	}
}
