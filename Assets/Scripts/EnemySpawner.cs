using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int intervalInSeconds;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalInSeconds);

            var enemy = Instantiate(GetRandomEnemyPrefab());
            enemy.transform.position = transform.position;
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            return null;

        var index = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[index];
    }
}