using UnityEngine;

public class EndGameState : GameBaseState
{
    private WordData _wordData;
    private bool _isCorrect;

    public EndGameState(WordData wordData, bool isCorrect)
    {
        _wordData = wordData;
        _isCorrect = isCorrect;
    }

    public override void OnActive()
    {
        base.OnActive();
        // show result
        gameControl.result.ShowResult(_wordData, _isCorrect);
        // hide keyboard
        gameControl.keyBoard.active(false);
        // hide text answer
        gameControl.gridWord.HideTextAnswer();
    }
}