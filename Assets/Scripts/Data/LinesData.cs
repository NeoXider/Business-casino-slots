using UnityEngine;

[CreateAssetMenu(fileName = "LinesDafualt", menuName = "Slot/Lines")]
public class LinesData : ScriptableObject
{
    [System.Serializable]
    public class InnerArray
    {
        public int[] corY;
    }

    [SerializeField] private InnerArray[] _lines;
    public InnerArray[] lines => _lines;
}
