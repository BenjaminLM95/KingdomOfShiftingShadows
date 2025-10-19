using UnityEngine;

public enum PlayerState 
{
    Walk,
    Attack,
    Idle

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
   

    [Header("References")]
    [SerializeField] private UpgradeManager upgradeManager; 

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
                UpdatingSwordMight(); 
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        body.linearVelocity = new Vector2(horizontal * speedMov, vertical * speedMov); 
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

}
