using UnityEngine;
using UnityEngine.Pool;

public class PlatformPool : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
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
        platform.ContainsPropeller = PlatfromSpawnHandler.GetRandomBool(0.05f);
        platform.transform.position = new Vector3(Random.Range(-GameParameters.ScreenSize.x, GameParameters.ScreenSize.x), platformSpawnHeight, 0);
    }

    public void DespawnPlatform(Platform platform)
    {
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
