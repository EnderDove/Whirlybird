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

    public float MaxReachedY { get; private set; }
    private Vector2 playerBodySize;

    [Header("Player Parameters")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpHeight = 8f;
    [SerializeField] private float hightJumpHeight = 15f;
    [SerializeField] private float flightDuration = 7f;
    [SerializeField] private float flightSpeed = 7f;

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
        HandleJump(10f);
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float VelocityDifference = InputHandler.MovementInput * movementSpeed - PlayerBody.velocityX;
        PlayerBody.AddForce(new Vector2(VelocityDifference, 0), ForceMode2D.Impulse);
        MaxReachedY = Mathf.Max(MaxReachedY, PlayerBody.position.y);

        PlayerAnimator.SetFloat("Velocity", PlayerBody.velocityX);

        float teleportationDistance = GameParameters.ScreenSize.x + playerBodySize.x / 2;
        if (Mathf.Abs(PlayerBody.position.x) > teleportationDistance)
            PlayerBody.position -= Mathf.Sign(PlayerBody.position.x) * new Vector2(2 * teleportationDistance, 0);
    }

    #region Actions
    public void Die()
    {
        SceneManager.LoadScene(0);
    }

    public void Jump()
    {
        HandleJump(jumpHeight);
    }

    public void HighJump()
    {
        HandleJump(hightJumpHeight);
    }

    public void ActivateFlight()
    {
        StartCoroutine(Fly());
    }

    private void HandleJump(float jumpHeight)
    {
        float jumpForce = Mathf.Sqrt(2 * PlayerBody.gravityScale * Physics.gravity.magnitude * jumpHeight);
        PlayerBody.AddForce(Vector2.up * (jumpForce - PlayerBody.velocityY), ForceMode2D.Impulse);
    }

    private IEnumerator Fly()
    {
        float _timer = flightDuration;
        while (_timer >= 0)
        {
            PlayerBody.velocity = new Vector2(PlayerBody.velocityX, flightSpeed);
            _timer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
