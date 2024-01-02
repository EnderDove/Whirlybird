using UnityEngine;
using UnityEngine.Pool;

public class PlatformPool : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private bool canSpawnPropeller;
    private ObjectPool<Platform> platforms;

    private void Start()
    {
        platforms = new(
            () => CreatePlatform(),
            (Platform platfrom) => GetPlatform(platfrom),
            (Platform platfrom) => ReleasePlatform(platfrom),
            (Platform platfrom) => DestroyPlatform(platfrom)
            );
    }

    public void SpawnPlatform(out Platform platform, float platformSpawnHeight)
    {
        platform = platforms.Get();
        float xPos = Random.Range(-GameParameters.Instance.ScreenSize.x + 0.5f, GameParameters.Instance.ScreenSize.x - 0.5f);
        platform.transform.position = new Vector3(xPos, platformSpawnHeight, 0);
        StartCoroutine(platform.CheckingForDespawn());

        if (canSpawnPropeller)
        {
            if (PlatfromSpawnHandler.GetRandomBool(GameParameters.Instance.GameSettings.PropellerSpawnChance))
                platform.AddPropeller();
        }
    }

    public void DespawnPlatform(Platform platform)
    {
        if (platform.ContainsPropeller)
            platform.RemovePropeller();
        platforms.Release(platform);
    }

    #region ObjectPooling
    private Platform CreatePlatform()
    {
        GameObject _platform = Instantiate(platformPrefab);
        Platform platform = _platform.GetComponent<Platform>();
        platform.ParentPool = this;
        platform.transform.parent = transform;
        return platform;
    }

    private void GetPlatform(Platform platform)
    {
        platform.gameObject.SetActive(true);
    }

    private void ReleasePlatform(Platform platform)
    {
        platform.gameObject.SetActive(false);
    }

    private void DestroyPlatform(Platform platform)
    {
        Destroy(platform.gameObject);
    }
    #endregion
}
