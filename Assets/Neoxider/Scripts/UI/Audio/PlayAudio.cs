using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudio : MonoBehaviour
{
    [SerializeField]
    private Button button;

    private AudioManager am;

    private void Start()
    {
        am = AudioManager.Instance;
        
    }

    private void OnEnable()
    {
        button.onClick.AddListener(AudioPlay);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(AudioPlay);
    }

    private void AudioPlay()
    {
        print("button clip");
        
        if (am != null)
        {
            am.PlayGameSound();
        }
    }

    private void OnValidate()
    {
        button = GetComponent<Button>();
    }
}
