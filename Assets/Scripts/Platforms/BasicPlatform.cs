public class BasicPlatform : Platform, IPropellerSpawner
{
    protected override void OnLandingAction()
    {
        Whirlybird.Instance.Jump();
    }
}
