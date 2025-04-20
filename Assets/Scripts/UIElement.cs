using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class UIElement : MonoBehaviour
{
   
    public delegate void UiElementBecameActive();
    public static event UiElementBecameActive OnUiElementBecameActive;

    public delegate void UiElementBecameInactive();
    public static event UiElementBecameInactive OnUiElementBecameInactive;

    protected virtual void OnEnable()
    {
        
        OnUiElementBecameActive?.Invoke();
    }

    protected virtual void OnDisable()
    {
        
        OnUiElementBecameInactive?.Invoke();
    }
}
