
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class LevelContentGenerator : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface navMeshSurface;

    [SerializeField]
    private List<GameObject> prefabsToSpawn;

    [SerializeField]
    private int amountEach;

    [SerializeField]
    private float maxRange;

    protected List<GameObject> spawnedPrefabs = new List<GameObject>();

    //spawns content in random place among level
    public virtual void SpawnContent()
    {
        
       foreach (GameObject prefab in prefabsToSpawn) 
        { 
            for (int i = 0; i < amountEach; i++)
            {

                Vector3 randomNavMeshPosition = GetRandomNavMeshPosition(navMeshSurface.navMeshData.sourceBounds.center,
                navMeshSurface.navMeshData.sourceBounds.size,
                Mathf.Max(navMeshSurface.navMeshData.sourceBounds.size.x,
                navMeshSurface.navMeshData.sourceBounds.size.z));

                spawnedPrefabs.Add(Instantiate(prefab, randomNavMeshPosition, Quaternion.identity));
                
            }
       }
    }

    private Vector3 GetRandomNavMeshPosition(Vector3 center, Vector3 size, float maxRange)
    {   
        

        Vector3 randomPosition = new Vector3(
            Random.Range(center.x - size.x / 2, center.x + size.x / 2),
            center.y,
            Random.Range(center.z - size.z / 2, center.z + size.z / 2)
        );
        

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, maxRange, NavMesh.AllAreas))
        {
            return hit.position;
        }

        // If no valid NavMesh position is found within maxDistance, return center
        return center;
    }
}
