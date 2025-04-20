using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    

    public static GameManager Instance 
    { 
    
        get 
        { 
            if (_instance == null) 
            {

                _instance = FindObjectOfType<GameManager>();

             /*  if (_instance == null) 
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

    public delegate void CollectiblesCountChanged(int currentCollectiblesAmount);
    public event CollectiblesCountChanged OnCollectiblesCountChanged;

    public delegate void CollectiblesCountMaxReach();
    public event CollectiblesCountMaxReach OnCollectiblesCountMaxReach;

  
    [SerializeField]
    private int _maxCollectiblesCount;

    public int MaxCollectiblesCount => _maxCollectiblesCount;

    private int _currentCollectiblesCount;

    public int CurrentCollectiblesCount 
    { 
    
        get 
        { 
            return _currentCollectiblesCount; 
        
        }
        set 
        { 
            _currentCollectiblesCount = value;
            OnCollectiblesCountChanged?.Invoke(_currentCollectiblesCount);
            
            if(CurrentCollectiblesCount == _maxCollectiblesCount) 
            {

                OnCollectiblesCountMaxReach?.Invoke();
            
            }

        }
    
    }


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
    }

   

    private void OnDisable()
    {

       
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
       
        InitNewGame();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void InitNewGame()
    {

        Time.timeScale = 1;
        Cursor.visible = false;
        CurrentCollectiblesCount = 0;
    }

    
}
