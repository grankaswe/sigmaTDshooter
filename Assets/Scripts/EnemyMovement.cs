using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    public float moveSpeed = 2f;
    public float separationDistance = 1.5f;
    public float separationStrength = 1f;
    EnemyLogic enemyspawn;

    private int hitCount = 0;
    private int maxHits = 1;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

        }
        enemyspawn = FindObjectOfType<EnemyLogic>();
    }

    
    private void Update()
    {
        FollowPlayer();
        ApplySeparation();
    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    public void ApplySeparation()
    {
        foreach (EnemyLogic otherEnemyLogic in EnemyLogic.allEnemies)
        {
            if (otherEnemyLogic != this.GetComponent<EnemyLogic>()) // Avoid self-comparison
            {
                float distance = Vector2.Distance(transform.position, otherEnemyLogic.transform.position);
                if (distance < separationDistance)
                {
                    Vector2 moveAway = (transform.position - otherEnemyLogic.transform.position).normalized;
                    transform.position += (Vector3)(moveAway * separationStrength * Time.deltaTime);
                }
            }
        }
    }
    public void TakeDamage()
    {
        hitCount++;

        if (hitCount >= maxHits)
        {
            Die();
        }
    }

    private void Die()
    {
        
        //enemyspawn.allEnemies.Remove(this);
        //enemyspawn.totalEnemies--;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }
}
