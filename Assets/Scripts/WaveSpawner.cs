using System;
using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    public Transform spawnPoint;
    public TextMeshProUGUI waveCountdownText;
    public float timeBetweenWaves = 2;
    public GameManager gameManager;

    private float countdown = 10f;
    private int waveNumber = 0;

    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }
        
        if (waveNumber == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
        
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        //waveCountdownText.text = "Next Wave: " + Mathf.Ceil(countdown).ToString();
        waveCountdownText.text = "Next Wave: " + string.Format("{0:00.0}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;
        Wave wave = waves[waveNumber];
        EnemiesAlive = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNumber++;
        Debug.Log("Wave Incoming");
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
