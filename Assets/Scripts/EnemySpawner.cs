using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }
    
    private IEnumerator SpawnAllWaves()
    {
        for(int i = startingWave; i < waveConfigs.Count; i++)
        {
            var CurrentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(CurrentWave));
        }
        
    }
    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var NewEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWavePoints()[0].transform.position, Quaternion.identity);
            NewEnemy.GetComponent<enemyPathing>().SetWaveConfig(waveConfig);//  setting wave config in enemyPathing class
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
