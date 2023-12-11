using UnityEngine;

public abstract class MovingPlatform : Platform
{
    [SerializeField] private float Speed = 0.3f;
    private Transform platformTransform;

    private Vector2 leftTargetPosition;
    private Vector2 rightTargetPosition;

    private float yPos;
    private float randomShift;
    private float platformScaleX;

    private void Start()
    {
        platformTransform = transform;
        platformScaleX = GetComponent<BoxCollider2D>().size.x / 2f;
    }

    private void OnEnable()
    {
        randomShift = Random.value * 10;
        yPos = PlatfromSpawnHandler.platformSpawnHeight;

        leftTargetPosition = new Vector2(-GameParameters.ScreenSize.x + platformScaleX, yPos);
        rightTargetPosition = new Vector2(GameParameters.ScreenSize.x - platformScaleX, yPos);
    }

    private void Update()
    {
        platformTransform.position = Vector2.Lerp(leftTargetPosition, rightTargetPosition, (Mathf.Cos(Time.time * Mathf.PI * Speed + randomShift) + 1) * 0.5f);
    }
}
