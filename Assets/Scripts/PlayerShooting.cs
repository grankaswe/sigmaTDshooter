using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;

    [Header("Shooting Settings")]
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private bool autoFire = false;
    [SerializeField] private float spread = 5f; // Bullet spread in degrees
    [SerializeField] private int bulletsPerShot = 1; // For shotgun-like effect

    [Header("Effects")]
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private AudioClip shootSound;

    private float nextFireTime;
    private AudioSource audioSource;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && shootSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Create firePoint if not assigned
        if (firePoint == null)
        {
            GameObject newFirePoint = new GameObject("FirePoint");
            firePoint = newFirePoint.transform;
            firePoint.SetParent(transform);
            firePoint.localPosition = Vector3.up;
        }
    }

    private void Update()
    {
        // Detect left mouse button click (button 0 is left click)
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        nextFireTime = Time.time + fireRate;

        // Play sound effect
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Show muzzle flash
        if (muzzleFlashPrefab != null)
        {
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            Destroy(muzzleFlash, 0.1f);
        }

        // Spawn bullets
        for (int i = 0; i < bulletsPerShot; i++)
        {
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        // Calculate spread
        Quaternion spreadRotation = Quaternion.Euler(0, 0, Random.Range(-spread, spread));
        Quaternion finalRotation = firePoint.rotation * spreadRotation;

        // Spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, finalRotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            bulletRb.velocity = bullet.transform.up * bulletSpeed;
            Destroy(bullet, 2f);  // Destroy bullet after 2 seconds
        }

        // Ignore collision between the player and the bullet
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        Collider2D playerCollider = GetComponent<Collider2D>();

        if (bulletCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(bulletCollider, playerCollider);
        }
    }

    // Helper method to set weapon properties at runtime
    public void SetWeaponProperties(float newFireRate, bool newAutoFire, float newSpread, int newBulletsPerShot)
    {
        fireRate = newFireRate;
        autoFire = newAutoFire;
        spread = newSpread;
        bulletsPerShot = newBulletsPerShot;
    }
}
