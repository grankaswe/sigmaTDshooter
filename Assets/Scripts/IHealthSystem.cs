using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private HealthSystem healthSystem;

    // Define these BEFORE using them in Awake
    public void OnHealthChanged(float healthPercentage)
    {
        Debug.Log($"Player health changed to: {healthPercentage * 100}%");
    }

    public void OnPlayerDeath()
    {
        Debug.Log("Player died!");
    }

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        if (healthSystem == null)
        {
            healthSystem = gameObject.AddComponent<HealthSystem>();
        }

        // Now these method references will be valid
        healthSystem.onHealthChanged.AddListener(OnHealthChanged);
        healthSystem.onDeath.AddListener(OnPlayerDeath);
    }

    private void OnDestroy()
    {
        if (healthSystem != null)
        {
            healthSystem.onHealthChanged.RemoveListener(OnHealthChanged);
            healthSystem.onDeath.RemoveListener(OnPlayerDeath);
        }
    }

    public void TakeDamage(float damage)
    {
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damage);
        }
    }

    public void Heal(float amount)
    {
        if (healthSystem != null)
        {
            healthSystem.Heal(amount);
        }
    }

    public float GetCurrentHealth()
    {
        return healthSystem != null ? healthSystem.GetCurrentHealth() : 0f;
    }
}