// BulletBehaviour.cs (Updated to work with new HealthSystem)
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private GameObject hitEffectPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Spawn hit effect
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        // Deal damage if the object has a health component
        var healthComponent = collision.gameObject.GetComponent<HealthSystem>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}