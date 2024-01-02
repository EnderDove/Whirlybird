using System.Collections;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = transform;
    }

    private void Update()
    {
        if (GameParameters.Instance.IsGameEnded)
            return;
        if (Whirlybird.Instance.PlayerBody.position.y < GameParameters.Instance.MaxReachedY - GameParameters.Instance.ScreenSize.y)
            StartCoroutine(FlyAway());
        if (cameraTransform.position.y < GameParameters.Instance.MaxReachedY)
        {
            Vector3 deltaPos = new(0, GameParameters.Instance.MaxReachedY - cameraTransform.position.y, 0);
            cameraTransform.position += deltaPos;
        }
    }

    private IEnumerator FlyAway()
    {
        GameParameters.Instance.IsGameEnded = true;
        while (Whirlybird.Instance.PlayerBody.position.y > GameParameters.Instance.MaxReachedY - GameParameters.Instance.ScreenSize.y * 5)
        {
            cameraTransform.position = new Vector3(0, Mathf.MoveTowards(cameraTransform.position.y, Whirlybird.Instance.PlayerBody.position.y, 1), -10);
            yield return new WaitForFixedUpdate();
        }
        Whirlybird.Instance.Die();
    }
}
