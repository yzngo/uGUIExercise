using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class UIManager
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    private Transform _desktopTransform;
    private Transform DesktopTransform
    {
        get
        {
            if (_desktopTransform == null)
            {
                _desktopTransform = GameObject.Find("Desktop").transform;
            }
            return _desktopTransform;
        }
    }
    private Transform _windowTransform;
    private Transform WindowTransform
    {
        get
        {
            if (_windowTransform == null)
            {
                _windowTransform = GameObject.Find("Window").transform;
            }
            return _windowTransform;
        }
    }
    private Transform _dialogTranform;
    private Transform DialogTranform
    {
        get
        {
            if (_dialogTranform == null)
            {
                _dialogTranform = GameObject.Find("Dialog").transform;
            }
            return _dialogTranform;
        }
    }
    private Transform _topmostTransform;
    private Transform TopmostTransform
    {
        get
        {
            if (_topmostTransform == null)
            {
                _topmostTransform = GameObject.Find("Topmost").transform;
            }
            return _topmostTransform;
        }
    }
    private Dictionary<string, GameObject> pageDict;

    private UIManager()
    {
        pageDict = new Dictionary<string, GameObject>();
        GameObject.DontDestroyOnLoad(DesktopTransform.gameObject);
        ParseUIPageListJson();
    }

    public GameObject GetPage(string name)
    {
        return pageDict.GetValue(name);
    }

    public void OpenWindow(GameObject page)
    {
        page.transform.SetParent(WindowTransform);
        page.transform.SetAsFirstSibling();
        page.SetActive(true);
    }

    public void OpenDialog(GameObject page)
    {
        page.transform.SetParent(DialogTranform);
        page.transform.SetAsFirstSibling();
        page.SetActive(true);
    }

    public void OpenTopmost(GameObject page)
    {
        page.transform.SetParent(TopmostTransform);
        page.transform.SetAsFirstSibling();
        page.SetActive(true);
    }
    public void CloseWindow(GameObject page)
    {
        page.SetActive(false);
    }

    public void SendMsg(string id, string param1, string param2, string param3)
    {
        foreach (KeyValuePair<string,GameObject> page in pageDict)
        {
            for(int i = 0; i < page.Value.GetComponentsInChildren<LuaBehaviour>(false).Length;i++)
            {
                var luaBehaviour = page.Value.GetComponentsInChildren<LuaBehaviour>(false)[i];
                var ret = luaBehaviour.SendMessage(id,param1,param2,param3);
                if(ret == 1)
                {
                    return;
                }
            }

        }
    }

    //解析json文件
    private void ParseUIPageListJson()
    {
        TextAsset textPageList = Resources.Load<TextAsset>("Page/UIPageList");
        UIPageList pageList = JsonUtility.FromJson<UIPageList>(textPageList.text);
        foreach (UIPageInfo pageInfo in pageList.list)
        {
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>(pageInfo.path), WindowTransform, false);
            //BaumUI.Instantiate(gameObject, uiPrefab);
            pageDict.Add(pageInfo.name, go);
            go.SetActive(false);
        }
    }
}
