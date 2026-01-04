using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfig currentWave;

    void Start()
    {
        SpawnEnemies();
    }

    public WaveConfig GetCurrentWave()
    {
        return currentWave;
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < currentWave.GetEnemyCount(); i++)
        {
            Instantiate(currentWave.GetEnemyPrefab(0), 
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.identity, 
                        transform);
        }
    }
}
