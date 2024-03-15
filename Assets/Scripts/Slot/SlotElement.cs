using UnityEngine;

public class SlotElement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _sprite;
    public Sprite sprite { get => _sprite; set => SetSprite(value); }

    internal void SetSprite(Sprite sprite)
    {
        _sprite = sprite;
        _spriteRenderer.sprite = sprite;
    }
}
