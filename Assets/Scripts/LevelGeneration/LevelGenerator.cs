using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    private static LevelGenerator _instance;

    public static LevelGenerator Instance
    {

        get
        {
            if (_instance == null)
            {

                _instance = FindObjectOfType<LevelGenerator>();

                /*if (_instance == null) 
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

    [SerializeField]
    private GameObject _startingLevelPart;

    [SerializeReference]
    private List<GameObject> _levelParts;

    [SerializeField]
    private List<GameObject> _zeroOneConnectorsLevelParts;

    [SerializeField]
    private NavMeshSurface NavMeshSurface;

    [SerializeField]
    private List<LevelContentGenerator> _levelContentGenerators;

    [SerializeField]
    private NavMeshCleaner navMeshCleaner;

    [SerializeField]
    private string layerMaskName;
    
    private GameObject _spawnedPrefab;



    private void Start()
    {
        GenerateLevel(_levelParts);
       
    }

  /*  private GameObject ChooseObject(List<GameObject> levelParts, GameObject current)
    {
        
        List<GameObject> filteredLevelParts = new List<GameObject>();

        switch (current.GetComponent<LevelPart>().LevelPartType)
        {
            case LevelPartType.Room:
                filteredLevelParts = levelParts.FindAll((levelPart) => levelPart.GetComponent<LevelPart>().LevelPartType == LevelPartType.Intersection);
                break;
            case LevelPartType.Intersection:
                filteredLevelParts = levelParts.FindAll((levelPart) => levelPart.GetComponent<LevelPart>().LevelPartType == LevelPartType.Room);
                break;
            default:
                Debug.LogError("Undefined type of strucutre!");
                break;
        }
        if (filteredLevelParts.Count > 0)
        {
            int randIndex = Random.Range(0, filteredLevelParts.Count - 1);

            return filteredLevelParts[randIndex];
        }
        else
        { 
            return null; 
        }

        
    }*/

    private GameObject SpawnLevelPart(ref List<GameObject> levelParts)
    {

        int randIndex = Random.Range(0, levelParts.Count - 1);

        GameObject result = Instantiate(levelParts[randIndex], new Vector3(100,100,100), Quaternion.identity);

        _spawnedPrefab = levelParts[randIndex];

        levelParts.RemoveAt(randIndex);

        return result;
    }
   
    private bool ConnectParts(GameObject current, GameObject next)
    {
        List<GameObject> currentConnectors = GetConnectors(current);
        List<GameObject> nextConnectors = GetConnectors(next);

        //filter unconnected connectors
        currentConnectors = currentConnectors.FindAll((connector) => !connector.GetComponent<Connector>().IsConnected);
        nextConnectors = nextConnectors.FindAll((connector) => !connector.GetComponent<Connector>().IsConnected);

        GameObject currentPickedConnector = currentConnectors[Random.Range(0, currentConnectors.Count-1)];
        GameObject nextPickedConnector = nextConnectors[Random.Range(0, nextConnectors.Count-1)];

        RotateParentFaceToFace(currentPickedConnector, ref nextPickedConnector);

        if (MoveParentToPos(nextPickedConnector, currentPickedConnector.transform.position, current))
        {

            currentPickedConnector.GetComponent<Connector>().IsConnected = true;
            nextPickedConnector.GetComponent<Connector>().IsConnected = true;

            currentPickedConnector.SetActive(false);
            nextPickedConnector.SetActive(false);
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool MoveParentToPos(GameObject objectToMove, Vector3 desiredPos, GameObject current)
    {
        Vector3 childCurrentPosition = objectToMove.transform.position;

        Vector3 offset = desiredPos - childCurrentPosition;

        Vector3 targetPos = objectToMove.transform.parent.position + offset;

        BoxCollider objectToMoveBoxCollider = objectToMove.transform.parent.GetComponent<BoxCollider>();

        Vector3 colliderCenter = objectToMove.transform.parent.TransformPoint(objectToMoveBoxCollider.center) + offset;
        Vector3 colliderExtents = objectToMoveBoxCollider.size / 2;
        Quaternion colliderRotation = objectToMoveBoxCollider.transform.rotation;

        Physics.SyncTransforms();

        if (IsEnoughSpaceToMove(colliderCenter, colliderExtents, colliderRotation, current))
        {
            objectToMove.transform.parent.position += offset;

            return true;
        }
        else
        {
            Debug.Log("Not enough space to move between: " + objectToMove.transform.parent.name + " and " + current.name);
            return false;
        }
        
    }

    private bool IsEnoughSpaceToMove(Vector3 colliderCenter, Vector3 colliderExtents, Quaternion colliderRotation, GameObject current)
    {
        
        Debug.Log("current: " + current.gameObject.name);

        Debug.Log("colliderCenter: " + colliderCenter);
        Debug.Log("colliderExtents: " + colliderExtents);
        //OutputData(current.GetComponent<BoxCollider>().bounds);
        // Debug.Log("next: " + objectToMove.gameObject.name);
        //OutputData(objectToMoveBoxCollider.bounds);

        
        Collider[] colliders = Physics.OverlapBox(colliderCenter, colliderExtents, colliderRotation, LayerMask.GetMask(layerMaskName));

        colliders.ToList().ForEach((collider) => { Debug.Log(collider); });
  

       /* List<Collider> collidersList = colliders.ToList().FindAll((collider) =>
        {
            LevelPart levelPart;
            return collider.gameObject.TryGetComponent<LevelPart>(out levelPart);

        });*/

        Debug.Log("Colliders which cause overlapping: " + colliders.Length);
        //collidersList.ForEach((collider) => { Debug.Log(collider); });

        return colliders.Length == 0;
        
    }

    private void RotateParentFaceToFace(GameObject current, ref GameObject next)
    {
        Vector3 currentOpposite = -current.transform.forward;
        Vector3 nextForward = next.transform.forward;

        //Quaternion currentRotationNext = Quaternion.LookRotation(nextForward, Vector3.up);
        Quaternion targetRotationNext = Quaternion.LookRotation(currentOpposite, Vector3.up);
        
        Quaternion relativeRotation = targetRotationNext * Quaternion.Inverse(next.transform.rotation);

        next.transform.parent.rotation = relativeRotation * next.transform.parent.rotation;
    }

    private List<GameObject> GetConnectors(GameObject levelPart)
    {
        List<GameObject> connectors = new List<GameObject>();

        foreach(Connector connector in levelPart.GetComponentsInChildren<Connector>()) 
            connectors.Add(connector.gameObject);
        
        return connectors;
        
    }

    private List<GameObject> RemovePartsWithoutFreeConnectors(List<GameObject> avaliableLevelParts)
    {   
        List<GameObject> filteredList = new List<GameObject>();

        foreach(GameObject part in avaliableLevelParts)
        {
            if(GetConnectors(part).FindAll((connector) => !connector.GetComponent<Connector>().IsConnected).Count > 0)
            {
                filteredList.Add(part);
            }
        }

        return filteredList;
    }

    private void GenerateContent(List<LevelContentGenerator> levelContentGenerators)
    {
        foreach(var levelContentGenerator in levelContentGenerators)
        {
            levelContentGenerator.SpawnContent();
        }

    }

    

   /* private void OutputData(Bounds bounds)
    {
        //Output to the console the center and size of the Collider volume
        Debug.Log("Collider Center : " + bounds.center);
        Debug.Log("Collider Size : " + bounds.size);
        Debug.Log("Collider bound Minimum : " + bounds.min);
        Debug.Log("Collider bound Maximum : " + bounds.max);
    }*/

    private void GenerateLevel(List<GameObject> levelParts)
    {   
        int counter = 0;
        
        List<GameObject> avaliableLevelParts = new List<GameObject>
        {
            _startingLevelPart
        };

        Debug.Log("LevelPart count: " + levelParts.Count);

        while (levelParts.Count > 0)
        {
          
            avaliableLevelParts = RemovePartsWithoutFreeConnectors(avaliableLevelParts);

            GameObject current = null;

            Debug.Log(avaliableLevelParts.Count);

            current = avaliableLevelParts[Random.Range(0, avaliableLevelParts.Count)];
       
            GameObject next = SpawnLevelPart(ref levelParts);

            if(ConnectParts(current, next)) 
            { 
                avaliableLevelParts.Add(next);
                counter++;
            }
            else
            {
                
                levelParts.Add(_spawnedPrefab);
                Destroy(next);
                
            }

            if(levelParts.Count == 0)
            {
                levelParts = _zeroOneConnectorsLevelParts;
            }
        }

        Debug.Log("Spawned level parts: " + counter);

        NavMeshSurface.BuildNavMesh();

        navMeshCleaner.Build();

        NavMeshSurface.BuildNavMesh();

        navMeshCleaner.SetMeshVisible(false);

        GenerateContent(_levelContentGenerators);
    }

    
}
