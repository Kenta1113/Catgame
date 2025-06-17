using UnityEngine;

public class SimpleLift : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float topY = 5f;
    public float bottomY = 0f;

    private bool goingUp = true;

    void Update()
    {
        Vector3 pos = transform.position;

        if (goingUp)
        {
            pos.y += moveSpeed * Time.deltaTime;
            if (pos.y >= topY)
            {
                pos.y = topY;
                goingUp = false;
            }
        }
        else
        {
            pos.y -= moveSpeed * Time.deltaTime;
            if (pos.y <= bottomY)
            {
                pos.y = bottomY;
                goingUp = true;
            }
        }

        transform.position = pos;
    }
}
