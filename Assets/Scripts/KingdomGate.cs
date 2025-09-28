using UnityEngine;

public class KingdomGate : MonoBehaviour
{
    public bool gameOver { get; private set; }
    public GameObject gameOverText;

    [Header("Reference")]
    private PlayerHealth playerHealth; 


    private void Awake()
    {
        gameOver = false; 
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    private void Update()
    {
        if(!gameOver && playerHealth.healthSystem.health <= 0) 
        {
            SetGameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy")) 
        {
            if (!gameOver)
            {
                SetGameOver();
            }
        }
    }

    private void SetGameOver() 
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    
}
