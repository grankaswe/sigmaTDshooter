using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        if (healthSystem != null)
        {
            healthSystem.onHealthChanged.AddListener(UpdateHealthBar);
        }
    }

    private void UpdateHealthBar(float healthPercentage)
    {
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = healthPercentage;
        }
    }

    // If you need to assign the health system at runtime
    public void SetHealthSystem(HealthSystem newHealthSystem)
    {
        healthSystem = newHealthSystem;
        healthSystem.onHealthChanged.AddListener(UpdateHealthBar);
    }
}