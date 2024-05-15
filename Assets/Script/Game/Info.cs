using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    [SerializeField] private GameObject infoPage;
    [SerializeField] private Text infoText;

    [Header("Button")]
    [SerializeField] private Button openPage;
    [SerializeField] private Button closePage;

    public bool isShowPage { get; private set; }

    private void Awake()
    {
        OnMessage();
    }

    private void OnMessage()
    {
        openPage.onClick.AddListener(OnOpen);
        closePage.onClick.AddListener(OnClose);
    }

    private void OnOpen()
    {
        ShowInfo();
    }

    private void OnClose()
    {
        infoPage.SetActive(false);
        isShowPage = false;
    }

    public void ShowInfo()
    {
        var wordData = GameBaseState.gameControl.gridWord.wordData;
        if (wordData != null)
        {
            infoText.text = wordData.info;
            infoPage.SetActive(true);
            isShowPage = true;
        }
    }
}