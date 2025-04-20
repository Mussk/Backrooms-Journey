using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseWindowUI;

    [SerializeField]
    private GameObject settingsWindowUI;

    private void Update()
    {
        
    }

    public void OnPause(InputValue value) 
    {

        TogglePause();
        
       
    }
    private void TogglePause()
    {   
        if (settingsWindowUI.activeSelf)
        { 
        
            settingsWindowUI.SetActive(false);
            return;

        }
        if (pauseWindowUI.activeSelf) 
        {
            pauseWindowUI.SetActive(false);
        }
        else 
        { 
        
            pauseWindowUI.SetActive(true);
        
        }
        

    }

}
