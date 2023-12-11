using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    public PlatformPool ParentPool;

    protected abstract void OnLandingAction();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != Whirlybird.Instance.gameObject.name)
            return;

        if (Whirlybird.Instance.PlayerBody.velocityY <= 0 && transform.position.y < Whirlybird.Instance.PlayerBody.position.y)
            OnLandingAction();
    }

    private void FixedUpdate()
    {
        if (Whirlybird.Instance.MaxReachedY - GameParameters.ScreenSize.y > transform.position.y)
            ParentPool.DespawnPlatform(gameObject);
    }
}
