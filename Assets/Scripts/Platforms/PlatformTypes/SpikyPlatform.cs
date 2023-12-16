public class SpikyPlatform : Platform
{
    protected override void OnLandingAction()
    {
        Whirlybird.Instance.Die();
    }
}
