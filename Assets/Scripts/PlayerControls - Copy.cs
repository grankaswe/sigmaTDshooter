using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Camera mainCamera;
    private PlayerWeapon weapon;

    // Additional fields for death on enemy contact
    private float contactTime = 0f;
    private const float timeToDie = 2f;
    private bool inContactWithEnemy = false;

    private void Awake()
    {
        mainCamera = Camera.main;
        weapon = gameObject.AddComponent<PlayerWeapon>();
    }

    private void Update()
    {
        MoveTowardsMouse();

        if (Input.GetMouseButton(0))
        {
            weapon.Shoot();
        }

        RotateTowardsMouse();

        // Track contact time with enemy
        if (inContactWithEnemy)
        {
            contactTime += Time.deltaTime;
            Debug.Log($"Contact time: {contactTime}");

            if (contactTime >= timeToDie)
            {
                Die();
            }
        }
        else
        {
            contactTime = 0f;
        }
    }

    private void MoveTowardsMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z;
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);

        Vector2 mousePosition2D = new Vector2(worldMousePos.x, worldMousePos.y);
        Vector2 currentPosition = transform.position;
        Vector2 direction = (mousePosition2D - currentPosition).normalized;

        transform.position = Vector2.MoveTowards(currentPosition, mousePosition2D, moveSpeed * Time.deltaTime);
    }

    private void RotateTowardsMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z;
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);

        Vector2 direction = (new Vector2(worldMousePos.x, worldMousePos.y) - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            inContactWithEnemy = true;
            Debug.Log("Player started contact with enemy.");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            inContactWithEnemy = false;
            contactTime = 0f;
            Debug.Log("Player exited contact with enemy.");
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        Destroy(gameObject);
    }
}
