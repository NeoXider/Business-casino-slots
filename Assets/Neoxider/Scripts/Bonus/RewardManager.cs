using System;
using TMPro;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _takeMoneyText;
    [SerializeField] private int secondsToWaitForReward = 60*60*24;
    private const string LastRewardTimeKey = "LastRewardTime";
    public string lastRewardTimeStr;
    public int countMoneyTake = 500;
    

    private void Start()
    {
        if(_takeMoneyText != null)
            _takeMoneyText.text = countMoneyTake.ToString();
    }

    private void Update()
    {
        int seconds = GetSecondsUntilReward();

        if(_timeText != null)
            _timeText.text = FormatTime(seconds);
    }

    private string FormatTime(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return time.ToString(@"hh\:mm\:ss");
    }

    public int GetSecondsUntilReward()
    {
        lastRewardTimeStr = PlayerPrefs.GetString(LastRewardTimeKey, string.Empty);

        if (!string.IsNullOrEmpty(lastRewardTimeStr))
        {
            DateTime lastRewardTime;
            if (DateTime.TryParse(lastRewardTimeStr, out lastRewardTime))
            {
                DateTime currentTime = DateTime.UtcNow;
                TimeSpan timeSinceLastReward = currentTime - lastRewardTime;
                int secondsPassed = (int)timeSinceLastReward.TotalSeconds;
                int secondsUntilReward = secondsToWaitForReward - secondsPassed;

                return secondsUntilReward > 0 ? secondsUntilReward : 0;
            }
        }

        return 0;
    }

    public void GiveReward()
    {
        if (GetSecondsUntilReward() == 0)
        {
            Money.instance.AddMoneyP(countMoneyTake);
            SaveCurrentTimeAsLastRewardTime();
        }
            
    }

    private void SaveCurrentTimeAsLastRewardTime()
    {
        print("Save");
        PlayerPrefs.SetString(LastRewardTimeKey, DateTime.UtcNow.ToString());
    }

    private void OnValidate()
    {
        if(_takeMoneyText != null)
            _takeMoneyText.text = countMoneyTake.ToString();
    }
}
