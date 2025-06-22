using UnityEngine;

public class EnemyControllere : MonoBehaviour
{
    [SerializeField] float _speed = 3.0f;
    [SerializeField] float _hp = 1f;
    [SerializeField] Transform groundCheck;  // 足元チェック位置
    [SerializeField] Transform frontCheck;   // 前方チェック位置
    [SerializeField] float checkDistance = 0.3f;

    private bool movingRight = false;
    private Vector3 defaultScale;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultScale = transform.localScale;
    }

    void Update()
    {
        // Debug可視化用
        Debug.DrawRay(groundCheck.position, Vector2.down * checkDistance, Color.green); // 地面確認
        Vector2 frontDir = movingRight ? Vector2.right : Vector2.left;
        Debug.DrawRay(frontCheck.position, frontDir * checkDistance, Color.red); // 前方確認

        // 地面チェック
        RaycastHit2D groundHit = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance);
        bool noGroundAhead = !(groundHit.collider != null && groundHit.collider.CompareTag("Ground"));

        // 前方チェック（壁 or 敵）
        RaycastHit2D frontHit = Physics2D.Raycast(frontCheck.position, frontDir, checkDistance);
        bool wallOrEnemyAhead = frontHit.collider != null &&
                                (frontHit.collider.CompareTag("Ground") || frontHit.collider.CompareTag("Enemy"));

        if (noGroundAhead || wallOrEnemyAhead)
        {
            Flip();
        }

        rb.velocity = new Vector2((movingRight ? 1 : -1) * _speed, rb.velocity.y);
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
            Debug.Log("プレイヤーから攻撃を受けた");

            if (_hp <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("やられた～");
            }
        }
    }
}
