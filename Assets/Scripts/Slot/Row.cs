using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Row : MonoBehaviour
{
    [SerializeField] private SlotElement _slotPrefab;

    public int countSlotElement = 3;
    [Header("SlotElement x2")] public List<SlotElement> SlotElements;
    public bool is_spinnung { get => _isSpinning; }
    public float speed = 5f;
    public float timeSpin = 3f;
    [Range(0f, 5f)]
    public float endSpeed = 2f;

    [SerializeField] private float _spaceY = 1;
    [SerializeField] private float _resetPosition;
    [SerializeField] private float[] yPositions;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Sprite[] _spritesEnd;

    [SerializeField] private float _timerSpin;
    [SerializeField] private bool _isSpinning = false;

    public UnityEvent OnStop = new UnityEvent();

    void Update()
    {

    }

    private IEnumerator SpinCoroutine()
    {
        _isSpinning = true;
        _timerSpin = 0;

        while (true)
        {
            if (_timerSpin > timeSpin
            && SlotElements[countSlotElement - 1].transform.position.y > yPositions[SlotElements.Count - 2])
            {
                break;
            }

            _timerSpin += Time.deltaTime;

            for (int i = 0; i < SlotElements.Count; i++)
            {
                SlotElement slot = SlotElements[i];
                slot.transform.Translate(Vector3.down * speed * Time.deltaTime);

                if (slot.transform.position.y <= _resetPosition)
                {
                    float spawnY = GetLastY() + _spaceY;
                    slot.transform.position = new Vector3(slot.transform.position.x, spawnY, slot.transform.position.z);
                    slot.SetSprite(GetRandomSprite());
                }
            }

            yield return null;
        }

        SetSpriteEnd();
        float newSpeed = speed;

        while (true)
        {
            bool shouldStop = false;
            for (int i = 0; i < SlotElements.Count; i++)
            {
                SlotElement slot = SlotElements[i];
                slot.transform.Translate(Vector3.down * newSpeed * Time.deltaTime);

                if (i == 0 && Mathf.Abs(slot.transform.position.y - yPositions[0]) < 0.02f)
                {
                    SetStartPositions();
                    shouldStop = true;
                    break;
                }

                if (slot.transform.position.y <= _resetPosition)
                {
                    float newYPosition = yPositions[yPositions.Length - 1] + (yPositions[0] - _resetPosition);
                    slot.transform.position = new Vector3(slot.transform.position.x, newYPosition, slot.transform.position.z);
                }
            }

            newSpeed = Mathf.Max(newSpeed - endSpeed * speed * Time.deltaTime, 0.5f);

            if (shouldStop)
            {
                break;
            }

            yield return null;
        }

        _isSpinning = false;
        OnStop?.Invoke();
    }

    private float GetLastY()
    {
        return SlotElements.Max(slotElement => slotElement.transform.position.y);
    }

    private void SetSpriteEnd()
    {
        for (int i = 0; i < countSlotElement; i++)
        {
            SlotElements[i].SetSprite(_spritesEnd[i]);
        }
    }

    private void SetStartPositions()
    {
        for (int j = 0; j < SlotElements.Count; j++)
        {
            SlotElements[j].transform.position = new Vector2(transform.position.x, yPositions[j]);
        }
    }

    private Sprite GetRandomSprite()
    {
        return _sprites[Random.Range(0, _sprites.Length)];
    }

    public void Spin(Sprite[] allSprites, Sprite[] sprites)
    {
        _sprites = allSprites;

        for (int i = 0; i < _spritesEnd.Length; i++)
        {
            _spritesEnd[i] = sprites[i];
        }

        for (int i = SlotElements.Count - 1; i >= countSlotElement; i--)
        {
            SlotElements[i].SetSprite(GetRandomSprite());
        }

        StartCoroutine(SpinCoroutine());
    }

    public void Stop()
    {
        _isSpinning = false;
    }

    private void OnValidate()
    {
        yPositions = new float[countSlotElement * 2];
        _resetPosition = transform.position.y - _spaceY;

        if (_spritesEnd.Length != countSlotElement)
            _spritesEnd = new Sprite[countSlotElement];

        if (countSlotElement * 2 > SlotElements.Count)
        {
            for (int i = 0; i < countSlotElement * 2 - SlotElements.Count; i++)
            {
                SlotElements.Add(Instantiate(_slotPrefab, transform));
            }
        }

        for (int i = 0; i < SlotElements.Count; i++)
        {
            yPositions[i] = transform.position.y + _spaceY * i;
        }

        SetStartPositions();
    }

    internal void SetSprites(Sprite sprite)
    {
        foreach (var s in SlotElements)
        {
            s.SetSprite(sprite);
        }
    }
}
