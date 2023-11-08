using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public static GameParameters Instance;

    public Vector2 ScreenSize;
    public float DeadZoneY = 9f;
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
        Vector2 DotLD = new Vector2(-ScreenSize.x, -ScreenSize.y);
        Vector2 DotLU = new Vector2(-ScreenSize.x, ScreenSize.y);
        Vector2 DotRD = new Vector2(ScreenSize.x, -ScreenSize.y);
        Vector2 DotRU = new Vector2(ScreenSize.x, ScreenSize.y);
        Debug.DrawLine(DotLD, DotLU, Color.red);
        Debug.DrawLine(DotRD, DotRU, Color.red);
        Debug.DrawLine(DotLD, DotRD, Color.red);
        Debug.DrawLine(DotLU, DotRU, Color.red);
        Debug.DrawLine(new Vector2(-ScreenSize.x, -DeadZoneY), new Vector2(ScreenSize.x, -DeadZoneY));
    }
}
