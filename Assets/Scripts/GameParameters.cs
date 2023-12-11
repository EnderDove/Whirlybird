using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public static GameParameters Instance;

    public static Vector2 ScreenSize { get; private set; }
    public bool IsGameEnded { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        ScreenSize = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
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
