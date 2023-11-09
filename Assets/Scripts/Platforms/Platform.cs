using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    protected abstract void OnLandingAction();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != Whirlybird.Instance.gameObject.name)
            return;
        if (Whirlybird.Instance.PlayerBody.velocityY <= 0)
            OnLandingAction();
    }
}
