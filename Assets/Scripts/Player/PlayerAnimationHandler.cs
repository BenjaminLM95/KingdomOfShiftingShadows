using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }


    public void PlayerMovAnim(bool active) 
    {
        playerAnimator.SetBool("isWalking", active);
    }

    
    public void PlayPlayerAttackAnim() 
    {
        playerAnimator.Play("FSkyAttackAnimation");
    }

    public void PlayerDamagedAnim(bool active) 
    {
        playerAnimator.SetBool("isDamaged", active);
    }


    public void PlayerDeathAnim(bool active) 
    {
        playerAnimator.SetBool("isDead", active);
    }

    public void PlayerHurtAnim(bool active) 
    {
        playerAnimator.SetBool("isDamaged", active); 
    }

    

}
