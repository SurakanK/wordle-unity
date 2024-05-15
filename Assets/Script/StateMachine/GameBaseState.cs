using System.Collections;
using System.Collections.Generic;
using StatePattern;
using UnityEngine;

public abstract class GameBaseState : IState
{
    public static StateMachine stateMachine;
    public static GameController gameControl;

    public override void OnInitialized()
    {
        gameControl = GameObject.FindAnyObjectByType<GameController>();
        Debug.Log(gameControl);
        base.OnInitialized();
    }

    public override void OnActive()
    {
        base.OnActive();

        Color color = Color.yellow;
        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}> Active state: </color> {3}", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), this.GetType().FullName));
    }

    public override void OnEnded()
    {
        base.OnEnded();

        Color color = Color.magenta;
        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}> Ended state: </color> {3}", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), this.GetType().FullName));
    }
}
