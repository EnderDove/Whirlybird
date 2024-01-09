using UnityEngine;

public class DestructiblePlatform : Platform
{
    public bool IsDesrtoyed { get; private set; }

    [SerializeField] private SpriteRenderer platformSpriteRenderer;
    [SerializeField] private ParticleSystem platformParticleSystem;

    private void OnEnable()
    {
        IsDesrtoyed = false;
        platformSpriteRenderer.enabled = true;
    }

    protected override void OnLandingAction()
    {
        Whirlybird.Instance.Jump(GameParameters.Instance.GameSettings.JumpHeight);
        if (IsDesrtoyed)
            return;
        platformSpriteRenderer.enabled = false;
        platformParticleSystem.Play();
        IsDesrtoyed = true;
    }
}
