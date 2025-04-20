using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private int healAmount = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            
            GameManager.Instance.CurrentCollectiblesCount++;

            other.gameObject.GetComponent<HealthController>().CurrentHealth += 30;

            Destroy(this.gameObject);
        
        }
    }
}
