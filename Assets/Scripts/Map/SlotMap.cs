using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SlotMap : MonoBehaviour
{
    [SerializeField] private GameObject[] _slot;
    [SerializeField] private TextMeshProUGUI[] _nameSlotText;
    [SerializeField] private TextMeshProUGUI[] _priceSlotText;
    [Space]
    [Header("Setting")]
    [DoNotSerialize] public int id;
    public Map map;
    public string nameSlot = "Clasic";
    public int priceSlot = 1000;
    public bool activ = false;

    public void SetVisual(string name, int price)
    {
        nameSlot = name;
        priceSlot = price;
        Visual();
    }

    public void Buy()
    {
        map.Buy(id);
    }

    public void Open()
    {
        map.Open(id);
    }

    private void Visual()
    {
        if (activ)
        {
            _slot[0].SetActive(true);
            _slot[1].SetActive(false);
        }
        else
        {
            _slot[0].SetActive(false);
            _slot[1].SetActive(true);
        }

        for (int i = 0; i < _nameSlotText.Length; i++)
        {
            _nameSlotText[i].text = nameSlot;
            _priceSlotText[i].text = priceSlot.ToString();
        }
    }

    private void OnValidate()
    {
        Visual();
    }
}
