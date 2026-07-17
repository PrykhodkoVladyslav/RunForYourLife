using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int intervalInSeconds;
    private float _timeElapsed = 0;

    private void Update()
    {
        if (PauseController.Instance.IsPaused)
            return;

        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >= intervalInSeconds)
        {
            _timeElapsed -= intervalInSeconds;

            var prefab = GetRandomEnemyPrefab();
            if (prefab)
            {
                var enemy = Instantiate(prefab);
                enemy.transform.position = transform.position;
            }
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