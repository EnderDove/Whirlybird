using System.Collections;
using System.Linq;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = transform;
    }

    private void FixedUpdate()
    {
        if (GameParameters.Instance.IsGameEnded)
            return;

        if (Whirlybird.Instance.PlayerBody.position.y < Whirlybird.Instance.MaxReachedY - GameParameters.ScreenSize.y)
        {
            StartCoroutine(FlyAway());
        }

        if (cameraTransform.position.y < Whirlybird.Instance.MaxReachedY)
        {
            cameraTransform.position = new Vector3(0, Mathf.MoveTowards(cameraTransform.position.y, Whirlybird.Instance.MaxReachedY, 1), -10);
        }
    }

    private IEnumerator FlyAway()
    {
        GameParameters.Instance.IsGameEnded = true;
        while (Whirlybird.Instance.PlayerBody.position.y > Whirlybird.Instance.MaxReachedY - GameParameters.ScreenSize.y * 5)
        {
            cameraTransform.position = new Vector3(0, Mathf.MoveTowards(cameraTransform.position.y, Whirlybird.Instance.PlayerBody.position.y, 1), -10);
            yield return new WaitForFixedUpdate();
        }
        Whirlybird.Instance.Die();
    }
}
