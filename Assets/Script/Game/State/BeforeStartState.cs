
using System.Collections;
using UnityEngine;

public class BeforeStartState : GameBaseState
{
    private Coroutine doLogic;
    
    public override void OnActive()
    {
        base.OnActive();

        // random Word
        var wordData = WordFactory.GetWord();
        // create board
        gameControl.gridWord.InitBoard(wordData);
        gameControl.gridWord.ShowTextAnswer(wordData);
        // show info
        gameControl.info.ShowInfo();
        // hide keyboard
        gameControl.keyBoard.active(false);

        doLogic = gameControl.StartCoroutine(DoLogic());
    }

    public override IEnumerator DoLogic()
    {
        yield return null;
        yield return new WaitUntil(() => gameControl.info.isShowPage == false);
        stateMachine.ChangeState(new GamePlayingState());
    }

    public override void OnEnded()
    {
        base.OnEnded();
        gameControl.StopCoroutine(doLogic);
    }
}