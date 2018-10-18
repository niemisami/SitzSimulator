using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongBookBehaviour : MonoBehaviour {

    //Audio variables, all audio stuff should be made into an own class... 
    public List<AudioClip> clips;
    public Button playSongButton;
    public Dropdown dropdownSongList;
    public AudioClip audioClipMetronomeSound;
    public AudioClip audioClipSong;
    public float volume = 1;
    private AudioSource audioSource;

    //Song timer variables
    public InputField bpmInput;
    private Vector3 positionVector;
    private Vector3 rowPositionVector;
    private Vector3 StartPositionVector;
    private Vector3 padding = new Vector3(0F, 0.2F, 0F);
    private Vector3 rowPadding = new Vector3(0F, 0.4F, 0F);
    private Vector3 horisontalSpeedVector;
    private float bpm = 80;
    private float horisontalSpeed;
    private float bars = 4;
    private float rows = 24;
    private float rowCounter = 0;
    private int counterTime=1;
    private int counterTimeMetronome = 1;
    private float startTime;
    private float elapsedTime;

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

        //initialization for dropdown menu
        var de = new Dropdown.DropdownEvent();
        de.AddListener(selectSong);
        dropdownSongList.onValueChanged = de;
        dropdownSongList.ClearOptions();

        //Adds the names of the songs to the dropdown menu
        List<string> clipsString = new List<string>();
        foreach (AudioClip clip in clips)
        {
            clipsString.Add(clip.name);
        }
        dropdownSongList.AddOptions(clipsString);
        audioSource = GetComponent<AudioSource>();
        selectSong(0);

        startTime = Time.time;



        //Sets the starting position of the song timer
        StartPositionVector = GameObject.Find("Song book").transform.position - padding;
        positionVector = StartPositionVector;
        rowPositionVector = StartPositionVector;
    }
	

	// Update is called once per frame
	void Update () {

        elapsedTime = Time.time - startTime;
        horisontalSpeed = 10*((bpm / bars) / 60) * (Time.deltaTime);
        horisontalSpeedVector = new Vector3(horisontalSpeed, 0F, 0F);

        if (elapsedTime  / counterTimeMetronome  >= ((60) / (bpm)))//metronome sound every beat
        {
            print("Tick");
            counterTimeMetronome++;
            audioSource.PlayOneShot(audioClipMetronomeSound, volume);
        }

        if (rowCounter >= rows && (elapsedTime / counterTime >= ((60) / (bpm / bars))))//move the song timer to the top
            {
            counterTime++;
            rowCounter = 0;
            rowPositionVector = StartPositionVector;
            positionVector = rowPositionVector;
            positionVector = positionVector + horisontalSpeedVector;
            transform.position = positionVector;
        }     
        else if (elapsedTime / counterTime >= ((60) / (bpm / bars)))//move to next row
        {
            counterTime++;
            rowPositionVector = rowPositionVector - rowPadding;
            positionVector = rowPositionVector;
            positionVector = positionVector + horisontalSpeedVector;
            transform.position = positionVector;
            rowCounter++;
        }
        else//move horisontal
        {
            positionVector = positionVector + horisontalSpeedVector;
            transform.position = positionVector;
        }
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
        audioSource.Stop();
        audioSource.PlayOneShot(audioClipSong, volume);
        positionVector = StartPositionVector;
        rowPositionVector = StartPositionVector;
        rowCounter = 0;
        counterTime = 1;
        startTime = Time.time;
        elapsedTime = 0;
        counterTimeMetronome = 1;
    }
    private void selectSong(int i)//Is calles when a song is selected from dropdown menu
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == dropdownSongList.options[dropdownSongList.value].text)
                audioClipSong = clip;
        }
    }
}
