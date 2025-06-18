using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] float _jump = 5f;
    [SerializeField] float _scratchDuration = 0.3f; // �X�N���b�`�\������
    [SerializeField] private Sprite attackSprite;    // �U�����̃X�v���C�g
    [SerializeField] GameObject _scratchPrehub;
    [SerializeField] Transform _claw = default;
    [SerializeField] float _hp = 3f;
    [SerializeField] float _attackCooldown = 1f;
    float _lastAttackTime = -Mathf.Infinity;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Sprite defaultSprite;                    // ���̃X�v���C�g
    private bool isGrounded = false;
    private bool isScratching = false;
    public string _goalanime = "PlayerGoal";
    public string _gameover = "Gameover";

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        defaultSprite = sr.sprite; // ���݂̃X�v���C�g���L�^
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (!isScratching)
        {
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

            // �A�j���[�V�����Đ�����
            anim.speed = (isGrounded && move != 0) ? 1f : 0f;
        }

        // �U������
        if (Input.GetButtonDown("Fire1") && !isScratching && Time.time >= _lastAttackTime + _attackCooldown)
        {
            _lastAttackTime = Time.time; // �N�[���_�E���J�n

            StartCoroutine(DoScratch());
            Debug.Log("�U��");

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

        // �A�j���[�V�����~�߂ăX�v���C�g�؂�ւ�
        anim.enabled = false;
        sr.sprite = attackSprite;

        yield return new WaitForSeconds(_scratchDuration);

        // ���̃X�v���C�g�ƃA�j���[�V�����ɖ߂�
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
            Debug.Log("�_���[�W���󂯂�");
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
