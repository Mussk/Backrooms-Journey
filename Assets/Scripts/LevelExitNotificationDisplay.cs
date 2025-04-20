using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelExitNotificationDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _exitLevelNotificationText;

    [SerializeField]
    private float _alphaDelta;

    [SerializeField]
    private int _timeToShowUiElement;

    private void Start()
    {
        _exitLevelNotificationText.alpha = 0f;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnCollectiblesCountMaxReach += ShowUIElementEvent;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCollectiblesCountMaxReach -= ShowUIElementEvent;
        }
    }


    private void ShowUIElementEvent() 
    {

        StartCoroutine(ShowUIElementForTimeCoroutine(_exitLevelNotificationText));
    
    }

    private IEnumerator ShowUIElementForTimeCoroutine(TextMeshProUGUI uiElement) 
    {

        yield return StartCoroutine(ShowAndHideUICoroutine(uiElement, 0, 1, _alphaDelta));

        yield return new WaitForSeconds(_timeToShowUiElement);

        yield return StartCoroutine(ShowAndHideUICoroutine(uiElement,1, 0, _alphaDelta));

    }

    private IEnumerator ShowAndHideUICoroutine(TextMeshProUGUI uiElement ,float start, float end, float duration) 
    {

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            uiElement.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        uiElement.alpha = end;
    }
 }
