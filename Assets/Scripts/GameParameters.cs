using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public static GameParameters Instance;

    public Vector2 ScreenSize;
    public bool IsGameEnded = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void OnDrawGizmos()
    {
        Vector2 cameraPos = new(Camera.main.transform.position.x, Camera.main.transform.position.y);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(cameraPos, ScreenSize * 2);
    }
}
