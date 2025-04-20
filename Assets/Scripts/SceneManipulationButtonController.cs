using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManipulationButtonController : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoadName;

    [SerializeField]
    private string sceneToReloadName;

    [SerializeField]
    private StarterAssetsInputs PlayerStarterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
        Button thisButton;
        if(!TryGetComponent<Button>(out thisButton))
        {
            Debug.Log("This script is attached to wrong object!");
        }
        thisButton.onClick.AddListener(delegate { LoadAnotherScene(sceneToLoadName); });

    }

    private void LoadAnotherScene(string sceneName)
    {
        if (sceneName.Equals("MainMenuScene"))
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

        SceneManager.LoadScene(sceneName);
    }

    
}
