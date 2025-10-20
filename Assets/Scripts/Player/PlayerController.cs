using UnityEditor;
using UnityEngine;

public enum PlayerState 
{
    Walk,
    Attack,
    Idle,
    Death

}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;

    [Header("Player Movement Variables")]
    private float horizontal;
    private float vertical;
    [SerializeField] private float speedMov = 3f;
    [SerializeField] private float sprintSpeed = 5;
    private Vector3 initialPos = new Vector3(-6f, -0.5f);
    

    private Rigidbody2D body;

    [Header("Attack movements")]
    public GameObject sword; 
    public bool isAttacking = false;
    public int baseSwordPower = 3;
    public int upgradeSwordPower = 0;
    public int swordPower = 0; 
    [SerializeField] private Animator playerAnimator; 


    [Header("Abilities")]
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canAttack = true;

    public int numKill = 0;
    public int currency = 0;
    public bool isDead = false; 
   

    [Header("References")]
    [SerializeField] private UpgradeManager upgradeManager;
    public PlayerHealth playerHealth; 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
        transform.position = initialPos;    
        sword.gameObject.SetActive(false);
        body = GetComponent<Rigidbody2D>();
        playerState = PlayerState.Walk;
        playerAnimator = GetComponent<Animator>();
        UpdatingSwordMight();
        
       

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.I) && playerState != PlayerState.Attack) 
        {
            Attack(); 
        }


        if (upgradeManager.isSwordUpgrade && upgradeManager.currentSwordUpgrade != null) 
        {
            if(upgradeSwordPower != upgradeManager.currentSwordUpgrade.value) 
            {
                upgradeSwordPower = upgradeManager.currentSwordUpgrade.value;
                Debug.Log(upgradeSwordPower); 
                UpdatingSwordMight(); 
            }
        }

        


        if(playerHealth.healthSystem.health <= 0)
        {
            playerState = PlayerState.Death;
            sword.gameObject.SetActive(false);
            canAttack = true;
            playerAnimator.SetBool("isAttacking", false);
            isDead = true; 

        }
    }

    private void FixedUpdate()
    {
        


        if (canMove)
        body.linearVelocity = new Vector2(horizontal * speedMov, vertical * speedMov); 

        if(body.linearVelocity.magnitude > 0) 
        {
            if (playerState != PlayerState.Attack)
            {
                if (isAttacking)
                    isAttacking = false; 

                playerState = PlayerState.Walk;
                playerAnimator.SetBool("isWalking", true);
            }

            if (body.linearVelocityX > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                
            }
            else if(body.linearVelocityX < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                
            }
            
        }
        else 
        {
            playerState = PlayerState.Idle;
            playerAnimator.SetBool("isWalking", false);
        }
    }

    public void Attack() 
    {
        if (canAttack)
        {
            playerState = PlayerState.Attack;
            isAttacking = true;
            playerAnimator.SetBool("isAttacking", true); 
            sword.gameObject.SetActive(true);           
            canAttack = false;
            Invoke("SaveSword", 0.4f);
        }
    }

    public void SaveSword() 
    {        
        sword.gameObject.SetActive(false);
        canAttack = true;
        playerAnimator.SetBool("isAttacking", false);
        playerState = PlayerState.Walk; 
    }

    public void UpdatingSwordMight() 
    {
        swordPower = baseSwordPower + upgradeSwordPower;
    }
        
    public void UpdateOnUpgradeManager(int _num) 
    {
        upgradeManager.UpdatePlayerCurrency(_num); 
    }

    public void UpdateCurrency() 
    {
        if(currency != upgradeManager.playerCurrency) 
        {
            currency = upgradeManager.playerCurrency;
        }
    }

    public void SetStartingPosition() 
    {
        transform.position = initialPos;
        transform.localScale = new Vector3(1, 1, 1);
        isDead = false; 
    }

    

}
