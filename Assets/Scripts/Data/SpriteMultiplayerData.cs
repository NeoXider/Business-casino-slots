using UnityEngine;

[CreateAssetMenu(fileName = "SpritesMultipliersDafualt", menuName = "Slot/SpritesMult")]
public class SpriteMultiplayerData : ScriptableObject
{
    [System.Serializable]
    public class SpritesMultiplier
    {
        public SpriteMult[] spriteMults;
    }

    [System.Serializable]
    public struct SpriteMult
    {
        public Sprite sprite;
        public CountMultiplayer[] countM;
    }

    [System.Serializable]
    public struct CountMultiplayer
    {
        public int count;
        public int m;
    }

    [SerializeField] private SpritesMultiplier _spritesMultiplier;
    public SpritesMultiplier spritesMultiplier => _spritesMultiplier;
}
