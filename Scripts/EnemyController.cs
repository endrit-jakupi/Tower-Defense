using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    private float moveSpeed = 2f;
    private int waypointIndex = 0;
    private Dictionary<int, float> rotationChanges;
    public GameObject explosionPrefab;
    GameObject scoreTextUI;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[waypointIndex].transform.position;
        }

        rotationChanges = new Dictionary<int, float>()
        {
            { 0, -90f },
            { 1, -180f },
            { 2, -90f },
            { 3, -0 },
            { 4, -90f },
        };

        scoreTextUI = GameObject.FindWithTag("ScoreText");
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex < waypoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.1f)
            {
                if (rotationChanges.ContainsKey(waypointIndex))
                {
                    transform.rotation = Quaternion.Euler(0, 0, rotationChanges[waypointIndex]);
                }

                waypointIndex += 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TowerBullet" || collision.tag == "Tower")
        {
            scoreTextUI.GetComponent<GameScoreController>().Score += 1;
            GameObject explosion = (GameObject)Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
