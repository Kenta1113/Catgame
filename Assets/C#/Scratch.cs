using UnityEngine;

public class ScratchEffectController : MonoBehaviour
{
    [SerializeField] float _scratchspeed = 1f;
    [SerializeField] float _scratchtime = 1f;

    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, _scratchtime);
    }

    public void SetDirection(int dir)
    {
        Vector2 velocity = (dir == 1) ? Vector2.right : Vector2.left; ;
        if (_rb != null)
        {
            _rb.velocity = velocity * _scratchspeed;
        }
    }
}
