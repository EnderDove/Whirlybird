using System.Collections;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    public PlatformPool ParentPool { set; get; }
    public bool ContainsPropeller = false;

    [SerializeField] private SpriteRenderer propellerSprite;

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

    public void RemovePropeller()
    {
        ContainsPropeller = false;
        propellerSprite.enabled = false;
    }

    public void AddPropeller()
    {
        ContainsPropeller = true;
        propellerSprite.enabled = true;
    }

    public IEnumerator CheckingForDespawn()
    {
        while (true)
        {
            if (gameObject.activeSelf)
            {
                if (transform.position.y <= GameParameters.Instance.MaxReachedY - GameParameters.Instance.ScreenSize.y)
                    ParentPool.DespawnPlatform(this);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
