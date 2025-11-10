using UnityEngine;

public class EnemyAlert : MonoBehaviour
{
    public GameObject alertWitchObj;
    public GameObject alertZombieObj; 
    [SerializeField] private float alertCooldown = 2f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SettOffAllAlerts();
        alertCooldown = 2f;
    }    
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger!"); 
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy.enemyType == EnemyType.DayEnemy)
            {
                Debug.Log("Day Enemy appears"); 
                alertWitchObj.SetActive(true);
                Invoke("SetOffWitchAlert", alertCooldown);
            }
            else if(enemy.enemyType == EnemyType.NightEnemy) 
            {
                alertZombieObj.SetActive(true);
                Invoke("SetOffZombieAlert", alertCooldown); 
            }
            else 
            {
                Debug.LogWarning("No Enemy component found on " + other.name);
            }
        }
    }

    private void SetOffWitchAlert() 
    {
        alertWitchObj.SetActive(false); 
    }

    private void SetOffZombieAlert() 
    {
        alertZombieObj.SetActive(false); 
    }

    private void SettOffAllAlerts() 
    {
        SetOffWitchAlert();
        SetOffZombieAlert(); 
    }

}
