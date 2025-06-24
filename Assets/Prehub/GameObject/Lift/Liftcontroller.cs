using UnityEngine;

public class SimpleLift : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 2f;
    [SerializeField] float _moveRange = 5f;

    [SerializeField] private AudioClip _liftSound;  // ìÆçÏâπ
    [SerializeField] private float _volume = 1f;

    private bool goingUp = true;
    private Vector3 startPos;

    private AudioSource audioSource;
    private Renderer rend;

    void Start()
    {
        startPos = transform.position;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = _liftSound;
        audioSource.loop = true;
        audioSource.volume = _volume;

        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (rend != null && rend.isVisible)
        {
            MoveLift();

            // âπÇ™ñ¬Ç¡ÇƒÇ»ÇØÇÍÇŒçƒê∂
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            // å©Ç¶Ç»ÇØÇÍÇŒâπÇé~ÇﬂÇÈ
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    private void MoveLift()
    {
        Vector3 pos = transform.position;
        float offsetY = pos.y - startPos.y;

        if (goingUp)
        {
            pos.y += _moveSpeed * Time.deltaTime;
            if (offsetY >= _moveRange)
            {
                pos.y = startPos.y + _moveRange;
                goingUp = false;
            }
        }
        else
        {
            pos.y -= _moveSpeed * Time.deltaTime;
            if (offsetY <= 0)
            {
                pos.y = startPos.y;
                goingUp = true;
            }
        }

        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.parent != this.transform)
                collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.activeInHierarchy)
                collision.transform.SetParent(null);
        }
    }
}
