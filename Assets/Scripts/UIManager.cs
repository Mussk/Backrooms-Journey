using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField]
    private StarterAssetsInputs PlayerStarterAssetsInputs;
    [SerializeField]
    private string PlayerTag;
    [SerializeField]
    private Volume DoFVolume;
    [SerializeField]
    private string DoFVolumeTag;

      public static UIManager Instance
      {

          get
          {
              if (_instance == null)
              {

                  _instance = FindObjectOfType<UIManager>();

                 /*   if (_instance == null) 
                     {

                         GameObject singletonObject = new GameObject();
                         _instance = singletonObject.AddComponent<GameManager>();
                         singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";

                     }*/

              }
              return _instance;
          }

}

    public static bool IsInitialized => _instance != null;

    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void OnEnable()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
        UIElement.OnUiElementBecameActive += UIElement_OnUiElementBecameActive;
        UIElement.OnUiElementBecameInactive += UIElement_OnUiElementBecameInactive;
    }

 
    private void OnDisable()
    {

          SceneManager.sceneLoaded -= OnSceneLoaded;
          UIElement.OnUiElementBecameActive -= UIElement_OnUiElementBecameActive;
          UIElement.OnUiElementBecameInactive -= UIElement_OnUiElementBecameInactive;
        
        
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        

        if (arg0.name.Equals("GameScene"))
        {
            PlayerStarterAssetsInputs = GameObject.FindGameObjectWithTag(PlayerTag)?.GetComponent<StarterAssetsInputs>();
            DoFVolume = GameObject.FindGameObjectWithTag(DoFVolumeTag)?.GetComponent<Volume>();

            if (DoFVolume != null)
            {
                DoFVolume.gameObject.SetActive(false);
            }
        }
        else
        {

            Time.timeScale = 1;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (PlayerStarterAssetsInputs != null)
            {
                PlayerStarterAssetsInputs.cursorLocked = false;
                PlayerStarterAssetsInputs.cursorInputForLook = false;
            }


        }
         
    }

    private void UIElement_OnUiElementBecameActive()
    {
        DoFVolume.gameObject.SetActive(true);
        PlayerStarterAssetsInputs.cursorLocked = false;
        PlayerStarterAssetsInputs.cursorInputForLook = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        
    }

    private void UIElement_OnUiElementBecameInactive()
    {
        DoFVolume.gameObject.SetActive(false);
        PlayerStarterAssetsInputs.cursorLocked = true;
        PlayerStarterAssetsInputs.cursorInputForLook = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
