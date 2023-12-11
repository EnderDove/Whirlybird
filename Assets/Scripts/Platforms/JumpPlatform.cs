using UnityEngine;

public class JumpPlatform : Platform
{
    [SerializeField] private Animator anim;

    protected override void OnLandingAction()
    {
        anim.Play("JumpPlatform");
        Whirlybird.Instance.HighJump();
    }
}
