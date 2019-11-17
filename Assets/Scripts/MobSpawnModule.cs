using UnityEngine;
using System.Collections.Generic;

public class MobSpawnModule : MonoBehaviour
{
    private GameObject[] mobSpawnPoints;
    public List<MobSpawn> mobs = new List<MobSpawn>();
    private List<GameObject> mobSpawnQueue = new List<GameObject>();

    [Header("Wave Spawn Settings")]
    public int maxWave;
    public int currentWave;
    [Tooltip("x < 1: Early Rapid Spawn\nx = 1: Linear Spawn\nx > 1: Late Rapid Spawn")]
    public float spawnGrowth;
    public enum WaveType { Normal, Horde }
    public WaveType waveType;
    [Tooltip("Horde wave occurs every x waves.")]
    public int hordePeriod;
    [Tooltip("The delay for wave spawn.")]
    public float waveSpawnDelay;
    private float waveSpawnDelayTimer = 0f;
    [Tooltip("The rate for wave spawning each mob.")]
    public float waveMobSpawnRate;
    private float waveMobSpawnRateTimer = 0f;
    private bool waveEnded;
    private bool reachedMaxWave;

    public event System.Action hordeWaveEvent;

    void Start()
    {
        mobSpawnPoints = GameObject.FindGameObjectsWithTag("MobSpawnPoint");
        currentWave = 0;

        if (mobs.Count > 0)
        {
            foreach (MobSpawn mob in mobs)
            {
                GenerateSpawnCountsForMob(mob.spawnCounts, mob.waveStart, mob.baseCount, mob.finalCount);
            }
        }
    }

    void Update()
    {
        WaveSpawn();

        reachedMaxWave = (currentWave >= maxWave);
    }

    void WaveSpawn()
    {
        waveEnded = (mobSpawnQueue.Count == 0 && FindObjectsOfType<Mob>().Length == 0);

        if (waveType == WaveType.Horde)
        {
            ToggleHordeWaveEvent();
        }

        if (waveEnded)
        {
            if (Utilities.HasReachedTime(waveSpawnDelay, ref waveSpawnDelayTimer))
            {
                NextWave();
            }
        }

        UnloadMobSpawnQueueWave(waveMobSpawnRate, ref waveMobSpawnRateTimer);
    }

    void NextWave()
    {
        currentWave++;
        waveType = currentWave % hordePeriod == 0 ? WaveType.Horde : WaveType.Normal;

        if (waveType == WaveType.Horde)
        {
            if (reachedMaxWave)
            {
                WaveSpawnHorde((int) maxWave);
            }
            else
            {
                WaveSpawnHorde(currentWave);
            }
        }

        if (reachedMaxWave)
        {
            LoadMobSpawnQueueWave((int) maxWave);
        }
        else
        {
            LoadMobSpawnQueueWave(currentWave);
        }

        if (mobSpawnQueue.Count == 0)
        {
            waveEnded = false;
        }
    }

    void WaveSpawnHorde(int wave)
    {
        ToggleHordeWaveEvent();

        foreach (MobSpawn mob in mobs)
        {
            mob.spawnCounts[wave - 1] *= 2;
        }
    }

    void ToggleHordeWaveEvent()
    {
        if (hordeWaveEvent != null)
        {
            hordeWaveEvent();
        }
    }

    void GenerateSpawnCountsForMob(List<int> spawnCounts, int waveStart, int baseCount, int finalCount)
    {
        int waveCount = 0;
        for (int i = 0; i < maxWave; i++)
        {
            if (i + 1 >= waveStart)
            {
                waveCount++;
                spawnCounts.Add(MobSpawnCountAtWave(waveCount, waveStart, baseCount, finalCount));
            }
            else
            {
                spawnCounts.Add(0);
            }
        }
    }

    int MobSpawnCountAtWave(int waveCurrent, int waveStart, int baseCount, int finalCount)
    {
        float spawnConstant = (finalCount - baseCount) / Mathf.Pow(maxWave - waveStart, spawnGrowth);
        float spawnBaseFactor = Mathf.Pow(waveCurrent - 1, spawnGrowth);

        return (int)(spawnConstant * spawnBaseFactor + baseCount);
    }

    void LoadMobSpawnQueueWave(int wave)
    {
        foreach (MobSpawn mob in mobs)
        {
            for (int numberOfMobs = 0; numberOfMobs < mob.spawnCounts[wave - 1]; numberOfMobs++)
            {
                mobSpawnQueue.Add(mob.prefab);
            }
        }
    }

    void UnloadMobSpawnQueueWave(float t, ref float timer)
    {
        if (mobSpawnQueue.Count > 0)
        {
            if (Utilities.HasReachedTime(t, ref timer))
            {
                if (mobSpawnPoints.Length > 0)
                {
                    int index = Random.Range(0, mobSpawnQueue.Count);
                    GameObject mob = Instantiate(mobSpawnQueue[index], GetRandomSpawnPoint(), false);
                    mob.transform.SetParent(null);
                    mobSpawnQueue.Remove(mobSpawnQueue[index]);
                }
            }
        }
    }

    Transform GetRandomSpawnPoint() => mobSpawnPoints[Random.Range(0, mobSpawnPoints.Length)].transform;

    bool IsSpawning(float x) => Random.Range(0f, 1f) < x;
}

[System.Serializable]
public struct MobSpawn
{
    public string name;
    public GameObject prefab;
    [Tooltip("The percent chance of this mob spawning per t time.")]
    [Range(0, 1)] public float spawnChance;
    public int waveStart;
    public int baseCount;
    public int finalCount;
    public List<int> spawnCounts;
}
