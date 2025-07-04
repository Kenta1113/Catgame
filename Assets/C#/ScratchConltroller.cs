using UnityEngine;

public class ScratchController : MonoBehaviour
{
    [SerializeField] float _scratchspeed = 1f;
    [SerializeField] float _scratchtime = 1f;

    Rigidbody2D _rb;

    void Start()
    {
        Destroy(this.gameObject, _scratchtime);
    }

    public void SetDirection(int dir)
    {
        Vector2 velocity = (dir == 1) ? Vector2.right : Vector2.left; ;
        _rb = GetComponent<Rigidbody2D>();
        if (_rb != null)
        {
            _rb.velocity = velocity * _scratchspeed;
        }
    }
}

