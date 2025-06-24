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
    [SerializeField] int _life = 3;
    [SerializeField] float _attackCooldown = 1f;
    [SerializeField] UIHPmanager uIHPmanager;

    //  各種効果音
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _healSound;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private float _attackVolume = 1.0f;
    [SerializeField] private float _jumpVolume = 2.0f;
    [SerializeField] private float _healVolume = 1.0f;
    [SerializeField] private float _damageVolume = 1.0f;

    Transform _respawnPoint;

    float _lastAttackTime = -Mathf.Infinity;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Sprite defaultSprite;
    private bool isGrounded = false;
    private bool isScratching = false;
    private bool isControlEnabled = true;
    private AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        defaultSprite = sr.sprite;
        audioSource = GetComponent<AudioSource>();

        GameObject startObj = GameObject.FindGameObjectWithTag("Start");
        if (startObj != null)
        {
            _respawnPoint = startObj.transform;
        }
        else
        {
            Debug.LogError("Startタグがついたオブジェクトが見つかりません！");
        }

        uIHPmanager.SetHp(_life);
    }

    void Update()
    {
        if (!isControlEnabled) return;

        float move = Input.GetAxisRaw("Horizontal");

        if (!isScratching)
        {
            rb.velocity = new Vector2(move * _speed, rb.velocity.y);

            if (move != 0)
                transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, _jump);

                //  ジャンプ音再生
                if (_jumpSound != null) audioSource.PlayOneShot(_jumpSound, _jumpVolume);
            }

            anim.speed = (isGrounded && move != 0) ? 1f : 0f;
        }

        if (Input.GetButtonDown("Fire1") && !isScratching && Time.time >= _lastAttackTime + _attackCooldown)
        {
            _lastAttackTime = Time.time;

            StartCoroutine(DoScratch());
            Debug.Log("攻撃");

            if (_attackSound != null) audioSource.PlayOneShot(_attackSound, _attackVolume);

            var scratch = Instantiate(_scratchPrefab);
            scratch.transform.position = _claw.position;
            int direction = transform.localScale.x > 0 ? 1 : -1;

            Vector3 scratchScale = scratch.transform.localScale;
            scratchScale.x = Mathf.Abs(scratchScale.x) * direction;
            scratch.transform.localScale = scratchScale;

            ScratchController bc = scratch.GetComponent<ScratchController>();
            if (bc != null) bc.SetDirection(direction);
        }
    }

    void DieAndRespawn()
    {
        _life--;
        uIHPmanager.SetHp(_life);

        //  ダメージ音
        if (_damageSound != null) audioSource.PlayOneShot(_damageSound, _damageVolume);

        if (_life <= 0)
        {
            Debug.Log("Game Over");
            isControlEnabled = false;
            FindObjectOfType<GameOverManager>().ShowGameOver();
            return;
        }

        Debug.Log("残機: " + _life + " → リスポーン");

        transform.position = _respawnPoint.position;
        rb.velocity = Vector2.zero;
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
            _life--;
            uIHPmanager.SetHp(_life);

            //  ダメージ音
            if (_damageSound != null) audioSource.PlayOneShot(_damageSound, _damageVolume);

            if (_life <= 0)
            {
                Debug.Log("Game Over");
                isControlEnabled = false;
                FindObjectOfType<GameOverManager>().ShowGameOver();
            }
            else
            {
                Debug.Log("敵に当たった → 残機: " + _life);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fall"))
        {
            DieAndRespawn();
        }
        else if (collision.CompareTag("Goal"))
        {
            FindObjectOfType<GameClearManager>().ShowGameClear();
        }
        else if (collision.CompareTag("Item"))
        {
            _life++;
            uIHPmanager.SetHp(_life);

            //  回復音
            if (_healSound != null) audioSource.PlayOneShot(_healSound, _healVolume);

            Debug.Log("アイテムを取った！HP: " + _life);
            Destroy(collision.gameObject);
        }
    }
}
