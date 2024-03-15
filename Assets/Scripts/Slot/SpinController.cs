using System.Linq;
using TMPro;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    [SerializeField] private CheckSpin _checkSpin;
    [SerializeField] private LinesSlot _lineSlot;
    [SerializeField] private BetsLineData _betsLineData;
    [SerializeField] private SpritesData _allSpritesData;
    [SerializeField] public int countRowElements = 3;
    [SerializeField] private Row[] _rows;
    [SerializeField] private Sprite[,] _spritesEnd;

    [SerializeField] private TextMeshProUGUI _textCountLine;
    [SerializeField] private TextMeshProUGUI _textLineBet;
    [SerializeField] private TextMeshProUGUI _textMoneyWin;

    [Range(0f, 1f)]
    public float chanseWin = 0.5f;

    private int countLine = 1;
    private int lineBet = 1;

    private int price;

    private void Start()
    {
        lineBet = 0;
        countLine = 1;

        for (int i = 0; i < _rows.Length; i++)
        {
            _rows[i].SetSprites(_allSpritesData.sprites[0]);
        }

        SetPrice();
        _lineSlot.LineActiv(false);
    }

    private void SetPrice()
    {
        price = countLine * _betsLineData.bets[lineBet];

        _textCountLine.text = _betsLineData.bets[lineBet].ToString();
        _textLineBet.text = countLine.ToString();

        int[] sequence = Enumerable.Range(0, countLine).ToArray();
        _lineSlot.LineActiv(sequence);
    }

    public void AddLine()
    {
        if (IsStop())
        {
            countLine++;

            if (countLine > _lineSlot.lines.Length)
            {
                countLine = 1;
            }

            SetPrice();
        }
    }

    public void RemoveLine()
    {
        if (IsStop())
        {
            countLine--;

            if (countLine < 1)
            {
                countLine = _lineSlot.lines.Length;
            }

            SetPrice();
        }
    }

    public void AddBet()
    {
        if (IsStop())
        {
            lineBet++;

            if (lineBet >= _betsLineData.bets.Length)
            {
                lineBet = 0;
            }

            SetPrice();
        }
    }

    public void RemoveBet()
    {
        if (IsStop())
        {
            lineBet--;

            if (lineBet < 0)
            {
                lineBet = _betsLineData.bets.Length - 1;
            }

            SetPrice();
        }
    }

    private void Update()
    {

    }

    private void Win(int[] lines, int[] mult)
    {
        _lineSlot.LineActiv(lines);
        int moneyWin = 0;
        
        for (int i = 0; i < mult.Length; i++)
        {
            moneyWin += mult[i] * _betsLineData.bets[lineBet];
        }

        _textMoneyWin.text = moneyWin.ToString();
        Money.instance.AddMoneyS(moneyWin);
    }

    private void Lose()
    {
        _textMoneyWin.text = 0.ToString();
    }

    public void StartSpin()
    {
        if (IsStop())
            if (Money.instance.SpendStandart(price))
            {
                _textMoneyWin.text = "";
                _lineSlot.LineActiv(false);

                CreateRandomSprites();
                print(nameof(StartSpin));

                for (int x = 0; x < _rows.Length; x++)
                {
                    Sprite[] rowSprite = new Sprite[countRowElements];

                    for (int y = 0; y < countRowElements; y++)
                    {
                        rowSprite[y] = _spritesEnd[x, y];
                    }

                    _rows[x].Spin(_allSpritesData.sprites, rowSprite);
                }
            }
    }

    private void CreateRandomSprites()
    {
        _spritesEnd = new Sprite[_rows.Length, countRowElements];

        for (int x = 0; x < _spritesEnd.GetLength(0); x++)
            for (int y = 0; y < _spritesEnd.GetLength(1); y++)
            {
                Sprite randSprite = _allSpritesData.sprites[Random.Range(0, _allSpritesData.sprites.Length)];
                _spritesEnd[x, y] = randSprite;
            }

        if (Random.Range(0, 1f) < chanseWin)
        {
            print(nameof(Win));
            _checkSpin.SetWin(_spritesEnd, _allSpritesData.sprites, lineBet);
        }
        else
        {
            print(nameof(Lose));
            _checkSpin.SetLose(_spritesEnd, _checkSpin.GetWinningLines(_spritesEnd, countLine), _allSpritesData.sprites, countLine);
        }

    }

    private bool IsStop()
    {
        for (int i = 0; i < _rows.Length; i++)
        {
            if (_rows[i].is_spinnung)
                return false;
        }
        return true;
    }

    private void CheckWin()
    {
        if (IsStop())
        {
            int[] lines = _checkSpin.GetWinningLines(_spritesEnd, countLine);
            int countWin = lines.Length;
            int[] mult = _checkSpin.GetMultiplayers(_spritesEnd, countLine);

            if (countWin > 0)
                Win(lines, mult);
            else if (countWin == 0)
                Lose();
        }
    }

    private void OnEnable()
    {
        _rows.Last().OnStop.AddListener(CheckWin);
    }

    private void OnDisable()
    {
        _rows.Last().OnStop.RemoveListener(CheckWin);
    }

    private void OnValidate()
    {
        for (int i = 0; i < _rows.Length; i++)
        {
            _rows[i].SetSprites(_allSpritesData.sprites[0]);
        }
    }
}
