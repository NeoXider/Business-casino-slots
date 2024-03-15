using UnityEngine;

[CreateAssetMenu(fileName = "BetsDafualt", menuName = "Slot/Bets")]
public class BetsLineData : ScriptableObject
{
    [SerializeField] private int[] _bets;

    public int[] bets => _bets;
}

