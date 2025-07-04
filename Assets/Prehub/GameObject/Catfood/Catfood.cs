using UnityEngine;

public class Catfood : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); // ƒAƒCƒeƒ€‚ğÁ‚·
        }
    }
}
