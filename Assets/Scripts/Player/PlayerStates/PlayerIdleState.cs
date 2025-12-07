using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public PlayerAnimationHandler playerAnim; 

    public PlayerIdleState (PlayerAnimationHandler _playerAnim) 
    {
        playerAnim = _playerAnim;
    }

    public void StateEnter()
    {
        playerAnim.PlayerMovAnim(false); 
    }

    public void StateExit()
    {
        throw new System.NotImplementedException();
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
