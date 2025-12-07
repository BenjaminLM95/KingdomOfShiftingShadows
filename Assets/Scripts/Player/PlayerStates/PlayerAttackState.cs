using UnityEngine;

public class PlayerAttackState : IPlayerState
{
    public PlayerController playerController;
    public PlayerAnimationHandler playerAnim;
    public SoundsManager soundManager;
    

    public PlayerAttackState(PlayerController _playerController, PlayerAnimationHandler _playerAnim, SoundsManager _soundManager) 
    {
        playerController = _playerController;
        playerAnim = _playerAnim;
        soundManager = _soundManager;
    }

    public void StateEnter()
    {
        playerController.Attack(); 
        soundManager.PlaySoundFXClip("SlashSword");
        playerController.canAttack = false;
        playerAnim.PlayPlayerAttackAnim();
        playerController.Invoke("SaveSword", playerController.attackCooldown); 

    }

    public void StateExit()
    {
        
    }

    public void StateFixedUpdate()
    {
        playerController.body.linearVelocity /= (1 + Time.fixedDeltaTime * 4);
    }

    public void StateUpdate()
    {
        
    }
}
