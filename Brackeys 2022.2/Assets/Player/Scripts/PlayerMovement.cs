using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float horizontal;
    [SerializeField] private Transform GFX;

    public bool IsMoving => horizontal != 0;

    [Space]
    [SerializeField] private float speed;

    [Header("Jumping")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private LayerMask jumpableLayer;
    public bool isGrounded = false;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        UpdateInput();

        if (!isGrounded && rb.velocity.y < 0f)
            rb.velocity += Vector2.down * gravityMultiplier;

        if (horizontal < 0)
            GFX.eulerAngles = Vector3.up * 180f;
        else if (horizontal > 0)
            GFX.eulerAngles = Vector3.up * 0f;

        if (IsMoving && isGrounded)
            AudioManager.Instance.Play("Walking");
        else
            AudioManager.Instance.Stop("Walking");

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void UpdateInput()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, jumpableLayer);

        horizontal = Input.GetAxisRaw("Horizontal");
    }

    public void Jump()
    {
        if (isGrounded)
            rb.AddForce(Vector2.up * jumpHeight * rb.mass, ForceMode2D.Impulse);
    }
}