using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;

    private Vector2 moveDirection;

    void Start()
    {
        speed = 8f;
        moveDirection = transform.up;
    }

    void Update()
    {
        Vector2 position = transform.position;
        position += moveDirection * speed * Time.deltaTime;
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (position.y > max.y || position.y < min.y || position.x > max.x || position.x < min.x)
        {
            Destroy(gameObject);
        }
    }
}
