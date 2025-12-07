using UnityEngine;

public class PlayerDeathState : IPlayerState
{
    public PlayerAnimationHandler playerAnim;
    public PlayerController playerController;

    public PlayerDeathState(PlayerAnimationHandler _playerAnim, PlayerController _playerController)
    {
        playerAnim = _playerAnim;
        playerController = _playerController;
    }

    public void StateEnter()
    {
        playerController.RestrictPlayer(); 
        playerAnim.PlayerDeathAnim(true); 
    }

    public void StateExit()
    {
        playerAnim.PlayerDeathAnim(false);
    }

    public void StateFixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void StateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
