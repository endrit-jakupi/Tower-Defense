using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    float speed;
    bool isReady;
    Vector2 direction;
    private EnemySpawnerController enemySpawner;

    void Awake()
    {
        speed = 5f;
        isReady = false;
    }

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawnerController>();
    }

    public void SetDirection(Vector2 direction, float angle)
    {
        isReady = true;
        this.direction = direction;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void Update()
    {
        if (isReady)
        {
            Vector2 position = transform.position;
            position += direction * speed * Time.deltaTime;
            transform.position = position;

            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tower" || collision.tag == "TowerBullet")
        {
            Destroy(gameObject);
        }
    }
}