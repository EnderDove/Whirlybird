using UnityEngine;

public class Cloud : Platform
{
    public bool IsDesrtoyed = false;

    [SerializeField] private SpriteRenderer cloudSpriteRenderer;
    [SerializeField] private ParticleSystem cloudParticleSystem;

    protected override void OnLandingAction()
    {
        if (IsDesrtoyed)
            return;
        cloudSpriteRenderer.enabled = false;
        cloudParticleSystem.Play();
        IsDesrtoyed = true;
    }
}
