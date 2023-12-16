using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    public PlatformPool ParentPool;
    public bool ContainsPropeller;

    [SerializeField] private SpriteRenderer PropellerSprite;

    protected abstract void OnLandingAction();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != Whirlybird.Instance.gameObject.name)
            return;

        if (Whirlybird.Instance.PlayerBody.velocityY <= 0 && transform.position.y < Whirlybird.Instance.PlayerBody.position.y)
        {
            OnLandingAction();
            if (ContainsPropeller)
            {
                Whirlybird.Instance.ActivateFlight();
                RemovePropeller();
            }
        }
    }

    private void FixedUpdate()
    {
        if (Whirlybird.Instance.MaxReachedY - GameParameters.ScreenSize.y > transform.position.y)
        {
            if (ContainsPropeller) { RemovePropeller(); }
            ParentPool.DespawnPlatform(this);
        }
    }

    private void RemovePropeller()
    {
        ContainsPropeller = false;
        PropellerSprite.enabled = false;
    }
}
