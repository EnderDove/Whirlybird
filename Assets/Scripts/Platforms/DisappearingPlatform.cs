using UnityEngine;

public class DisappearingPlatform : Platform
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color color;

    private void Update()
    {
        spriteRenderer.color = Color.Lerp(color, new Color(0, 0, 0, 0), -0.5f * Mathf.Cos(Time.time * Mathf.PI) + 0.5f);
    }

    protected override void OnLandingAction()
    {
        Whirlybird.Instance.Jump();
    }
}
