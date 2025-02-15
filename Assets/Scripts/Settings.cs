﻿using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterAudioSlider;
    [SerializeField] private Slider sfxAudioSlider;
    [SerializeField] private Slider musicAudioSlider;
    [SerializeField] private Slider uiAudioSlider;

    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private TMP_Dropdown resolution;

    private void MasterSliderChanged(float value)
    {
        audioMixer.SetFloat("MasterVolume", value);
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        masterAudioSlider.onValueChanged.AddListener(MasterSliderChanged);
        sfxAudioSlider.onValueChanged.AddListener(SfxSliderChanged);
        musicAudioSlider.onValueChanged.AddListener(MusicSliderChanged);
        uiAudioSlider.onValueChanged.AddListener(UISliderChanged);
        
        fullScreenToggle.onValueChanged.AddListener(FullScreenToggle);
        resolution.onValueChanged.AddListener(ResolutionChanged);
        
        masterAudioSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
        sfxAudioSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        musicAudioSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        uiAudioSlider.value = PlayerPrefs.GetFloat("UIVolume", 1);

        fullScreenToggle.isOn = PlayerPrefs.GetInt("FullScreen") == 1;
        resolution.value = PlayerPrefs.GetInt("ResolutionIndex");
    }

    private void SfxSliderChanged(float value)
    {
        audioMixer.SetFloat("SFXVolume", value);
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }

    private void MusicSliderChanged(float value)
    {
        audioMixer.SetFloat("MusicVolume", value);
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }
    
    private void UISliderChanged(float value)
    {
        audioMixer.SetFloat("UIVolume", value);
        PlayerPrefs.SetFloat("UIVolume", value);
        PlayerPrefs.Save();
    }

    private static void FullScreenToggle(bool value)
    {
        Screen.fullScreen = value;
        PlayerPrefs.SetInt("FullScreen", value ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void ResolutionChanged(int index)
    {
        var option = resolution.options[index].text;
        var w = int.Parse(option.Split('x')[0]);
        var h = int.Parse(option.Split('x')[1]);
        Screen.SetResolution(w, h, fullScreenToggle.isOn);
        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.SetInt("ResolutionWidth", w);
        PlayerPrefs.SetInt("ResolutionHeight", h);
        PlayerPrefs.Save();
    }
}