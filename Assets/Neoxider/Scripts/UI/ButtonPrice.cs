using TMPro;
using UnityEngine;

public class ButtonPrice : MonoBehaviour
{
    public enum ButtonType
    {
        Choosen,
        Choose,
        Buy
    }

    [SerializeField] private int price = 1000;
    [SerializeField] private bool price_0 = false;
    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private ButtonType type = ButtonType.Buy;
    [SerializeField] private GameObject[] visualButton;

    public void SetVisual(ButtonType bType, int price)
    {
        if (!price_0 && bType == ButtonType.Buy && price == 0)
            bType = ButtonType.Choose;

        if (price > 0 && ButtonType.Buy != bType)
        {
            Debug.LogWarning("Не правильный тип");
            bType = ButtonType.Buy;
        }


        type = bType;
        this.price = price;
        textPrice.text = price.ToString("N0");

        switch (type)
        {
            case ButtonType.Choosen:
                Visual(0);
                break;
            case ButtonType.Choose:
                Visual(1);
                break;
            case ButtonType.Buy:
                Visual(2);
                break;
        }
    }

    private void Visual(int id)
    {
        for (int i = 0; i < visualButton.Length; i++)
        {
            visualButton[i].SetActive(i == id);
        }
    }

    private void OnValidate()
    {
        SetVisual(type, price);
    }
}
