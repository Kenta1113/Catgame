using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float jumpForce = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Debug.Log("ƒWƒƒƒ“ƒv");
                Vector2 currentVelocity = rb.velocity;
                currentVelocity.y = 0;
                rb.velocity = currentVelocity;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
