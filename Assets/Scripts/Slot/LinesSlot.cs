using UnityEngine;

public class LinesSlot : MonoBehaviour
{
    public GameObject[] lines;

    public void LineActiv(int[] idList)
    {
        foreach (GameObject item in lines)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < idList.Length; i++)
        {
            lines[idList[i]].SetActive(true);
        }
    }

    public void LineActiv(bool activ)
    {
        foreach (GameObject item in lines)
        {
            item.SetActive(activ);
        }
    }
}
