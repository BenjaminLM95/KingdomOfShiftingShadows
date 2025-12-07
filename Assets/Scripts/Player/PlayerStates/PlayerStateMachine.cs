using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public IPlayerState currentPlayerState { get; private set; }

    public string currentStateString; 

    public void Initialize(IPlayerState playerState) 
    {
        //Debug.Log("Initialize a state"); 
        currentPlayerState = playerState;
        currentPlayerState.StateEnter();
        currentStateString = GetStateName(playerState);
    }


    public void ChangeState(IPlayerState playerState) 
    {
        //Debug.Log("Change states"); 
        currentPlayerState.StateExit();
        Initialize(playerState);
        currentStateString = GetStateName(playerState); 
    }


    private string GetStateName(IPlayerState playerState) 
    {
        switch (playerState) 
        {
            case PlayerAttackState:
                return "Attack State";
            case PlayerIdleState:
                return "Idle State";
            case PlayerWalkState:
                return "Walking State";
            case PlayerDeathState:
                return "Death State";
            case PlayerHurtState:
                return "Hurt State";
            default:
                return ""; 
        }
    }
}
