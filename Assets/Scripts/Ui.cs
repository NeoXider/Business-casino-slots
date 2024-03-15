using TMPro;
using UnityEngine;

public class Ui : MonoBehaviour, IScreenManagerSubscriber
{
    [Header("Main Settings")]
    [SerializeField] private ScreenManager _screenManager;
    [SerializeField] private GameObject _canvasMenu;

    [Header("Game")]

    [Header("Money")]
    [SerializeField] private TextMeshProUGUI[] _textMoneyStandart;
    [SerializeField] private TextMeshProUGUI[] _textMoneyPremium;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void OnChangePage(Page newPage)
    {
        if (newPage.pageType == PageType.Game)
        {
            print("ScreenOrientation.LandscapeLeft");
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            _canvasMenu.SetActive(false);
            _screenManager.FindPage(PageType.Map).gameObject.SetActive(false);
        }
        else
        {
            print("ScreenOrientation.Portrait");
            Screen.orientation = ScreenOrientation.Portrait;
            _canvasMenu.SetActive(true);
            _screenManager.FindPage(PageType.Map).gameObject.SetActive(true);
        }
    }

    public void UpdateText(TextMeshProUGUI[] text, int money)
    {
        foreach (var item in text)
            item.text = money.ToString();
    }

    public void UpdateTextStandart(int money)
    {
        UpdateText(_textMoneyStandart, money);
    }

    public void UpdateTextPremium(int money)
    {
        UpdateText(_textMoneyPremium, money);
    }
}
