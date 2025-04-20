using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Audio;

public class VolumeSettingsController : MonoBehaviour
{
    [SerializeField]
    private Slider masterVolumeSlider;


    [SerializeField]
    private Slider musicVolumeSlider;

  
    [SerializeField]
    private Slider sfxVolumeSlider;

    
    [SerializeField]
    private AudioMixer audioMixer;


    void Awake() 
    {

        masterVolumeSlider.onValueChanged.AddListener(delegate { ChangeVolumeBySlider("masterVolume", masterVolumeSlider.value); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { ChangeVolumeBySlider("musicVolume", musicVolumeSlider.value); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { ChangeVolumeBySlider("sfxVolume", sfxVolumeSlider.value); });     
    }

   
    private void OnEnable()
    {
        
        float masterVolume;
        float musicVolume;
        float sfxVolume;

        audioMixer.GetFloat("masterVolume", out masterVolume);
        audioMixer.GetFloat("musicVolume", out musicVolume);
        audioMixer.GetFloat("sfxVolume", out sfxVolume);

       
        masterVolumeSlider.value = Mathf.Pow((float)Math.E, masterVolume / 20);
        musicVolumeSlider.value = Mathf.Pow((float)Math.E, musicVolume / 20);
        sfxVolumeSlider.value = Mathf.Pow((float)Math.E, sfxVolume / 20);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeVolumeBySlider(string channelName, float sliderValue)
    {

        audioMixer.SetFloat(channelName, Mathf.Log(sliderValue) * 20);

    }

}
