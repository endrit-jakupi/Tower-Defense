using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float moveSpeed = 3f;
    public float backwardSpeed = 3f;
    public GameObject bulletPrefab;
    public GameObject towerGun;
    public GameObject explosionPrefab;
    public GameObject gameOverPanel;
    public GameScoreController gameScoreController;
    public GameObject gameWonPanel;
    float cameraHeight;
    float cameraWidth;
    public float health;
    public float maxHealth;
    public Image healthBar;
    public Image redHealthBar;
    public int lives = 3;
    public Text livesText;
    public float healthBarOffset = 1f;
    private float healthLoss = 35;

    void Start()
    {
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
        maxHealth = health;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            currentPosition += transform.up * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            currentPosition -= transform.up * (Input.GetKey(KeyCode.LeftShift) ? backwardSpeed : moveSpeed) * Time.deltaTime;
        }

        currentPosition.x = Mathf.Clamp(currentPosition.x, -cameraWidth + 0.5f, cameraWidth - 0.5f);
        currentPosition.y = Mathf.Clamp(currentPosition.y, -cameraHeight + 0.5f, cameraHeight - 0.5f);

        transform.position = currentPosition;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<AudioSource>().Play();
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = towerGun.transform.position;
            bullet.transform.rotation = transform.rotation;
        }

        if (gameScoreController.Score >= 13)
        {
            gameWonPanel.SetActive(true);
            Time.timeScale = 0;
        }

        Vector3 healthBarPosition = new Vector3(currentPosition.x, currentPosition.y + healthBarOffset, currentPosition.z);
        healthBar.rectTransform.position = Camera.main.WorldToScreenPoint(healthBarPosition);
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        redHealthBar.rectTransform.position = healthBar.rectTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet" || collision.tag == "Enemy")
        {
            lives--;
            livesText.text = $"{lives}";
            health -= healthLoss;
            healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            if (health <= 0)
            {
                healthBar.fillAmount = 0; 
                gameOverPanel.SetActive(true);
                Time.timeScale = 0;
                Destroy(gameObject);
            }
        }
    }
}