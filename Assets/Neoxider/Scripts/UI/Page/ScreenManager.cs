//v.1.0.3
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public interface IScreenManagerSubscriber
{
    void OnChangePage(Page newPage);
}

public class ScreenManager : MonoBehaviour
{
    [Header("Additionally UI func")]
    public UIReady uiReady;

    [Header("All Pages")]
    public Page[] pages;

    //игнорирование страницы (всегда включена или всегда выключена)
    [Header("Ignore Page Type")]
    public PageType[] ignorePageTypes;
    public bool ignorePageActiv;

    //при включение определенной страницы что будет с предыдущей
    [Header("Page last with Change")]
    public PageType[] onePageTypes;
    public bool lastPageActiv = true;

    //включение только 1 страницы и выключение остальных
    [Header("Page with Only Set")]
    public PageType[] setPageTypes;

    //одновременное включение страниц
    [Header("Pages Together")]

    //активные страницы
    [Header("Pages Used")]
    public Page activPage;
    public Page lastPage;

    //начальные настройки
    [Header("Start Settings")]
    [SerializeField]
    private PageType _startPage = PageType.Menu;

    [Header("Page Change Event")]
    public UnityEvent<Page> onPageChanged;

    [Header("Editor Only")]
    [SerializeField]
    private bool _refresh = true;

    [SerializeField]
    private PageType _activPageEditor;


    private void Start()
    {
        SetPage(_startPage);
        lastPage = null;
    }

    public void ChangePage(PageType page, bool lastPageDisable = true)
    {
        if (Array.Exists(setPageTypes, element => element == page))
        {
            SetPage(page);
        }

        if (activPage != null && page == activPage.pageType)
            return;

        lastPage = activPage;

        if (page == PageType.None)
        {
            PagesActivate(PageType.None, false);
            activPage = null;
            return;
        }

        activPage = FindPage(page);

        if (activPage == null)
        {
            Debug.LogError("Change page null PageType: " + page.ToString());
        }

        if (lastPage != null)
            if (lastPageDisable && lastPage.gameObject.activeSelf)
                if (Array.Exists(onePageTypes, element => element == activPage.pageType))
                    SetActiv(lastPage, lastPageActiv);
                else
                    SetActiv(lastPage, false);

        if (!activPage.gameObject.activeSelf)
            SetActiv(activPage, true);

        onPageChanged?.Invoke(activPage);
    }

    public void SwitchLastPage(bool lastPageActiv = false)
    {
        Page _page = lastPage;
        lastPage = activPage;
        activPage = _page;
        SetActiv(lastPage, lastPageActiv);
        SetActiv(activPage, true);

        onPageChanged?.Invoke(activPage);
    }

    public void SetPage(PageType page)
    {
        lastPage = activPage;
        activPage = PagesActivate(page);
    }

    public Page FindPage(PageType page)
    {
        Page _page = null;

        foreach (var item in pages)
        {
            if (item.pageType == page)
            {
                _page = item;
                break;
            }
        }

        return _page;
    }

    public Page PagesActivate(PageType targetPage, bool activ = true,
        PageType ignorPage = PageType.None, bool ignorActiv = false, bool otherActiv = false)
    {
        Page _page = null;

        foreach (var item in pages)
        {
            if (item.pageType == targetPage)
            {
                _page = item;
                SetActiv(_page, activ);
                continue;
            }
            else
            {
                SetActiv(item, otherActiv);
            }

            if (ignorPage == item.pageType)
            {
                SetActiv(item, ignorActiv);
            }
        }

        SetActivIgnorePageSettings();
        onPageChanged?.Invoke(_page);
        return _page;
    }

    public static Page[] FindAllPages()
    {
        var _allPages = Resources.FindObjectsOfTypeAll<Page>();
        var _scenePages = new List<Page>();

        foreach (var page in _allPages)
        {
            if (page.gameObject.scene.name != null)
            {
                _scenePages.Add(page);
            }
        }

        return _scenePages.ToArray();
    }

    private void SetActiv(Page page, bool activ, bool SendPage = true)
    {
        if (page == null)
        {
            Debug.LogWarning("null Page");
            return;
        }

        if (activ)
        {
            page.gameObject.SetActive(true);
            if (SendPage)
                page.StartActiv();
        }
        else
        {
            if (SendPage)
                page.EndActiv();

            page.gameObject.SetActive(false);
        }
    }

    private void SetActivIgnorePageSettings()
    {
        if (ignorePageTypes != null)
        {
            foreach (var item in pages)
            {
                if (ignorePageTypes.Contains(item.pageType))
                {
                    item.gameObject.SetActive(ignorePageActiv);
                }
            }
        }
    }

    private void CheckDublicate()
    {
        var duplicates = pages
                   .GroupBy(x => x)
                   .Where(g => g.Count() > 1)
                   .Select(g => g.Key);

        foreach (var d in duplicates)
        {
            Debug.LogWarning("Be careful there are duplicates: " + d);
        }
    }

    private void OnValidate()
    {
        name = nameof(ScreenManager);

        if (_refresh)
        {
            Page[] scenePages = FindAllPages();

            if(scenePages.Length > 0)
            {
                CheckDublicate();

                pages = scenePages;
                PagesActivate(_activPageEditor);
            }
        }
    }
}



