using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] private ScreenManager _screenManager;
    [SerializeField] private Slots _slots;
    [SerializeField] private Image _imageSlot;
    [SerializeField] private TextMeshProUGUI _textNameSlot;

    
    [SerializeField] private SlotMap[] _slotMaps;
    [SerializeField] private string[] _nameSlots;
    [SerializeField] private int[] _priceSlots;
    [SerializeField] private Sprite[] _spritesSlots;

    [Space]
    public int currentIdSlot;

    [Header("Grade")]
    [SerializeField] private GradeSlot[] _gradeSlot;
    [SerializeField] private TextMeshProUGUI _takeProfitText;
    [SerializeField] private TextMeshProUGUI _priceGradeText;
    [SerializeField] private TextMeshProUGUI _profitPerSecondsText;
    [SerializeField] private TextMeshProUGUI _nextProfitPerSecondsText;
    [SerializeField] private TextMeshProUGUI _maxProfitText;
    [SerializeField] private TextMeshProUGUI _nextMaxProfitText;
    [SerializeField] private Image _btnImage;
    [SerializeField] private Sprite[] _btnSpriteGrade;

    void Start()
    {
        Load();
        Visual();
    }

    private void Update()
    {
        VisualSlotGrade();
    }

    private void VisualSlotGrade()
    {
        int price = _gradeSlot[currentIdSlot].GetPrice();

        _takeProfitText.text = _gradeSlot[currentIdSlot].GetCurrentProfit().ToString();
        _priceGradeText.text = price.ToString();
        _profitPerSecondsText.text = _gradeSlot[currentIdSlot].GetProfitPerSecond().ToString() + "/sec";
        _nextProfitPerSecondsText.text = _gradeSlot[currentIdSlot].GetProfitPerSecond(1).ToString() + "/sec";
        _maxProfitText.text = _gradeSlot[currentIdSlot].GetMaxProfit().ToString();
        _nextMaxProfitText.text = _gradeSlot[currentIdSlot].GetMaxProfit(1).ToString();

        _btnImage.sprite = Money.instance.SpendPremium(price, false) ? _btnSpriteGrade[0] : _btnSpriteGrade[1];
    }

    public void Buy(int id)
    {
        if(Money.instance.SpendPremium(_priceSlots[id]))
        {
            _priceSlots[id] = 0;
            _gradeSlot[id].Upgrade();
            PlayerPrefs.SetInt(nameof(_priceSlots) + id, _priceSlots[id]);
            Visual();
        }
    }

    public void Open(int id)
    {
        currentIdSlot = id;
        _screenManager.ChangePage(PageType.Grade);
        _imageSlot.sprite = _spritesSlots[id];
        _textNameSlot.text = _nameSlots[id];
    }

    public void Play()
    {
        PlaySlot(currentIdSlot);
        _screenManager.SetPage(PageType.Game);
    }

    public void Upgrade()
    {
        int price = _gradeSlot[currentIdSlot].GetPrice();

        if (Money.instance.SpendPremium(price))
            _gradeSlot[currentIdSlot].Upgrade();
    }

    public void Take()
    {
        int count = _gradeSlot[currentIdSlot].TakeMoney();
        Money.instance.AddMoneyS(count);
    }

    private void PlaySlot(int id)
    {
        _slots.SetSlot(id);
    }

    private void Load()
    {
        for (int i = 0; i < _priceSlots.Length; i++)
        {
            _priceSlots[i] = PlayerPrefs.GetInt(nameof(_priceSlots) + i, _priceSlots[i]);
        }
    }

    private void Visual()
    {
        for (int i = 0; i < _slotMaps.Length; i++)
        {
            _slotMaps[i].activ = _priceSlots[i] == 0;
            _slotMaps[i].SetVisual(_nameSlots[i], _priceSlots[i]);
        }
    }

    private void OnValidate()
    {
        for (int i = 0; i < _slotMaps.Length; i++)
        {
            _slotMaps[i].id = i;
            _slotMaps[i].map = this;
            _slotMaps[i].SetVisual(_nameSlots[i], _priceSlots[i]);
        }

        for (int i = 0; i < _gradeSlot.Length; i++)
        {
            _gradeSlot[i].slotName = _nameSlots[i];
        }
    }
}
