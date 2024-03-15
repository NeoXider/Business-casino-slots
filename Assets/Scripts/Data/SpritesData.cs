using UnityEngine;

[CreateAssetMenu(fileName = "SpritesDafualt", menuName = "Slot/Sprites")]
public class SpritesData : ScriptableObject
{
    [SerializeField] private Sprite[] _sprites;
    public Sprite[] sprites => _sprites;
}
