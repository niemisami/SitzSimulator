using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuScript : MonoBehaviour {

    public GameObject MenuScreen;
    public GameObject OptionsScreen;
    public Slider volumeSlider;
    private void Start()
    {
        MenuScreen.SetActive(true);
        OptionsScreen.SetActive(false);
    }

    public void playGame()
    {
        SceneManager.LoadScene("ITGD");
    }
    public void goToOptions()
    {
        MenuScreen.SetActive(false);
        OptionsScreen.SetActive(true);

    }
    public void BackToMenu()
    {
        MenuScreen.SetActive(true);
        OptionsScreen.SetActive(false);
    }
    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
