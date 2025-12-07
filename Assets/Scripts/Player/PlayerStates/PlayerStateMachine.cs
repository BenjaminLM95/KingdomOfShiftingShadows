using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public IPlayerState currentPlayerState { get; private set; }

    public void Initialize(IPlayerState playerState) 
    {
        currentPlayerState = playerState;
        currentPlayerState.StateEnter(); 
    }


    public void ChangeState(IPlayerState playerState) 
    {
        currentPlayerState.StateExit();
        Initialize(playerState);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
