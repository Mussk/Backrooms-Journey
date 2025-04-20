using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthTextDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _healthText;

    
    private void OnEnable()
    {

        HealthController.OnHealthChanged += UpdateHealthDisplay;

    }

    private void OnDisable()
    {
        HealthController.OnHealthChanged -= UpdateHealthDisplay;
    }

    // Start is called before the first frame update
    void Start()
    {
        _healthText.text = "100";
    }

    private void UpdateHealthDisplay(int currentHealth)
    {

        _healthText.text = $"{currentHealth}"; ;
    }
}
