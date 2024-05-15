using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class KeyCap
{
    public KeyCode keyCode;
    public Button button;
}

public class KeyBoard : MonoBehaviour
{
    [SerializeField] private List<KeyCap> keyCaps = new List<KeyCap>();

    public Action<KeyCode> OnKeyPress;

    private void Awake()
    {
        OnKeyCaps();
    }

    void Update()
    {
#if UNITY_EDITOR
        OnKeyBoard();
#endif
    }

    private void OnKeyBoard()
    {
        foreach (var keyCap in keyCaps)
        {
            if (Input.GetKeyDown(keyCap.keyCode))
            {
                OnKeyPress?.Invoke(keyCap.keyCode);
            }
        }
    }

    private void OnKeyCaps()
    {
        foreach (var keyCap in keyCaps)
        {
            keyCap.button.onClick.AddListener(() =>
            {
                OnKeyPress?.Invoke(keyCap.keyCode);
            });
        }
    }

    public void KeyCapHighlight(KeyCode keyCode, Color color)
    {
        var keyCap = keyCaps.Find(e => e.keyCode == keyCode);
        Highlight(keyCap, color);
    }

    public void ClearKeyCapHighlight()
    {
        foreach (var keyCap in keyCaps)
        {
            Highlight(keyCap, Color.white);
        }
    }

    private void Highlight(KeyCap keyCap, Color color)
    {
        var keyCapColor = keyCap.button.transform.GetComponent<Image>().color;
        if (color != Color.white && keyCapColor == GameColor.Green)
        {
            return;
        }

        keyCap.button.transform.GetComponent<Image>().color = color;
    }

    public void active(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
