using UnityEngine;

public class Slots : MonoBehaviour
{
    [SerializeField] private GameObject[] slots;
    [SerializeField] private GameObject[] uiSlots;

    private void Start()
    {
        SetSlot(-1);
    }

    public void SetSlot(int id)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetActive(i == id);
            uiSlots[i].SetActive(i == id);
        }
    }
}
