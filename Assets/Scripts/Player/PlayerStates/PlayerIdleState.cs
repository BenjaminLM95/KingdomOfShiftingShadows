using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public PlayerAnimationHandler playerAnim;
    public PlayerController playerController; 

    public PlayerIdleState (PlayerAnimationHandler _playerAnim, PlayerController _playerController) 
    {
        playerAnim = _playerAnim;
        playerController = _playerController;
    }

    public void StateEnter()
    {
        playerAnim.PlayerMovAnim(false); 
    }

    public void StateExit()
    {
        
    }

    public void StateFixedUpdate()
    {
        
    }

    public void StateUpdate()
    {
        playerController.PlayerMovement();
    }
}
