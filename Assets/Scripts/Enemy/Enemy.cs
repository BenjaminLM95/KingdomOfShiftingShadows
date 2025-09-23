using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthSystem healthSystem = new HealthSystem();

    [Header("Base Stats")]
    [SerializeField] private int hp;
    [SerializeField] private int atk;


    [Header("Reference")]
    [SerializeField] PlayerController playerController;


    [Header("Other variables")]
    public bool invincibility = false; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();

        hp = 4;
        atk = 2; 
        SetStats(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (hp != healthSystem.health)
        {
            Debug.Log(healthSystem.health); 
            hp = healthSystem.health;
        }
    }

    public void SetStats() 
    {
        healthSystem.setMaxHP(hp);
        healthSystem.health = healthSystem.maxHealth; 
        healthSystem.setAttack(atk); 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Sword") )//&& !invincibility) 
        {
            healthSystem.TakeDamage(playerController.swordPower);
            Debug.Log(healthSystem.health); 
            invincibility = true;
            Invoke("vulnerability", 1f); 
        }
    }

    private void vulnerability() 
    {
        invincibility = false; 
    }


}
