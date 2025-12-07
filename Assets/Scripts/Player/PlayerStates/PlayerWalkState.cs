using UnityEngine;

public class PlayerWalkState : IPlayerState
{
    public PlayerAnimationHandler playerAnim;
    public PlayerController playerController;
    
    public PlayerWalkState(PlayerAnimationHandler _playerAnim, PlayerController _playerController) 
    {
        playerAnim = _playerAnim;
        playerController = _playerController;
    }

    public void StateEnter()
    {
        playerAnim.PlayerMovAnim(true); 
    }

    public void StateExit()
    {
        playerAnim.PlayerMovAnim(false);
    }

    public void StateFixedUpdate()
    {
        
    }

    public void StateUpdate()
    {
        playerController.PlayerMovement();
    }
}
