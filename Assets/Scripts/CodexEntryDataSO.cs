using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewCodexEntryData", menuName = "ScriptableObjects/CodexEntryData", order = 1)]
public class CodexEntryDataSO : ScriptableObject
{
    public string Description;

    public RenderTexture RenderTexture;
}
    