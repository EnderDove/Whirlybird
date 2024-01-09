using System;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public static GameParameters Instance { get; private set; }
    public GameSettings GameSettings;

    public Vector2 ScreenSize { get; private set; }
    public bool IsGameEnded { get; set; }
    public float MaxReachedY
    {
        get => maxReachedY;
        set
        {
            scoreText.ChangeText();
            maxReachedY = value;
        }
    }
    public static float Record
    {
        get
        {
            record = MathF.Max(Instance.MaxReachedY, record);
            return record;
        }
    }

    [SerializeField] private Transform recordLine;
    [SerializeField] private TextView scoreText;
    private float maxReachedY;
    private static float record;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        ScreenSize = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
        recordLine.position = Vector3.up * Record;
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
