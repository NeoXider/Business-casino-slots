using Unity.Mathematics;
using UnityEngine;

public class GradeSlot : MonoBehaviour
{
    public string slotName = "Slot";

    [SerializeField] private int _levelSlot;
    [SerializeField] private int _maxProfit = 50;
    [SerializeField] private float _currentProfit;
    [SerializeField] private float _profitPerSecond = 1;

    [SerializeField] private int _upgradePrice = 500;
    [SerializeField] private float _addPricePerLevel = 1.3f;


    void Start()
    {
        LoadData();
    }

    void Update()
    {
        if (_levelSlot >= 1)
        {
            if (_currentProfit < GetMaxProfit())
                _currentProfit += GetProfitPerSecond() * Time.deltaTime;
            else
                _currentProfit = GetMaxProfit();
        }
    }

    public int GetProfitPerSecond(int addLevel = 0)
    {
        return (int)(_profitPerSecond * (_levelSlot + addLevel));
    }

    public int GetMaxProfit(int addLevel = 0)
    {
        return _maxProfit * (_levelSlot + addLevel);
    }

    public int GetPrice()
    {
        if (_levelSlot == 0)
            return _upgradePrice;

        return (int)(_upgradePrice * math.pow(_addPricePerLevel, _levelSlot));
    }

    public int GetCurrentProfit()
    {
        return (int)_currentProfit;
    }

    public int TakeMoney()
    {
        int money = (int)_currentProfit;
        _currentProfit = 0;
        return money;
    }

    public void Upgrade()
    {
        _levelSlot++;
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(nameof(_levelSlot) + slotName, _levelSlot);
        PlayerPrefs.SetInt(nameof(_currentProfit) + slotName, (int)_currentProfit);
    }

    public void LoadData()
    {
        _levelSlot = PlayerPrefs.GetInt(nameof(_levelSlot) + slotName, _levelSlot);
        _currentProfit = PlayerPrefs.GetInt(nameof(_currentProfit) + slotName, (int)_currentProfit);
    }

    void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnValidate()
    {
        name = slotName + " slot grade";
    }
}
