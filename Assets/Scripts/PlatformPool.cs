using UnityEngine;
using UnityEngine.Pool;

public class PlatformPool : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    private ObjectPool<GameObject> platforms;

    private void Start()
    {
        platforms = new(
            () => CreatePlatform(),
            (GameObject platfrom) => GetPlatform(platfrom),
            (GameObject platfrom) => ReleasePlatform(platfrom),
            (GameObject platfrom) => DestroyPlatform(platfrom)
            );
    }

    public void SpawnPlatform(out GameObject platform, float platformSpawnHeight)
    {
        platform = platforms.Get();
        platform.transform.position = new Vector3(Random.Range(-GameParameters.ScreenSize.x, GameParameters.ScreenSize.x), platformSpawnHeight, 0);
    }

    public void DespawnPlatform(GameObject platform)
    {
        platforms.Release(platform);
    }

    #region ObjectPooling
    private GameObject CreatePlatform()
    {
        GameObject platfrom = Instantiate(platformPrefab);
        platfrom.GetComponent<Platform>().ParentPool = this;
        platfrom.transform.parent = transform;
        return platfrom;
    }

    private void GetPlatform(GameObject platform)
    {
        platform.SetActive(true);
    }

    private void ReleasePlatform(GameObject platform)
    {
        platform.SetActive(false);
    }

    private void DestroyPlatform(GameObject platform)
    {
        Destroy(platform);
    }
    #endregion
}
