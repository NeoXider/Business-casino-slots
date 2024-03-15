using System;
using UnityEngine;
using UnityEngine.Events;

public class Money : MonoBehaviour
{
    [SerializeField] private int _moneyStandart = 0;
    [SerializeField] private int _moneyPremium = 0;

    public static Money instance;

    public UnityEvent<int> OnChangeMoneyStandart = new UnityEvent<int>();
    public UnityEvent<int> OnChangeMoneyPremium = new UnityEvent<int>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Load();
    }

    public void AddMoneyS(int count)
    {
        _moneyStandart += count;
        OnChangeMoneyStandart?.Invoke(_moneyStandart);
        PlayerPrefs.SetInt(nameof(_moneyStandart), _moneyStandart);
    }

    public void AddMoneyP(int count)
    {
        _moneyPremium += count;
        OnChangeMoneyPremium?.Invoke(_moneyPremium);
        PlayerPrefs.SetInt(nameof(_moneyPremium), _moneyPremium);
    }

    public bool SpendStandart(int count, bool spend = true)
    {
        if (_moneyStandart >= count)
        {
            if(spend)
            {
                _moneyStandart -= count;
                OnChangeMoneyStandart?.Invoke(_moneyStandart);
                PlayerPrefs.SetInt(nameof(_moneyStandart), _moneyStandart);
            }
            
            return true;
        }

        return false;
    }

    public bool SpendPremium(int count, bool spend = true)
    {
        if (_moneyPremium >= count)
        {
            if (spend)
            {
                _moneyPremium -= count;
                OnChangeMoneyPremium?.Invoke(_moneyPremium);
                PlayerPrefs.SetInt(nameof(_moneyPremium), _moneyPremium);
            }

            return true;
        }

        return false;
    }

    private void Load()
    {
        _moneyStandart = PlayerPrefs.GetInt(nameof(_moneyStandart), _moneyStandart);
        _moneyPremium = PlayerPrefs.GetInt(nameof(_moneyPremium), _moneyPremium);

        OnChangeMoneyStandart?.Invoke(_moneyStandart);
        OnChangeMoneyPremium?.Invoke(_moneyPremium);
    }

    internal bool SpendStandart(object price)
    {
        throw new NotImplementedException();
    }
}
