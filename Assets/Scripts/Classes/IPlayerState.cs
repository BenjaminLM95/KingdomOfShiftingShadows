using UnityEngine;

public interface IPlayerState
{    

    public void StateEnter();   

    public void StateUpdate();

    public void StateFixedUpdate();

    public void StateExit();

}
