using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public delegate void HealthChanged(int currentHealth);
    public static event HealthChanged OnHealthChanged;

    public delegate void HealthIsZero();
    public static event HealthIsZero OnHealthIsZero;

    public int MaxHealth => _maxHealth;

    [SerializeField]
    private int _maxHealth;

    [SerializeField]
    private int _currentHealth;
    public int CurrentHealth
    {
        get
        { 
            return _currentHealth; 
        }
        set
        {
            _currentHealth = value;

            if (_currentHealth > 100)
            {
                _currentHealth = 100;
            }

            OnHealthChanged?.Invoke(_currentHealth);
           
            if (_currentHealth <= 0)
            {
                OnHealthIsZero?.Invoke();
            }
            
        }
    }
    
}
    