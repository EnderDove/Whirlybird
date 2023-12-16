public class BasicPlatform : Platform
{
    protected override void OnLandingAction()
    {
        Whirlybird.Instance.Jump();
    }
}
