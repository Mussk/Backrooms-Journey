using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    [SerializeField]
    private PlayerSensor FollowPlayerSensor;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip ChaseSound;

    [SerializeField]
    AudioClip AttackSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        FollowPlayerSensor.OnPlayerEnter += PlayChaseSound;
        Enemy.OnEnemyAttack += PlayAttackSound;
    }

    private void OnDisable()
    {
        FollowPlayerSensor.OnPlayerEnter -= PlayChaseSound;
        Enemy.OnEnemyAttack -= PlayAttackSound;
    }

    private void PlayAttackSound()
    {
        audioSource.PlayOneShot(AttackSound);
    }

    private void PlayChaseSound(Transform Player)
    {   
        if(ChaseSound != null)
        {
            audioSource.PlayOneShot(ChaseSound);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
