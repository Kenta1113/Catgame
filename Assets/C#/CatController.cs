using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] float _jump = 5f;
    [SerializeField] float _scratchDuration = 0.3f; // スクラッチ表示時間
    [SerializeField] private Sprite attackSprite;    // 攻撃時のスプライト
    [SerializeField] GameObject _scratchPrehub;
    [SerializeField] Transform _claw = default;
    [SerializeField] float _hp = 3f;
    [SerializeField] float _attackCooldown = 1f;
    float _lastAttackTime = -Mathf.Infinity;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Sprite defaultSprite;                    // 元のスプライト
    private bool isGrounded = false;
    private bool isScratching = false;
    public string _goalanime = "PlayerGoal";
    public string _gameover = "Gameover";

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
        if (Input.GetButtonDown("Fire1") && !isScratching && Time.time >= _lastAttackTime + _attackCooldown)
        {
            _lastAttackTime = Time.time; // クールダウン開始

            StartCoroutine(DoScratch());
            Debug.Log("攻撃");

            var scratch = Instantiate(_scratchPrehub);
            scratch.transform.position = _claw.position;
             int direction = transform.localScale.x > 0 ? 1 : -1;

            Vector3 scratchScale =scratch.transform.localScale;
            scratchScale.x=Mathf.Abs(scratchScale.x)*direction;
            scratch.transform.localScale = scratchScale;

            ScratchController bc = scratch.GetComponent<ScratchController>();
            if (bc != null)
                {
                bc.SetDirection(direction);
            }
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
        if(collision.gameObject.CompareTag("Enemy"))
        {
            _hp = _hp - 1;
            Debug.Log("ダメージを受けた");
            if(_hp == 0)
            {
                Debug.Log("Gameover");
            }
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
