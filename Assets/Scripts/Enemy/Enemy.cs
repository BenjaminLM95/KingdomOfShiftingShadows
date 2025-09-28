using UnityEngine;
using TMPro; 


public enum EnemyState 
{
    Running,
    Attacking,
    None
}


public class Enemy : MonoBehaviour
{
    public HealthSystem healthSystem = new HealthSystem();

    [Header("Base Stats")]
    [SerializeField] private int hp;
    [SerializeField] private int atk;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private Vector2 velocity; 


    [Header("Reference")]
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject kingdomsGate;
    public TextMeshPro hpText; 


    [Header("Behavior")]
    [SerializeField] EnemyState enemyState;
    [SerializeField] private Animator enemyAnimator; 

    [Header("Other variables")]
    public bool invincibility = false;


    private void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        kingdomsGate = GameObject.Find("KingdomGate");
        enemyAnimator = GetComponent<Animator>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        

        enemyState = EnemyState.Running;   //The enemy start running to the kingdom gates direction
        velocity = new Vector2(-1, 0); 

        hp = 4;
        atk = 2; 
        SetStats();

        hpText.text = "HP: " + healthSystem.health + " / " + healthSystem.maxHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        RunningThroughKingdomGates();
        enemyMove(); 
    }

    private void LateUpdate()
    {
        if (hp != healthSystem.health)
        {
            Debug.Log(healthSystem.health); 
            hp = healthSystem.health;
            hpText.text = "HP: " + healthSystem.health + " / " + healthSystem.maxHealth;

        }

        if(healthSystem.health <= 0)
            this.gameObject.SetActive(false);
    }

    public void SetStats() 
    {
        healthSystem.setMaxHP(hp);
        healthSystem.health = healthSystem.maxHealth; 
        healthSystem.setAttack(atk); 

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Sword") && !invincibility) 
        {
            Debug.Log("Collision with sword");
            healthSystem.TakeDamage(playerController.swordPower);
            Debug.Log(healthSystem.health);
            invincibility = true;
            enemyAnimator.SetBool("isDamaged", true); 
            Invoke("vulnerability", 1f);
        }

       
    }       

    private void vulnerability() 
    {
        invincibility = false;
        enemyAnimator.SetBool("isDamaged", false);
    }

    private void enemyMove() 
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0f) * speed * Time.deltaTime; 
    }


    private void RunningThroughKingdomGates() 
    {
        velocity = (kingdomsGate.transform.position - transform.position).normalized; 
    }
    
    

}
