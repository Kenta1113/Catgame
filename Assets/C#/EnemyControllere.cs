using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllere : MonoBehaviour
{
    [SerializeField] float _speed = 3.0f;
    [SerializeField] string direction = "left";
    [SerializeField] float range = 0f;

    Vector3 defPos;
    Vector3 defaultscale;

    void Start()
    {
        defPos = transform.position;
        defaultscale = transform.localScale;

        if (direction == "right")
        {
            transform.localScale = new Vector3(-defaultscale.x, defaultscale.y, defaultscale.z);
        }
    }

    void Update()
    {
        if (range > 0f)
        {
            if (transform.position.x < defPos.x - range / 2)
            {
                direction = "right";
                transform.localScale = new Vector3(-defaultscale.x, defaultscale.y, defaultscale.z);
            }
            if (transform.position.x > defPos.x + range / 2)
            {
                direction = "left";
                transform.localScale = new Vector3(defaultscale.x, defaultscale.y, defaultscale.z);
            }
        }
    }

    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (direction == "right")
        {
            rb.velocity = new Vector2(_speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-_speed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (direction == "right")
        {
            direction = "left";
            transform.localScale = new Vector3(defaultscale.x, defaultscale.y, defaultscale.z);
        }
        else
        {
            direction = "right";
            transform.localScale = new Vector3(-defaultscale.x, defaultscale.y, defaultscale.z);
        }
    }
}
