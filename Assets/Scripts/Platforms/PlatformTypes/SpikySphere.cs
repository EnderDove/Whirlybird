using UnityEngine;

public class SpikySphere : MovingPlatform
{
    [SerializeField] private Animator platformAnimator;

    protected override void OnLandingAction()
    {
        var t = LifeTime % platformAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length / platformAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        if (t > 0.65f && t < 0.85f)
            Whirlybird.Instance.Die();
        else
            Whirlybird.Instance.Jump(GameParameters.Instance.GameSettings.JumpHeight);
    }
}
