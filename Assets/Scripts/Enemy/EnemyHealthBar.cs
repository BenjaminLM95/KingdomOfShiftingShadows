using System;
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
        currentEnemyHealth = enemyStats.healthSystem.health;        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemyHealth != enemyStats.healthSystem.health) 
        {                                 
           float newScaleX = ((float)enemyStats.healthSystem.health/ (float)enemyStats.healthSystem.maxHealth) * healthScale;     // Get the new scale based on the hp             

           enemyHealthBar.localScale = new Vector3(newScaleX, enemyHealthBar.localScale.y, enemyHealthBar.localScale.z);           
            
            currentEnemyHealth = enemyStats.healthSystem.health;

            /*
            if (currentEnemyHealth > enemyStats.healthSystem.maxHealth/2) 
            {
                enemyHealthBar.gameObject.GetComponent<Renderer>().material.color = Color.green;
                Debug.Log("GreenBar"); 
            }
            else if(currentEnemyHealth > enemyStats.healthSystem.maxHealth / 4) 
            {
                enemyHealthBar.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                Debug.Log("Yellow Bar"); 
            }
            else if(currentEnemyHealth <= enemyStats.healthSystem.maxHealth / 4)
            {
                enemyHealthBar.gameObject.GetComponent<Renderer>().material.color = Color.red;
                Debug.Log("Red Bar"); 
            } */ 
        }

        if (enemyStats.isDefeat || enemyStats.enemyState == EnemyState.Faded)
        {
            
            healthBarBackground.SetActive(false);
            enemyHealthBar.gameObject.SetActive(false);
        }
        else 
        {
            healthBarBackground.SetActive(true);
            enemyHealthBar.gameObject.SetActive(true);
            
        }
    }
}
