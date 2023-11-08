using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    protected abstract void OnLandingAction();

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnLandingAction();
    }
}
