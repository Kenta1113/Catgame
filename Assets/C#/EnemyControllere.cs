using UnityEngine;

public class EnemyControllere : MonoBehaviour
{
    [SerializeField] float _speed = 3.0f;
    [SerializeField] float _hp = 1f;
    [SerializeField] Transform groundCheck;  // �����`�F�b�N�ʒu
    [SerializeField] Transform frontCheck;   // �O���`�F�b�N�ʒu
    [SerializeField] float checkDistance = 0.3f;

    [SerializeField] private AudioClip _crySound;      // ����
    [SerializeField] private float _cryVolume = 1.0f;

    [SerializeField] private AudioClip _deathSound;    // ���S���̉�
    [SerializeField] private float _deathVolume = 1.0f;

    private bool movingRight = false;
    private Vector3 defaultScale;
    private Rigidbody2D rb;

    private AudioSource audioSource;
    private Renderer rend;

    private float cryInterval = 1f;
    private float nextCryTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultScale = transform.localScale;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = _cryVolume;

        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Debug�p����
        Debug.DrawRay(groundCheck.position, Vector2.down * checkDistance, Color.green);
        Vector2 frontDir = movingRight ? Vector2.right : Vector2.left;
        Debug.DrawRay(frontCheck.position, frontDir * checkDistance, Color.red);

        // �n�ʃ`�F�b�N
        RaycastHit2D groundHit = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance);
        bool noGroundAhead = !(groundHit.collider != null && groundHit.collider.CompareTag("Ground"));

        // �O���`�F�b�N�i�ǂ��G�j
        RaycastHit2D frontHit = Physics2D.Raycast(frontCheck.position, frontDir, checkDistance);
        bool wallOrEnemyAhead = frontHit.collider != null &&
                                (frontHit.collider.CompareTag("Ground") || frontHit.collider.CompareTag("Enemy"));

        if (noGroundAhead || wallOrEnemyAhead)
        {
            Flip();
        }

        rb.velocity = new Vector2((movingRight ? 1 : -1) * _speed, rb.velocity.y);

        // �J�����ɉf���Ă���ꍇ�A1�b���Ƃɖ������Đ�
        if (rend != null && rend.isVisible)
        {
            if (Time.time >= nextCryTime)
            {
                if (_crySound != null)
                {
                    audioSource.PlayOneShot(_crySound, _cryVolume);
                    nextCryTime = Time.time + cryInterval;
                }
            }
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x = movingRight ? -Mathf.Abs(defaultScale.x) : Mathf.Abs(defaultScale.x);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            _hp -= 1;
            Debug.Log("�v���C���[����U�����󂯂�");

            if (_hp <= 0)
            {
                Debug.Log("���ꂽ�`");
                // ���S���̉���炷
                if (_deathSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(_deathSound, _deathVolume);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
