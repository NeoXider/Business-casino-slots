using UnityEngine;

public class Page : MonoBehaviour
{
    public PageType pageType = PageType.Other;
    [SerializeField] private PageAnim _pageAnim;

    private void OnValidate()
    {
        name = "Page " + pageType.ToString();
        _pageAnim = GetComponent<PageAnim>();
    }

    public virtual void StartActiv()
    {
        if(_pageAnim != null)
        {
            _pageAnim.StartAnim();
        }
    }

    public virtual void EndActiv()
    {

    }
}
