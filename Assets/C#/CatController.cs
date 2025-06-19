using System.Collections;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] float _jump = 5f;
    [SerializeField] float _scratchDuration = 0.3f;
    [SerializeField] private Sprite attackSprite;
    [SerializeField] GameObject _scratchPrefab;
    [SerializeField] Transform _claw = default;
    [SerializeField] int _hp = 3;
    [SerializeField] float _attackCooldown = 1f;
    [SerializeField] UIHPmanager uIHPmanager;

    float _lastAttackTime = -Mathf.Infinity;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Sprite defaultSprite;
    private bool isGrounded = false;
    private bool isScratching = false;

    public string _goalanime = "PlayerGoal";
    public string _gameover = "Gameover";

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        defaultSprite = sr.sprite;
        Debug.Log("初期HP: " + _hp);
        uIHPmanager.SetHp(_hp);
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (!isScratching)
        {
            rb.velocity = new Vector2(move * _speed, rb.velocity.y);
            //移動
            if (move != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
            }
            //ジャンプ
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, _jump);
            }

            anim.speed = (isGrounded && move != 0) ? 1f : 0f;
        }
        //攻撃
        if (Input.GetButtonDown("Fire1") && !isScratching && Time.time >= _lastAttackTime + _attackCooldown)
        {
            _lastAttackTime = Time.time;

            StartCoroutine(DoScratch());
            Debug.Log("攻撃");

            var scratch = Instantiate(_scratchPrefab);
            scratch.transform.position = _claw.position;
            int direction = transform.localScale.x > 0 ? 1 : -1;

            Vector3 scratchScale = scratch.transform.localScale;
            scratchScale.x = Mathf.Abs(scratchScale.x) * direction;
            scratch.transform.localScale = scratchScale;

            ScratchController bc = scratch.GetComponent<ScratchController>();
            if (bc != null)
            {
                bc.SetDirection(direction);
            }
        }
    }

    void TakeDamage()
    {
        _hp--;
        uIHPmanager.SetHp(_hp);

        if (_hp <= 0)
        {
            Debug.Log("Gameover");
            rb.velocity = Vector2.zero;
            // anim.Play(_gameover); // 必要ならコメントアウト解除
        }
        else
        {
            Debug.Log("ダメージを受けた → HP: " + _hp);
        }
    }

    void Heal(int amount)
    {
        _hp += amount;
        uIHPmanager.SetHp(_hp);
        Debug.Log("HPを回復 → HP: " + _hp);
    }

    IEnumerator DoScratch()
    {
        isScratching = true;

        anim.enabled = false;
        sr.sprite = attackSprite;

        yield return new WaitForSeconds(_scratchDuration);

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
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Heal(1);
        }
    }
}
