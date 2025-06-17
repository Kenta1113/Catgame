using System.Collections;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] float _jump = 5f;
    [SerializeField] float _scratchDuration = 0.3f; // �X�N���b�`�\������
    [SerializeField] private Sprite attackSprite;    // �U�����̃X�v���C�g

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Sprite defaultSprite;                    // ���̃X�v���C�g
    private bool isGrounded = false;
    private bool isScratching = false;

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
        if (Input.GetButtonDown("Fire1") && !isScratching)
        {
            StartCoroutine(DoScratch());
            Debug.Log("�U��");
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
