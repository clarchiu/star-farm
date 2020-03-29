using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;

    }

    public TimeSystem timeSystem;

    public Wave[] waves;
    private int nextWaveIndex = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private int totalCount = 0;

    private SpawnState state = SpawnState.COUNTING;

    private bool bossSpawned;
    public Transform boss;
    
    // Use this for initialization
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            gameObject.SetActive(false);
            throw new System.Exception("specify spawn points using inspector -Clarence");
        }

        if (waves.Length == 0)
        {
            gameObject.SetActive(false);
            throw new System.Exception("specify waves using inspector -Clarence");
        }

        if (timeSystem == null)
        {
            gameObject.SetActive(false);
            throw new System.Exception("specify TimeSystem using inspector -Clarence");
        }

        waveCountdown = timeBetweenWaves;
        InitializeTotalCount();
        timeSystem.OnDayIncrease += ScaleWaveWithDays;
        bossSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: temporary solution, need more details about how enemies spawn progresses
        if (timeSystem.isDay()) { return; } //do nothing if it is day time

        if (timeSystem.day % 7 == 0 && !bossSpawned)
        {
            SpawnEnemy(boss);
            bossSpawned = true;
        }

        if (state == SpawnState.WAITING)
        {
            //countdown
            WaveCompleted();
        }

        if (waveCountdown <= 0 && totalCount > 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //start spawning wave
                StartCoroutine(SpawnWave(waves[nextWaveIndex]));
            }
        }

        waveCountdown -= Time.deltaTime; //countdown to next wave
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWaveIndex + 1 > waves.Length - 1)
        {
            nextWaveIndex = 0;
        }
        else
        {
            nextWaveIndex++;
        }
    }

    //delegate pattern?
    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        //spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);

            yield return new WaitForSeconds(1 / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //spawn enemy
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        if (timeSystem.day > 1 && timeSystem.day % 7 != 0)
        {
            _enemy.gameObject.GetComponent<EnemyAI>().MyAttributes.maxHealth += timeSystem.day * 2;
        }
        Instantiate(_enemy, _sp.position, _sp.rotation);
        totalCount--;
    }

    private void ScaleWaveWithDays(int day)
    {
        ResetWaves();
        foreach (Wave wave in waves)
        {
            wave.count += day % 2;
            Debug.Log("wave count = " + wave.count);
        }

        InitializeTotalCount();
    }

    private void InitializeTotalCount()
    {
        totalCount = 0;
        foreach (Wave wave in waves)
        {
            totalCount += wave.count;
        }
        totalCount *= 10;
        Debug.Log("totalCount = " + totalCount);
    }

    void ResetWaves()
    {
        state = SpawnState.COUNTING;    //reset waves when it is day time
        waveCountdown = timeBetweenWaves;
        nextWaveIndex = 0;
        Debug.Log("reset waves");
    }
}



