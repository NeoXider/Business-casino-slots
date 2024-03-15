using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPageSwitch : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private PageType pageType;

    [SerializeField]
    private bool change = true;
    public bool switchLastPage = false;

    [SerializeField]
    private ScreenManager uIManager;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        if (button != null)
        {
            button.onClick.AddListener(SwitchPage);
        }
    }

    private void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(SwitchPage);
        }
    }

    private void OnMouseDown()
    {
        if (button == null)
        {
                SwitchPage();
        }
    }

    private void SwitchLastPage()
    {
        uIManager.SwitchLastPage();
    }

    public void SwitchPage()
    {
        print("switchPage - " + pageType);
        if(change)
        {
            if (switchLastPage)
                SwitchLastPage();
            else
                uIManager.ChangePage(pageType);
        }
        else
        {
            uIManager.SetPage(pageType);
        }
        
    }   

    private void OnValidate()
    {
        uIManager = FindFirstObjectByType<ScreenManager>();

#if UNITY_EDITOR
        if (!AssetDatabase.Contains(this))
        {
            if (uIManager == null)
            {
                Debug.LogError("Need UiManager");
            }
        }
#endif
        if(change)
        {
            if (switchLastPage)
                pageType = PageType.None;
        }
        else
        {
            switchLastPage = false;
        }
        

        button = GetComponent<Button>();
    }
}
