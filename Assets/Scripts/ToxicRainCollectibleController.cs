using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicRainCollectibleController : MonoBehaviour
{
    [SerializeField]
    private string playerTag;

    [SerializeField]
    private int damageAmount;

    [SerializeField]
    private AudioSource audioSource;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            audioSource.Play();

            other.gameObject.GetComponent<HealthController>().CurrentHealth -= damageAmount;
           
            Destroy(gameObject);
        }
    }
}
