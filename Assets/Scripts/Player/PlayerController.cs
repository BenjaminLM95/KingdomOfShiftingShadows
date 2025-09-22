using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Player Movement Variables")]
    private float horizontal;
    private float vertical;
    [SerializeField] private float speedMov = 3f;
    [SerializeField] private float sprintSpeed = 5;

    public Rigidbody2D body;

    [Header("Attack movements")]
    public GameObject sword; 
    public bool isAttacking = false;
     


    [Header("Abilities")]
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canAttack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sword.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Attack(); 
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
            isAttacking = true;
            sword.gameObject.SetActive(true);           
            canAttack = false;
            Invoke("SaveSword", 2f);
        }
    }

    public void SaveSword() 
    {        
        sword.gameObject.SetActive(false);
        canAttack = true; 
    }

}
