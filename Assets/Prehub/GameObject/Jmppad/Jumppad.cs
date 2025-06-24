using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float jumpForce = 10f;

    [SerializeField] private AudioClip _jumpPadSound;    // Å© å¯â âπ
    [SerializeField] private float _volume = 1.0f;        // Å© âπó 
    private AudioSource _audioSource;

    private void Start()
    {
        // AudioSource ÇéÊìæ or é©ìÆí«â¡
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Debug.Log("ÉWÉÉÉìÉv");

                Vector2 currentVelocity = rb.velocity;
                currentVelocity.y = 0;
                rb.velocity = currentVelocity;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                //  å¯â âπçƒê∂
                if (_jumpPadSound != null)
                {
                    _audioSource.PlayOneShot(_jumpPadSound, _volume);
                }
            }
        }
    }
}
