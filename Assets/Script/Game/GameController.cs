using System;
using System.Collections;
using System.Collections.Generic;
using StatePattern;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GridWord gridWord;
    public KeyBoard keyBoard;
    public Result result;
    public Info info;

    private void Awake()
    {
        GameBaseState.stateMachine = new StateMachine();
        User.category = "fruit";
    }

    private void Start()
    {
        GameBaseState.stateMachine.Initialize(new BeforeStartState());
    }

    public void KeyCapHighlight(char ch, Color color)
    {
        keyBoard.KeyCapHighlight(Enum.Parse<KeyCode>(ch.ToString(), true), color);
    }


}
