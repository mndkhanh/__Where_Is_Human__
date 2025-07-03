using UnityEngine;
using System.Collections;

public class EnemySpawnAfterDie : MonoBehaviour
{
    public float spawnDuration = 5;
    public GameObject enemyPrefab;

    public void SpawnEnemyAfterDie(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        Instantiate(enemyPrefab, spawnPosition, spawnRotation);
    }

    public IEnumerator RespawnAfterDelay(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        yield return new WaitForSeconds(spawnDuration);
        SpawnEnemyAfterDie(spawnPosition, spawnRotation);
    }
}
