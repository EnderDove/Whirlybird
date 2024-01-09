using UnityEngine;

public class BasicMovingPlatform : MovingPlatform
{
    protected override void OnLandingAction()
    {
        Whirlybird.Instance.Jump(GameParameters.Instance.GameSettings.JumpHeight);
    }
}
