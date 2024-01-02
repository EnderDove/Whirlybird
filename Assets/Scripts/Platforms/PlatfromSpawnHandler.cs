using UnityEngine;


public class PlatfromSpawnHandler : MonoBehaviour
{
    public PlatformPool[] platforms;
    public PlatformPool[] obstacles;

    public static float PlatformSpawnHeight { get; private set; }
    private static int spawnedObstaclesCount = 0;

    private void Start()
    {
        PlatformSpawnHeight = -GameParameters.Instance.ScreenSize.y;
    }

    private void FixedUpdate()
    {
        while (Mathf.Floor(GameParameters.Instance.MaxReachedY / GameParameters.Instance.GameSettings.PlatformSpawnInterval) * GameParameters.Instance.GameSettings.PlatformSpawnInterval + GameParameters.Instance.ScreenSize.y >= PlatformSpawnHeight)
            SpawnPlatform();
    }

    private void SpawnPlatform()
    {
        GetRandomPlatformPool().SpawnPlatform(out Platform platform, PlatformSpawnHeight);
        PlatformSpawnHeight += GameParameters.Instance.GameSettings.PlatformSpawnInterval;
    }

    public static bool GetRandomBool(float chance = 0.5f)
    {
        bool rand = Random.value < chance;
        return rand;
    }

    private PlatformPool GetRandomPlatformPool()
    {
        PlatformPool[] platformPools;

        float difficulty = Mathf.Clamp(GameParameters.Instance.MaxReachedY / 100, 0f, 0.5f);

        if (GetRandomBool(difficulty * (1f - (float)spawnedObstaclesCount / GameParameters.Instance.GameSettings.ObstaclesCountLimit)))
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
