using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectiblesCountDisplay : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _collectiblesCountText;


    private void OnEnable()
    {
       
        GameManager.Instance.OnCollectiblesCountChanged += UpdateCollectiblesCountDisplay;
       
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCollectiblesCountChanged -= UpdateCollectiblesCountDisplay;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _collectiblesCountText.text = $"{GameManager.Instance.CurrentCollectiblesCount} / {GameManager.Instance.MaxCollectiblesCount}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCollectiblesCountDisplay(int currentCollectiblesCount)
    {   
        
        _collectiblesCountText.text = $"{currentCollectiblesCount} / {GameManager.Instance.MaxCollectiblesCount}";
    }
}
