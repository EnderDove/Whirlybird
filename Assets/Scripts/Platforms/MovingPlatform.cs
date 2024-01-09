using UnityEngine;

public abstract class MovingPlatform : Platform
{
    public float LifeTime => Time.time - birthTime;

    private Transform platformTransform;

    private Vector2 leftTargetPosition;
    private Vector2 rightTargetPosition;

    private float yPos;
    private float randomShift;
    private float platformScaleX;
    private float birthTime;

    private void Start()
    {
        platformTransform = transform;
        platformScaleX = GetComponent<BoxCollider2D>().size.x / 2f;
    }

    private void OnEnable()
    {
        randomShift = Random.value * 10;
        yPos = PlatfromSpawnHandler.PlatformSpawnHeight;
        birthTime = Time.time;

        leftTargetPosition = new Vector2(-GameParameters.Instance.ScreenSize.x + platformScaleX, yPos);
        rightTargetPosition = new Vector2(GameParameters.Instance.ScreenSize.x - platformScaleX, yPos);
    }

    private void Update()
    {
        float lerp = (Mathf.Cos(LifeTime * Mathf.PI * GameParameters.Instance.GameSettings.MovingPlatformsSpeed + randomShift) + 1) * 0.5f;
        platformTransform.position = Vector2.Lerp(leftTargetPosition, rightTargetPosition, lerp);
    }
}
