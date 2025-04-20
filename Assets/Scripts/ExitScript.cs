using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ExitScript : MonoBehaviour
{

    [SerializeField]
    public bool IsExitAvaliable = false;

    public delegate void PlayerExit();
    public static event PlayerExit OnPlayerExit;

    
    private void OnEnable()
    {
        GameManager.Instance.OnCollectiblesCountMaxReach += OpenExit;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCollectiblesCountMaxReach -= OpenExit;
        }
    }

    private void OpenExit()
    {
        IsExitAvaliable = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && IsExitAvaliable) 
        {

            OnPlayerExit?.Invoke();

        }
    }
}
