using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameEndScript : MonoBehaviour
{

    [SerializeField]
    private GameObject _uiGameEndScreen;

    [SerializeField]
    private StarterAssetsInputs _startAssetsInputs;

    [SerializeField]
    private AudioMixer _audioMixer;

    private float _cachedMasterVolume = 0;

    private VideoPlayer _videoPlayer;
    private void Start()
    {
        _videoPlayer = gameObject.GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += OnVideoEnd;
    }

   

    private void OnEnable()
    {
        ExitScript.OnPlayerExit += ExecuteGameEnd;
        HealthController.OnHealthIsZero += ExecuteDefeat;
    }

    

    private void OnDisable()
    {

        ExitScript.OnPlayerExit -= ExecuteGameEnd;
        HealthController.OnHealthIsZero -= ExecuteDefeat;
    }

    private void ExecuteGameEnd()
    {
       
        Cursor.lockState = CursorLockMode.None;
        _audioMixer.GetFloat("masterVolume", out _cachedMasterVolume);
        _videoPlayer.SetDirectAudioVolume(0, Mathf.Pow((float)Math.E, _cachedMasterVolume / 20));
        _audioMixer.SetFloat("masterVolume", -80);
        Time.timeScale = 0;
        
        _videoPlayer.Play();
        
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        
        source.enabled = false;
        _audioMixer.SetFloat("masterVolume", _cachedMasterVolume);
        Cursor.lockState = CursorLockMode.Locked;
        _uiGameEndScreen.SetActive(true);
    }

    private void ExecuteDefeat()
    {
        _uiGameEndScreen.SetActive(true);
    }
}
