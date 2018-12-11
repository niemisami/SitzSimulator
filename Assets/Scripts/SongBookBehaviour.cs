using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongBookBehaviour : MonoBehaviour {

    //Audio variables for metronome
    public Button playSongButton;
    public AudioClip audioClipMetronomeSound;
    public float volume = 1;
    private AudioSource audioSource;

    //Song timer variables
    public InputField bpmInput;
    private Vector3 positionVector;
    private Vector3 rowPositionVector;
    private Vector3 StartPositionVector;
    private Vector3 padding = new Vector3(0F, 1.5F, 0F);
    private Vector3 rowPadding = new Vector3(0F, 1.22F, 0F);
    private Vector3 horisontalSpeedVector;
    private float bpm = 124;
    private float horisontalSpeed;
    private float bars = 4;
    private float rows = 11;
    private float rowCounter = 0;
    private int counterTime=1;
    private int counterTimeMetronome = 1;
    private float startTime;
    private float elapsedTime;

    private Vector3 startPositionTemp;

    // Use this for initialization
    void Start () {
        //initialization for input field
        var se = new InputField.SubmitEvent();
        se.AddListener(setBPM);
        bpmInput.onEndEdit = se;

        //initialization for button
        var bce = new Button.ButtonClickedEvent();
        bce.AddListener(playSong);
        playSongButton.onClick = bce;

        audioSource = GetComponent<AudioSource>();
        startTime = Time.time;

        //Sets the starting position of the song timer
        StartPositionVector = transform.position;//GameObject.Find("Song book").transform.position;
        positionVector = StartPositionVector;
        rowPositionVector = StartPositionVector;
    }
	// Update is called once per frame
	void Update () {


        if (GameManager.instance.GameIsActive == false)//resets the time while count down and resets position
        {
            elapsedTime = 0;
            startTime = Time.time;
            StartPositionVector = transform.position;// GameObject.Find("Song book").transform.position - padding;
            positionVector = StartPositionVector;
            rowPositionVector = StartPositionVector;
            startPositionTemp = GameObject.Find("Song book").transform.position;

        }

        if (GameManager.instance.GameIsActive == true)
        {
            if (elapsedTime == 0)//plays the first metronome click and starts helan går
            {
                audioSource.PlayOneShot(audioClipMetronomeSound, volume);
                AudioScript.playSong();//Calls a static funtion in AudioScript that plays the selected song
            }

            horisontalSpeed = 10 * ((bpm / bars) / 60) * (Time.deltaTime);
            horisontalSpeedVector = new Vector3(horisontalSpeed, 0F, 0F);
            elapsedTime = Time.time - startTime;

            if (elapsedTime / counterTimeMetronome >= ((60) / (bpm)))//metronome sound every beat
            {
                //print("Tick");
                counterTimeMetronome++;
                audioSource.PlayOneShot(audioClipMetronomeSound, volume);
            }


            if (rowCounter >= rows && (elapsedTime / counterTime >= ((60) / (bpm / bars))))//move the song timer to the top
            {
                counterTime++;
                rowCounter = 0;
                rowPositionVector = StartPositionVector;
                positionVector = rowPositionVector;
                MoveHorisontal();
            }
            else if (elapsedTime / counterTime >= ((60) / (bpm / bars)))//move to next row
            {
                counterTime++;
                rowPositionVector = rowPositionVector - rowPadding;
                positionVector = rowPositionVector;
                MoveHorisontal();
                rowCounter++;
            }
            else//move horisontal
            {
                MoveHorisontal();
            }
            

        }

        
    }
    private void MoveHorisontal()
    {
        positionVector = positionVector + horisontalSpeedVector;
        transform.position = positionVector + GameObject.Find("Song book").transform.position - startPositionTemp;
    }

    
    private void setBPM(string s)//is called when BPM input field is changed
    {
        bpm = float.Parse(s);
        positionVector = StartPositionVector;
        rowPositionVector = StartPositionVector;
        rowCounter = 0;
        counterTime = 1;
        startTime = Time.time;
        elapsedTime = 0;
        counterTimeMetronome = 1;
    }

    private void playSong()//Is called when play song button is pressed
    {
        positionVector = StartPositionVector;
        rowPositionVector = StartPositionVector;
        rowCounter = 0;
        counterTime = 1;
        startTime = Time.time;
        elapsedTime = 0;
        counterTimeMetronome = 1;
        AudioScript.playSong();//Calls a static funtion in AudioScript that plays the selected song
    }

    public Vector2 getSongTimer() 
    {
        return positionVector;
    }
    
}
