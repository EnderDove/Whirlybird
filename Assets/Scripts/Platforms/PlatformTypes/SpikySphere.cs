using UnityEngine;

public class SpikySphere : MovingPlatform
{
    [SerializeField] private Animator animator;

    protected override void OnLandingAction()
    {
        Debug.Log(animator);
    }
}
