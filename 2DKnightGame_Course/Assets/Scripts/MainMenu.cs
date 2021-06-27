using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _optionsWindow;
    
    private void Start()
    {
        Time.timeScale = 1.0f;
        GameController.Instance.AudioManager.PlayMusic(true);
        _optionsWindow.SetActive(false);
    }

    public void PlayButton() 
    {
        GameController.Instance.AudioManager.PlaySound("Select_2");
        GameController.Instance.AudioManager.PlayMusic(false);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OptionsButton() 
    {
        _optionsWindow.SetActive(true);
        _optionsWindow.GetComponent<Animator>().SetBool("Open", true);
    }

    public void CloseOptionsWindow() 
    {
        _optionsWindow.GetComponent<Animator>().SetBool("Open", false);
        _optionsWindow.SetActive(false);
    }

    public void ExitButton() 
    {
        Application.Quit();
    }

    public void SetSoundVolume(Slider slider)
    {
        GameController.Instance.AudioManager.SFXVolume = slider.value;
    }

    public void SetMusicVolume(Slider slider)
    {
        GameController.Instance.AudioManager.MusicVolume = slider.value;
    }
}
