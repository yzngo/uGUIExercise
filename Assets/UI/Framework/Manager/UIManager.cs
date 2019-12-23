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
    private Transform _canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if (_canvasTransform == null)
            {
                _canvasTransform = GameObject.Find("Canvas").transform;
            }
            return _canvasTransform;
        }
    }
    private Dictionary<string, GameObject> pageDict;

    private UIManager()
    {
        ParseUIPageListJson();
    }

    public GameObject GetPage(string name)
    {
        return pageDict.GetValue(name);
    }

    public void OpenWindow(GameObject page)
    {
        page.SetActive(true);
    }

    public void CloseWindow(GameObject page)
    {
        page.SetActive(false);
    }

    //解析json文件
    private void ParseUIPageListJson()
    {
        TextAsset textPageList = Resources.Load<TextAsset>("page/UIPageList");
        UIPageList pageList = JsonUtility.FromJson<UIPageList>(textPageList.text);

        if (pageDict == null)
        {
            pageDict = new Dictionary<string, GameObject>();
        }
        foreach (UIPageInfo pageInfo in pageList.list)
        {
            string path = pageInfo.path;
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>(path), CanvasTransform, false);
            //BaumUI.Instantiate(gameObject, uiPrefab);
            pageDict.Add(pageInfo.name, go);
            go.SetActive(false);
        }
    }
}
