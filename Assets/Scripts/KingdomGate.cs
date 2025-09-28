using UnityEngine;

public class KingdomGate : MonoBehaviour
{
    public bool gameOver { get; private set; }
    public GameObject gameOverText;


    private void Awake()
    {
        gameOver = false; 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy")) 
        {
            if (!gameOver)
            {
                gameOver = true;
                gameOverText.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    
}
