using UnityEngine;
using UnityEngine.Events;

public class LevelMoney : MonoBehaviour
{
    [SerializeField] private const string _moneySave = "MoneyStandart";

    public int money;
    public int levelMoney;

    public UnityEvent<int> OnChangedLevelMoney;
    public UnityEvent<int> OnChangedMoney;

    internal void AddLevelMoney(int count)
    {
        levelMoney += count;
        OnChangedLevelMoney?.Invoke(levelMoney);
    }

    internal void AddMoney(int count)
    {
        money += count;
        Save();
        OnChangedMoney?.Invoke(money);
    }

    internal void ResetLevelMoney()
    {
        levelMoney = 0;
        OnChangedLevelMoney?.Invoke(levelMoney);
    }

    internal void SetMoney()
    {
        money += levelMoney;
        OnChangedMoney?.Invoke(money);
        Save();
    }

    internal bool SpendMoney(int count)
    {
        if (CheckSpend(count))
        {
            money -= count;
            OnChangedMoney?.Invoke(money);
            Save();
            return true;
        }

        return false;
    }

    internal bool CheckSpend(int count)
    {
        return money >= count;
    }

    void Start()
    {
        Load();
        OnChangedMoney?.Invoke(money);
        ResetLevelMoney();
    }

    private void Load()
    {
        money = PlayerPrefs.GetInt(_moneySave, money);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(_moneySave, money);
    }
}
