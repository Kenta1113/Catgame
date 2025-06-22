using UnityEngine;

public class SimpleLift : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 2;
    [SerializeField] float _moveRange = 5; // �㉺�̈ړ��͈�

    private bool goingUp = true;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
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
            // ���łɐe�ɂȂ��ĂȂ����m�F
            if (collision.transform.parent != this.transform)
            {
                collision.transform.SetParent(this.transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Lift���A�N�e�B�u�̂Ƃ��̂� SetParent(null) ���s��
            if (gameObject.activeInHierarchy)
            {
                collision.transform.SetParent(null);
            }
        }
    }
}