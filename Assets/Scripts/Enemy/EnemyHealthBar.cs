using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform enemyHealthBar;
    public GameObject healthBarBackground; 

    private float healthScale;
    private float currentEnemyHealth;    
    private float dHealth; 
    [SerializeField] private Enemy enemyStats;
   

    private void Awake()
    {        
        enemyHealthBar.localScale = healthBarBackground.transform.localScale;
        healthScale = enemyHealthBar.localScale.x; 
        enemyStats = this.GetComponent<Enemy>();         
             
         
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Health Scale " + healthScale);
        currentEnemyHealth = enemyStats.healthSystem.health;        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemyHealth != enemyStats.healthSystem.health) 
        {           

            if (enemyStats.healthSystem.health <= 0)
            {
                healthBarBackground.SetActive(false);
                enemyHealthBar.gameObject.SetActive(false); 
            }
            else 
            {                
                float newScaleX = ((float)enemyStats.healthSystem.health/ (float)enemyStats.healthSystem.maxHealth) * healthScale;     // Get the new scale based on the hp             

                enemyHealthBar.localScale = new Vector3(newScaleX, enemyHealthBar.localScale.y, enemyHealthBar.localScale.z);            
            }

            currentEnemyHealth = enemyStats.healthSystem.health;
        }
    }
}
