using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputHandler))]
public class Whirlybird : MonoBehaviour
{
    #region  Object References
    public static Whirlybird Instance;
    public InputHandler InputHandler { get; private set; }
    public Rigidbody2D PlayerBody { get; private set; }
    public SpriteRenderer PlayerSprite { get; private set; }
    #endregion

    public float MaxReachedY { get; private set; }
    private bool isFacingLeft = false;

    [Header("Player Parameters")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float hightJumpForce = 20f;

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
        PlayerSprite = GetComponentInChildren<SpriteRenderer>();
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

        if (PlayerBody.velocityX > 0.01f & isFacingLeft)
            Flip();
        if (PlayerBody.velocityX < -0.01f & !isFacingLeft)
            Flip();

        if (PlayerBody.position.x > GameParameters.Instance.ScreenSize.x)
            PlayerBody.position -= new Vector2(2 * GameParameters.Instance.ScreenSize.x, 0);

        if (PlayerBody.position.x < -GameParameters.Instance.ScreenSize.x)
            PlayerBody.position += new Vector2(2 * GameParameters.Instance.ScreenSize.x, 0);
    }

    private void Flip()
    {
        PlayerSprite.flipX = !PlayerSprite.flipX;
        isFacingLeft = !isFacingLeft;
    }

    #region Actions
    public void Die()
    {
        SceneManager.LoadScene(0);
    }

    public void Jump()
    {
        HandleJump(jumpForce);
    }

    public void HighJump()
    {
        HandleJump(hightJumpForce);
    }

    private void HandleJump(float jumpForce)
    {
        Vector2 VelocityBeforeLanding = PlayerBody.velocity;
        VelocityBeforeLanding.y = 0f;
        PlayerBody.velocity = VelocityBeforeLanding;
        PlayerBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    #endregion
}
