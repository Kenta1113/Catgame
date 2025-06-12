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

        // 移動
        rb.velocity = new Vector2(move * _speed, rb.velocity.y);

        // 左右反転
        if (move != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
        }

        // ジャンプ
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jump);
        }

        // アニメーション切り替え（地面にいるときだけ）
        if (isGrounded)
        {
            anim.SetFloat("Speed", Mathf.Abs(move));
        }
        else
        {
            anim.SetFloat("Speed", 0f); // 空中ではSpeedを0に
        }
    }

    // 地面に触れたとき
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // 地面から離れたとき
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
