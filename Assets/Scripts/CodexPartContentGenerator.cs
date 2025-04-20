using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexPartContentGenerator : LevelContentGenerator
{
    [SerializeField]
    private List<CodexEntryDataSO> codexEntryDataSOs = new List<CodexEntryDataSO>();

    public override void SpawnContent()
    {
        base.SpawnContent();

        int randindex;
        
        foreach (GameObject spawnedPrefab in spawnedPrefabs)
        {
            Debug.Log("codexEntryDataSOs.Count: " + codexEntryDataSOs.Count);
            randindex = Random.Range(0, codexEntryDataSOs.Count);
            Debug.Log("randindex: " + randindex);
            spawnedPrefab.GetComponentInChildren<CodexItem>().CodexEntryData = codexEntryDataSOs[randindex];
            codexEntryDataSOs.RemoveAt(randindex);
        }

    }
}
