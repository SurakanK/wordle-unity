using UnityEngine;

public class GamePlayingState : GameBaseState
{
    public override void OnActive()
    {
        base.OnActive();

        // show keyBoard
        gameControl.keyBoard.active(true);
        gameControl.keyBoard.ClearKeyCapHighlight();

        gameControl.keyBoard.OnKeyPress += OnKeyPress;
    }

    public override void OnEnded()
    {
        base.OnEnded();
        gameControl.keyBoard.OnKeyPress -= OnKeyPress;
    }

    private void OnKeyPress(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.Backspace:
                gameControl.gridWord.DeleteText();
                break;
            case KeyCode.Return:
                gameControl.gridWord.ApplyWord();
                break;
            default:
                gameControl.gridWord.PushText(keyCode.ToString());
                break;
        }
    }
}