using UnityEngine;

public class PlayerHurtState : IPlayerState
{
    public PlayerController playerController;
    public PlayerHealth playerHealth;
    public PlayerAnimationHandler playerAnimationHandler;
    public SoundsManager soundManager;

    public PlayerHurtState(PlayerController _playerController, PlayerHealth _playerHealth, PlayerAnimationHandler _playerAnimationHandler, SoundsManager _soundManager) 
    {
        playerController = _playerController;
        playerHealth = _playerHealth;
        playerAnimationHandler = _playerAnimationHandler;
        soundManager = _soundManager;
    }


    public void StateEnter()
    {
        playerHealth.isHurt = false; 
        playerController._canAttack = false;        
        playerAnimationHandler.PlayerHurtAnim(true); 
        soundManager.PlaySoundFXClip("PlayerHurt");
        playerController.Invoke("Vulnerability", playerHealth.vulnerabilityCooldown);
    }

    public void StateExit()
    {
        playerAnimationHandler.PlayerHurtAnim(false); 
    }

    public void StateFixedUpdate()
    {
        //playerController.body.linearVelocity = Vector2.zero;
    }

    public void StateUpdate()
    {
        if (playerHealth.attackingEnemy != null)
        {
            if (playerHealth.attackingEnemy.enemyType == EnemyType.DayEnemy && playerHealth.healthSystem.health <= 0)
            {
                soundManager.PlaySoundFXClip("WitchLaugh");
                playerHealth.invincibility = false;
                playerController.playerStateMachine.ChangeState(playerController.playerDeathState);
            }
            else
            {
                playerHealth.attackingEnemy = null;
            }
        }
    }
}
