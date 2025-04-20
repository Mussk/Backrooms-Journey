using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexItem : MonoBehaviour
{
    
    public CodexEntryDataSO CodexEntryData;

    private GameObject _codexUI;

    [SerializeField]
    private string _codexUITag;

    [SerializeField]
    private string _playerTag;

    private void Start()
    {
        _codexUI = GameObject.FindGameObjectWithTag(_codexUITag);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
           
            CodexUIElement codexUIElement = _codexUI.GetComponent<CodexUI>().ChildCodexUI.GetComponent<CodexUIElement>();

            codexUIElement.Image.texture = CodexEntryData.RenderTexture;

            codexUIElement.DescriptionDisplayer.text = CodexEntryData.Description;

            codexUIElement.gameObject.SetActive(true);

            Destroy(gameObject);

        }
    }
}

