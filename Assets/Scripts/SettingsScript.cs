using System;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    private GameObject content; 
    private Slider effectsVolumeSlider;
    private Slider ambientVolumeSlider;
    private Slider musicVolumeSlider;
    private Slider sensXSlider;
    private Slider sensYSlider;



    private Toggle muteAllToggle;

    private float defaultAmbientVolume;
    private float defaultEffectsVolume;
    private float defaultMusicVolume;
    private float defaultSensX;
    private float defaultSensY;
    void Start()
    {
        Transform contentTransform = transform.Find("Content");
        content = contentTransform.gameObject;

        effectsVolumeSlider = contentTransform.Find("Effects").GetComponent<Slider>();
        defaultEffectsVolume = effectsVolumeSlider.value;

        ambientVolumeSlider = contentTransform.Find("Ambient").GetComponent<Slider>();
        defaultAmbientVolume = ambientVolumeSlider.value;

        musicVolumeSlider = contentTransform.Find("Music").GetComponent<Slider>();
        defaultMusicVolume = musicVolumeSlider.value;

        sensXSlider = contentTransform.Find("SliderX").GetComponent<Slider>();
        defaultSensX = sensXSlider.value;

        sensYSlider = contentTransform.Find("SliderY").GetComponent<Slider>();
        defaultSensY = sensYSlider.value;   

        muteAllToggle = contentTransform.Find("Toggle").GetComponent<Toggle>();


        GameState.isMuted = muteAllToggle.isOn;


        //Try Restore Saved
        if (PlayerPrefs.HasKey(nameof(GameState.ambientVolume)))
        {
            GameState.ambientVolume = PlayerPrefs.GetFloat(nameof(GameState.ambientVolume));
            ambientVolumeSlider.value = GameState.ambientVolume;
        }
        else
        {
            GameState.ambientVolume = ambientVolumeSlider.value;
        }

        if (PlayerPrefs.HasKey(nameof(GameState.effectsVolume)))
        {
            GameState.effectsVolume = PlayerPrefs.GetFloat(nameof(GameState.effectsVolume));
            effectsVolumeSlider.value = GameState.effectsVolume;
        }
        else
        {
            GameState.effectsVolume = effectsVolumeSlider.value;
        }

        if (PlayerPrefs.HasKey(nameof(GameState.musicVolume)))
        {
            GameState.musicVolume = PlayerPrefs.GetFloat(nameof(GameState.musicVolume));
            musicVolumeSlider.value = GameState.musicVolume;
        }
        else
        {
            GameState.musicVolume = musicVolumeSlider.value;
        }

        if (PlayerPrefs.HasKey(nameof(GameState.sensitivityX)))
        {
            GameState.sensitivityX = PlayerPrefs.GetFloat(nameof(GameState.sensitivityX));
            sensXSlider.value = GameState.sensitivityX;
        }
        else
        {
            GameState.sensitivityX = sensXSlider.value;
        }

        if (PlayerPrefs.HasKey(nameof(GameState.sensitivityY)))
        {
            GameState.sensitivityY = PlayerPrefs.GetFloat(nameof(GameState.sensitivityY));
            sensYSlider.value = GameState.sensitivityY;
        }
        else
        {
            GameState.sensitivityX = sensXSlider.value;
        }

        if (PlayerPrefs.HasKey(nameof(GameState.isMuted)))
        {
            GameState.isMuted = PlayerPrefs.GetInt(nameof(GameState.isMuted)) == 1;
            muteAllToggle.isOn = GameState.isMuted;
        }
        else
        {
            GameState.isMuted = muteAllToggle.isOn;
        }


        Time.timeScale = content.activeInHierarchy ? 0.0f : 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CloseSettings();

        }
    }

    public void CloseSettings()
    {

        Time.timeScale = content.activeInHierarchy ? 1.0f : 0.0f;
        content.SetActive(!content.activeInHierarchy);
    }

    public void OnDefaultClick()
    {
        ambientVolumeSlider.value = defaultAmbientVolume;
        musicVolumeSlider.value = defaultMusicVolume;
        effectsVolumeSlider.value = defaultEffectsVolume;
        sensXSlider.value = defaultSensX;
        sensYSlider.value = defaultSensY;
    }

    public void OnExitClick()
    {
        CloseSettings();
    }

    public void OnSaveButtonClick()
    {
        PlayerPrefs.SetFloat(nameof(GameState.ambientVolume), GameState.ambientVolume);
        PlayerPrefs.SetFloat(nameof(GameState.effectsVolume), GameState.effectsVolume);
        PlayerPrefs.SetFloat(nameof(GameState.musicVolume), GameState.musicVolume);
        PlayerPrefs.SetFloat(nameof(GameState.sensitivityX), GameState.sensitivityX);
        PlayerPrefs.SetFloat(nameof(GameState.sensitivityY), GameState.sensitivityY);
        PlayerPrefs.SetInt(nameof(GameState.isMuted), GameState.isMuted ? 1 : 0);

        PlayerPrefs.Save();
        CloseSettings();
    }
    public void OnEffectsVolumeChanged(Single value)
    {
        GameState.effectsVolume = value;    
    }
    public void OnAmbientChanged(Single value)
    {
        GameState.ambientVolume = value;
    }
    public void OnMusicChanged(Single value)
    {
        GameState.musicVolume = value;
    }
    public void OnMuteAllChanged(bool value)
    {
        GameState.isMuted = value;
    }
    public void OnSensXChanged(Single value)
    {
        GameState.sensitivityX = value;
    }
    public void OnSensYChanged(Single value)
    {
        GameState.sensitivityY = value;
    }
}
