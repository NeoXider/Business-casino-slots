using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countMoneyText;
    [SerializeField] private Image _icoMoney;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _priceImage;
    
    public Shop shop;
    public int id;
    public bool standart;

    public void SetVisual(int count, Sprite icoMoney, 
        Sprite spriteItem, int price, Sprite priceImage)
    {
        _countMoneyText.text = count.ToString();
        _icoMoney.sprite = icoMoney;
        _itemImage.sprite = spriteItem;
        _priceText.text = price.ToString();
        _priceImage.sprite = priceImage;
    }

    public void Buy()
    {
        if (standart)
            shop.BuyS(id);
        else
            shop.BuyP(id);
    }
}
