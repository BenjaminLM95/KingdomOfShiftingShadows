using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    public PlayerController player;

    [SerializeField] private Animator swordAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        swordAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isAttacking) 
        {
            swordAnimator.SetBool("swordATK", true);
        }
    }


    public void SaveSword()
    {
        swordAnimator.SetBool("swordATK", false);
        player.isAttacking = false; 
    }
}
