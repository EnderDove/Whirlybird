using UnityEngine;

public class MovingPlatform : Platform
{
    private Vector2 leftTargetPosition;
    private Vector2 rightTargetPosition;
    private float yPos;
    private float randomShift;

    private void OnEnable()
    {
        randomShift = Random.value * 10;
        yPos = PlatfromSpawnHandler.platformSpawnHeight;
        float platformScaleX = GetComponent<BoxCollider2D>().size.x;
        leftTargetPosition = new Vector2(-GameParameters.Instance.ScreenSize.x + platformScaleX, yPos);
        rightTargetPosition = new Vector2(GameParameters.Instance.ScreenSize.x - platformScaleX, yPos);
    }

    protected override void OnLandingAction()
    {
        Whirlybird.Instance.Jump();
    }

    private void Update()
    {
        Vector2 position = Vector2.Lerp(leftTargetPosition, rightTargetPosition, (Mathf.Cos(Time.time + randomShift) + 1) * 0.5f);
        transform.position = position;
    }
}
