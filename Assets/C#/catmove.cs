using UnityEngine;

public class CatMove : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] float _jump = 5f;

    private Animator anim;
    private Rigidbody2D rb;

    private bool isGrounded = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        // �ړ�
        rb.velocity = new Vector2(move * _speed, rb.velocity.y);

        // ���E���]
        if (move != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
        }

        // �W�����v
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jump);
        }

        // �A�j���[�V�����؂�ւ��i�n�ʂɂ���Ƃ������j
        if (isGrounded)
        {
            anim.SetFloat("Speed", Mathf.Abs(move));
        }
        else
        {
            anim.SetFloat("Speed", 0f); // �󒆂ł�Speed��0��
        }
    }

    // �n�ʂɐG�ꂽ�Ƃ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // �n�ʂ��痣�ꂽ�Ƃ�
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
