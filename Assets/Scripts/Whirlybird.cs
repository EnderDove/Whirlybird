using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputHandler))]
public class Whirlybird : MonoBehaviour
{
    #region  Object References
    public static Whirlybird Instance { get; private set; }
    public InputHandler InputHandler { get; private set; }
    public Rigidbody2D PlayerBody { get; private set; }
    public Animator PlayerAnimator { get; private set; }
    #endregion

    private Vector2 playerBodySize;

    #region Singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
    #endregion

    private void Start()
    {
        InputHandler = GetComponent<InputHandler>();
        PlayerBody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponentInChildren<Animator>();

        playerBodySize = GetComponent<BoxCollider2D>().size;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float VelocityDifference = InputHandler.MovementInput * GameParameters.Instance.GameSettings.MovementSpeed - PlayerBody.velocityX;
        PlayerBody.AddForce(new Vector2(VelocityDifference, 0), ForceMode2D.Impulse);
        GameParameters.Instance.MaxReachedY = Mathf.Max(GameParameters.Instance.MaxReachedY, PlayerBody.position.y);

        PlayerAnimator.SetFloat("Velocity", PlayerBody.velocityX);

        float teleportationDistance = GameParameters.Instance.ScreenSize.x + playerBodySize.x / 2;
        if (Mathf.Abs(PlayerBody.position.x) > teleportationDistance)
            PlayerBody.position -= Mathf.Sign(PlayerBody.position.x) * new Vector2(2 * teleportationDistance, 0);
    }

    #region Actions
    public void Die()
    {
        StartCoroutine(Dying());
    }

    public void ActivateFlight()
    {
        StartCoroutine(Fly());
    }

    public void Jump(float jumpHeight)
    {
        float jumpForce = Mathf.Sqrt(2 * PlayerBody.gravityScale * Physics.gravity.magnitude * jumpHeight);
        PlayerBody.AddForce(Vector2.up * (jumpForce - PlayerBody.velocityY), ForceMode2D.Impulse);
    }
    #endregion

    private IEnumerator Fly()
    {
        PlayerAnimator.SetBool("WithPropeller", true);
        float _timer = GameParameters.Instance.GameSettings.FlightDuration;
        while (_timer >= 0)
        {
            PlayerBody.velocity = new Vector2(PlayerBody.velocityX, GameParameters.Instance.GameSettings.FlightSpeed);
            _timer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        PlayerAnimator.SetBool("WithPropeller", false);
    }

    private IEnumerator Dying()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
