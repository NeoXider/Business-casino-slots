using System.Collections;
using TMPro;
using UnityEngine;

public class Roulette : MonoBehaviour
{
    [System.Serializable]
    public struct Bonus
    {
        public float money;
    }

    [SerializeField] private TextMeshProUGUI _textCountSpin;
    [SerializeField] private Transform _roll;
    [SerializeField] private Bonus[] _bonus;
    [SerializeField] private float _speedRotate = 400;
    [SerializeField] private float _timeRotate = 5;
    [SerializeField] private float _startBonusRotate = -10f;

    public int countSpin = 1;

    private bool isSpinning;

    private void Start()
    {
        countSpin = PlayerPrefs.GetInt(nameof(countSpin), countSpin);
    }

    public void Spin()
    {
        if (countSpin > 0 && !isSpinning)
        {
            countSpin--;
            StartCoroutine(RollCoroutine());
            PlayerPrefs.SetInt(nameof(countSpin), countSpin);
        }
    }

    private IEnumerator RollCoroutine()
    {
        isSpinning = true;
        float timeSpent = 0;
        float initialSpeed = _speedRotate + Random.Range(-100, 100);
        float totalRotationTime = _timeRotate + Random.Range(-1f, 1f);

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
        int countMoney = (int)_bonus[id].money;
        print("win = " + countMoney.ToString());
    }
}
