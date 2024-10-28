using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float multiplyTime = 5f;
    public float spawnRadius = 4f;
    public static List<EnemyLogic> allEnemies = new List<EnemyLogic>();
    public static int totalEnemies = 0;
    public static int maxEnemies = 50;

    private int hitCount = 0;
    private int maxHits = 2;

    private void Start()
    {
        // Add this enemy to the list and increment total enemy count
        allEnemies.Add(this);
        totalEnemies++;

        // Start the multiplication process
        StartCoroutine(MultiplyOverTime());
    }

    IEnumerator MultiplyOverTime()
    {
        yield return new WaitForSeconds(multiplyTime);

        if (totalEnemies < maxEnemies)
        {
            Multiply();
        }

        // Restart the multiplication process
        StartCoroutine(MultiplyOverTime());
    }

    private void Multiply()
    {
        Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        allEnemies.Add(newEnemy.GetComponent<EnemyLogic>());
        totalEnemies++;
    }
}
