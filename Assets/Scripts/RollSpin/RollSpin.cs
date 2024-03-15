using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RollSpin : MonoBehaviour
{
    [System.Serializable]
    public struct Bonus
    {
        public MoneyType moneyType;
        public float mult;
    }

    public enum MoneyType
    {
        Standart,
        Premium
    }

    [SerializeField] private TextMeshProUGUI _textCountSpin;
    [SerializeField] private Transform _roll;
    [SerializeField] private Bonus[] _bonus;
    [SerializeField] private float _speedRotate = 360;
    [SerializeField] private float _timeRotate = 2;
    [SerializeField] private float _startBonusRotate = -15f;

    public int countSpin;
    public int moneyBonus = 100;

    private int _currentSpeed;
    private bool isSpinning;

    private void Start()
    {
        countSpin = PlayerPrefs.GetInt(nameof(countSpin), countSpin);
        _textCountSpin.text = countSpin.ToString();
    }

    public void Spin()
    {
        if (countSpin > 0 && !isSpinning)
        {
            countSpin--;
            StartCoroutine(RollCoroutine());
            _textCountSpin.text = countSpin.ToString();
            PlayerPrefs.SetInt(nameof(countSpin), countSpin);
        }
    }

    private IEnumerator RollCoroutine()
    {
        isSpinning = true;
        float timeSpent = 0;
        float initialSpeed = _speedRotate + UnityEngine.Random.Range(-100, 100); 
        float totalRotationTime = _timeRotate + UnityEngine.Random.Range(-1f, 1f);

        while (timeSpent < totalRotationTime)
        {
            float currentSpeed = Mathf.Lerp(initialSpeed, 0, timeSpent / totalRotationTime);
            _roll.Rotate(0, 0, currentSpeed * Time.deltaTime);
            timeSpent += Time.deltaTime;
            yield return null;
        }

        DetermineBonus();
        isSpinning = false;
    }

    private void DetermineBonus()
    {
        float angle = _roll.eulerAngles.z - _startBonusRotate;
        angle = (360 + angle) % 360;
        float range = 360 / _bonus.Length;
        int bonusIndex = Mathf.FloorToInt(angle / range);
        bonusIndex = Mathf.Clamp(bonusIndex, 0, _bonus.Length - 1);

        Win(bonusIndex);
    }

    private void Win(int id)
    {
        print(nameof(Win)+ id);
        MoneyType money = _bonus[id].moneyType;
        int countMoney = (int)(_bonus[id].mult * moneyBonus);
        print("win = " + money.ToString() + " mult = " + _bonus[id].mult.ToString());

        switch (money)
        {
            case MoneyType.Standart:
                Money.instance.AddMoneyS(countMoney);
                break;
            case MoneyType.Premium:
                Money.instance.AddMoneyP(countMoney);
                break;
        }
    }
}
