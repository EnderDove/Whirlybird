using UnityEngine;

public class PlatfromSpawnHandler : MonoBehaviour
{
    public PlatformPool[] platforms;
    public PlatformPool[] obstacles;

    [Range(0.1f, 5f), SerializeField] private float platformSpawnInterval = 2f;
    [Range(1, 10), SerializeField] private int obstaclesCountLimit = 3;
    public static float platformSpawnHeight { get; private set; }
    private int spawnedObstaclesCount = 0;

    private void Start()
    {
        platformSpawnHeight = -10f;
    }

    private void Update()
    {
        while (Mathf.Floor(Whirlybird.Instance.MaxReachedY / platformSpawnInterval) * platformSpawnInterval + GameParameters.ScreenSize.y >= platformSpawnHeight)
            SpawnPlatform();
    }

    private void SpawnPlatform()
    {
        PlatformPool platformPool = GetRandomPlatformPool();

        platformPool.SpawnPlatform(out GameObject platform, platformSpawnHeight);
        platformSpawnHeight += platformSpawnInterval;

        if (platform.TryGetComponent(out IPropellerSpawner propellerSpawner))
        {
            if (GetRandomBool(0.125f))
                propellerSpawner.SpawnPropeller();
        }
    }

    private bool GetRandomBool(float chance = 0.5f)
    {
        bool rand = Random.value < chance;
        return rand;
    }

    private PlatformPool GetRandomPlatformPool()
    {
        PlatformPool[] platformPools;

        float difficulty = Mathf.Clamp(Whirlybird.Instance.MaxReachedY / 100, 0f, 0.5f);

        if (GetRandomBool(difficulty * (1f - (float)spawnedObstaclesCount / obstaclesCountLimit)))
        {
            platformPools = obstacles;
            spawnedObstaclesCount++;
        }
        else
        {
            platformPools = platforms;
            spawnedObstaclesCount = 0;
        }

        int _index = Random.Range(0, platformPools.Length);
        return platformPools[_index];
    }
}
