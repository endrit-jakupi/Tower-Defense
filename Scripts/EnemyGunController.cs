using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    private Vector2 playerTowerPosition;

    void Start()
    {
        GameObject playerTower = GameObject.Find("Tower");
        if (playerTower != null)
        {
            StartCoroutine(ShootEnemyBullet(playerTower));
        }
    }

    IEnumerator ShootEnemyBullet(GameObject playerTower)
    {
        while (true)
        {
            playerTowerPosition = playerTower.transform.position; 
            FireEnemyBullet();
            yield return new WaitForSeconds(5f);
        }
    }

    void FireEnemyBullet()
    {
        GameObject enemyBullet = Instantiate(enemyBulletPrefab);
        enemyBullet.transform.position = transform.position;

        Vector2 direction = (playerTowerPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        EnemyBulletController bulletController = enemyBullet.GetComponent<EnemyBulletController>();
        if (bulletController != null)
        {
            bulletController.SetDirection(direction, angle);
        }
    }
}
