using System.Collections;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] float _jump = 5f;
    [SerializeField] float _scratchDuration = 0.3f; // スクラッチ表示時間
    [SerializeField] private Sprite attackSprite;    // 攻撃時のスプライト

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Sprite defaultSprite;                    // 元のスプライト
    private bool isGrounded = false;
    private bool isScratching = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        defaultSprite = sr.sprite; // 現在のスプライトを記録
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (!isScratching)
        {
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

            // アニメーション再生制御
            anim.speed = (isGrounded && move != 0) ? 1f : 0f;
        }

        // 攻撃処理
        if (Input.GetButtonDown("Fire1") && !isScratching)
        {
            StartCoroutine(DoScratch());
            Debug.Log("攻撃");
        }
    }

    IEnumerator DoScratch()
    {
        isScratching = true;

        // アニメーション止めてスプライト切り替え
        anim.enabled = false;
        sr.sprite = attackSprite;

        yield return new WaitForSeconds(_scratchDuration);

        // 元のスプライトとアニメーションに戻す
        sr.sprite = defaultSprite;
        anim.enabled = true;

        isScratching = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
