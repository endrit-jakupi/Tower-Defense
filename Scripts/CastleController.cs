using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleController : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    private Animator animator;
    public float destroyedAnimationYPosition = 2.10f;
    public GameObject gameOverPanel;

    private void Start()
    {
        currentLives = maxLives;
        animator = GetComponent<Animator>();
        gameOverPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            currentLives -= 1;
            if (currentLives == 2)
            {
                animator.SetTrigger("damaged");
            }
            if (currentLives == 1)
            {
                animator.SetTrigger("destroyed");
            }
            if (currentLives <= 0)
            {
                gameOverPanel.SetActive(true);
                Time.timeScale = 0;
                Destroy(gameObject);
            }
        }
    }
}
