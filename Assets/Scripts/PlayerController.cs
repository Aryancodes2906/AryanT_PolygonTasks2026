using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask ground;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    public bool isGrounded;

    public bool isDead;


    public float coins = 0;
    public TMP_Text coin_text;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coin_text.text= coins.ToString();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed * (isDead?0:1), rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }

        if(transform.position.y <= -10)
        {
            SceneManager.LoadScene(0);
        }

    }

    private void Jump()
    {
        if(!isDead)
        {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        
        if(col.gameObject.tag.CompareTo("spike") == 0)
        {
            GetComponent<Collider2D>().enabled = false;
            isDead = true;
        }

        if(col.gameObject.tag.CompareTo("coin") == 0)
        {
            Destroy(col.gameObject);
            coins++;
            coin_text.text= coins.ToString();

        }

        
        if(col.gameObject.tag.CompareTo("end") == 0)
        {
            SceneManager.LoadScene(1);
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}