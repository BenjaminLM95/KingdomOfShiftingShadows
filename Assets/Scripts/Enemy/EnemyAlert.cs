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

            if (enemy.enemyType == EnemyType.DayEnemy && enemy.enemyState == EnemyState.Running)
            {
                Debug.Log("Day Enemy appears"); 
                alertWitchObj.SetActive(true);
                Invoke("SetOffWitchAlert", alertCooldown);
            }
            else if(enemy.enemyType == EnemyType.NightEnemy && enemy.enemyState == EnemyState.Running) 
            {
                alertZombieObj.SetActive(true);
                Invoke("SetOffZombieAlert", alertCooldown); 
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
