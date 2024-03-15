using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Money _money;
    [SerializeField] private ItemShop[] _itemShopsS;
    [SerializeField] private ItemShop[] _itemShopsP;

    [SerializeField] private int[] _price;
    [SerializeField] private int[] _count;
    [SerializeField] private Sprite _icoS;
    [SerializeField] private Sprite _icoP;
    [SerializeField] private Sprite[] _spriteS;
    [SerializeField] private Sprite[] _spriteP;

    void Start()
    {
        Visual();
    }

    public void BuyS(int id)
    {
        if (_money.SpendPremium(_price[id]))
        {
            _money.AddMoneyS(_count[id]);
        }
    }

    public void BuyP(int id)
    {
        if (_money.SpendStandart(_price[id]))
        {
            _money.AddMoneyP(_count[id]);
        }
    }

    private void Visual()
    {
        for (int i = 0; i < _itemShopsS.Length; i++)
        {
            _itemShopsS[i].SetVisual(_count[i], _icoS, _spriteS[i], _price[i], _icoP);
        }

        for (int i = 0; i < _itemShopsP.Length; i++)
        {
            _itemShopsP[i].SetVisual(_count[i], _icoP, _spriteP[i], _price[i], _icoS);
        }
    }

    private void OnValidate()
    {
        for (int i = 0; i < _itemShopsS.Length; i++)
        {
            _itemShopsS[i].id = i;
            _itemShopsS[i].shop = this;
            _itemShopsS[i].standart = true;
        }

        for (int i = 0; i < _itemShopsP.Length; i++)
        {
            _itemShopsP[i].id = i;
            _itemShopsP[i].shop = this;
            _itemShopsP[i].standart = false;
        }

        Visual();
    }
}
