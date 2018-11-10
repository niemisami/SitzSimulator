using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour {

    public List<AudioClip> clips;
    public Dropdown dropdownSongList;
    public static AudioClip audioClipSong;
    public static float volume = 1;
    public static AudioSource audioSource;
    public static bool songIsPlaying = false;
    private GameManagerScript GMS;
    public GameObject endGameScreen;

    // Use this for initialization
    void Start () {
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

        endGameScreen.SetActive(false);


    }

    private void Update()
    {

        if (!audioSource.isPlaying && songIsPlaying )//Pauses game after song has ended
        {
            GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
            GMS.GameIsActive = false;
            print("Game paused because song has ended");
            songIsPlaying = false;
            endGameScreen.SetActive(true);
        }

    }

    public static void playSong()//plays the song
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClipSong, volume);
        songIsPlaying = true;
    }

    public void selectSong(int i)//Is calles when a song is selected from dropdown menu
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == dropdownSongList.options[dropdownSongList.value].text)
                audioClipSong = clip;
        }
    }
}
