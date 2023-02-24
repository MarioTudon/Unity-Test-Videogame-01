using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefab;
    [SerializeField] private float minTimeSpawn;
    [SerializeField] private float maxTimeSpawn;
    private int enemyNumber = 0;
    private ObjectPool<Enemy> enemiesPool;

    private void Start()
    {
        enemyNumber = enemyPrefab.Length > 1 ? Random.Range(0, enemyPrefab.Length) : 0;

        enemiesPool = new ObjectPool<Enemy>(() =>
        {
            Enemy enemy = Instantiate(enemyPrefab[enemyNumber], transform.position, Quaternion.identity);
            enemy.DeactivateEnemy(DeactivateEnemyPool);
            return enemy;
        }, enemy =>
        {
            enemy.transform.position = transform.position;
            enemy.gameObject.SetActive(true);
        }, enemy =>
        {
            enemy.gameObject.SetActive(false);
        }, enemy =>
        {
            Destroy(enemy.gameObject);
        }, true, 10, 25);

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));

        while (true)
        {
            enemyNumber = enemyPrefab.Length > 1 ? Random.Range(0, enemyPrefab.Length) : 0;
            enemiesPool.Get();
            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
        }
    }

    private void DeactivateEnemyPool(Enemy enemy)
    {
        enemiesPool.Release(enemy);
    }
}
