using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    //[System.Serializable]
    //public struct AudioData
    //{
    //    AudioClip clip;
    //    ClipType type;
    //    //SourseType sourseType;
    //}
    //[System.Serializable]
    //public enum SourseType
    //{
    //    Interface,
    //    Music,
    //    Game  
    //}

    public AudioSource musicSource;
    public AudioSource gameSoundSource;
    public AudioSource interfaceSource;

    //[SerializeField]
    //public AudioData[] audioData;
    [Space]

    public AudioClip[] delet;
    public AudioClip fakeMove;
    public AudioClip bonusBoom;
    public AudioClip bonusLight;
    public AudioClip bonusCleer;
    public AudioClip bonusUp;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip buttonClickClip;
    

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGameSound(AudioClip clip = null, float volume = 1f, AudioSource source = null, Transform transform = null)
    {
        if (transform != null)
        {
            source.transform.position = transform.position;
        }

        if (source == null)
        {
            if (clip == null)
            {
                source = interfaceSource;
                clip = buttonClickClip;
            }
            else
                source = gameSoundSource;

            //    source.PlayOneShot(clip, volume);
        }
        //else
        // {
        source.PlayOneShot(clip, volume);
        // }
    }

    public void PlayRandomElement()
    {
        gameSoundSource.PlayOneShot(delet[Random.Range(0, delet.Length)]);
    }

    public void MuteAll(bool enable)
    {
        musicSource.mute = !enable;
        gameSoundSource.mute = !enable;
        interfaceSource.mute = !enable;
    }

    public void MuteMusic(bool enable)
    {
        musicSource.mute = !enable;
    }

    public void MuteEfx(bool enable)
    {
        interfaceSource.mute = !enable;
        gameSoundSource.mute = !enable;
    }
}
