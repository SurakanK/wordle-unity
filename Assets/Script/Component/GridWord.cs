using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using Unity.VisualScripting;

public class GridWord : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject TextAnswer;

    private int _curRow;
    private int _curColumn;
    private string _curGuess = "";
    private string _wordTarget;
    private int _maxRow = 6;
    private int _maxColumn = 0;

    public int CurRow => _curRow;

    public WordData wordData;

    private ObjectPool<GameObject> _linePoll;
    private ObjectPool<GameObject> _cellPoll;

    private void Awake()
    {
        _linePoll = new ObjectPool<GameObject>(CreateLine, GetLine, ReleaseLine);
        _cellPoll = new ObjectPool<GameObject>(CreateCell, GetCell, ReleaseCell);
    }

    private void ReleaseCell(GameObject cell)
    {
        cell.gameObject.SetActive(false);
        cell.GetComponent<Image>().color = Color.white;
        SetCharacter("", cell.transform);
    }

    private void GetCell(GameObject cell)
    {
        cell.SetActive(true);
    }

    private GameObject CreateCell()
    {
        var cell = Instantiate(cellPrefab, transform);
        cell.GetComponent<Image>().color = Color.white;
        SetCharacter("", cell.transform);
        cell.SetActive(false);
        return cell;
    }

    private void ReleaseLine(GameObject line)
    {
        line.SetActive(false);
    }

    private void GetLine(GameObject line)
    {
        line.SetActive(true);
    }

    private GameObject CreateLine()
    {
        return Instantiate(linePrefab, transform);
    }

    public void InitBoard(WordData wordData)
    {
        _curRow = 0;
        _curColumn = 0;
        _curGuess = "";
        _wordTarget = wordData.en;
        _maxColumn = _wordTarget.Length;
        this.wordData = wordData;

        ClearGrid();
        CreateGrid();
    }

    private void ClearGrid()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var line = transform.GetChild(i);
            for (int j = 0; j < line.childCount; j++)
            {
                var cell = line.GetChild(j);
                _cellPoll.Release(cell.gameObject);
            }
            _linePoll.Release(line.gameObject);
        }
    }

    private void CreateGrid()
    {
        for (int i = 0; i < _maxRow; i++)
        {
            var line = _linePoll.Get();
            var size = GameUtils.CellSize(line.transform, _maxColumn, 10);
            size = (size.x > 100) ? new Vector2(100, 100) : size;

            for (int j = 0; j < _maxColumn; j++)
            {
                var cell = _cellPoll.Get();
                cell.transform.SetParent(line.transform);
                GameUtils.RectTransformSize(cell.transform, size);
            }
        }
    }

    private void SetCharacter(string character, Transform cell)
    {
        var text = cell.GetComponentInChildren<TextMeshProUGUI>();
        text.color = Color.black;
        text.text = character;
    }

    public void PushText(string character)
    {
        if (_curRow == transform.childCount) return;

        var row = transform.GetChild(_curRow);
        if (_curColumn == row.childCount) return;

        var cell = row.GetChild(_curColumn);
        SetCharacter(character, cell);

        _curGuess += $"{character}";
        _curColumn++;
    }

    public void DeleteText()
    {
        if (_curColumn == 0) return;
        var cell = transform.GetChild(_curRow).GetChild(_curColumn - 1);
        SetCharacter("", cell);

        _curGuess = _curGuess.Substring(0, _curGuess.Length - 1);
        _curColumn--;
    }

    public void ApplyWord()
    {
        if (_curColumn < _maxColumn) return;

        var charWordTraget = _wordTarget.ToCharArray();
        var charWordGuess = _curGuess.ToLower().ToCharArray();
        GameBaseState.gameControl.keyBoard.ClearKeyCapHighlight();

        for (int i = 0; i < charWordGuess.Length; i++)
        {
            var ch = charWordGuess[i];
            if (ch == charWordTraget[i])
            {
                // correct
                CellHighlight(i, GameColor.Green);
                GameBaseState.gameControl.KeyCapHighlight(ch, GameColor.Green);
            }
            else if (_wordTarget.Contains(ch))
            {
                // wrong position
                CellHighlight(i, GameColor.Yellow);
                GameBaseState.gameControl.KeyCapHighlight(ch, GameColor.Yellow);
            }
            else
            {
                // not correct
                CellHighlight(i, GameColor.Grey);
                GameBaseState.gameControl.KeyCapHighlight(ch, GameColor.Grey);
            }
        }

        if (charWordTraget.SequenceEqual(charWordGuess))
        {
            GameBaseState.stateMachine.ChangeState(new EndGameState(wordData, true));
            return;
        }

        if (_curRow == _maxRow - 1)
        {
            GameBaseState.stateMachine.ChangeState(new EndGameState(wordData, false));
            return;
        }

        // next line
        _curRow++;
        _curColumn = 0;
        _curGuess = "";
    }

    private void CellHighlight(int column, Color color)
    {
        var cell = transform.GetChild(_curRow).GetChild(column);
        cell.GetComponent<Image>().color = color;
        cell.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
    }

    public void ShowTextAnswer(WordData ward)
    {
        TextAnswer.SetActive(true);
        TextAnswer.GetComponentInChildren<TextMeshProUGUI>().text = ward.en.ToUpper();
    }

    public void HideTextAnswer()
    {
        TextAnswer.SetActive(false);
    }
}