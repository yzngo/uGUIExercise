/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;
using System;

[System.Serializable]
public class Injection
{
    public string name;
    public GameObject value;
}

[System.Serializable]
public class Properties
{
    public string name;
    public string value;
}

[LuaCallCSharp]
public class LuaBehaviour : MonoBehaviour
{
    public TextAsset luaScript;
    public Injection[] injections;
    public Properties[] properties;
    //internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
    internal static LuaEnv luaEnv = LuaEnvInstance.Instance; //all lua behaviour shared one luaenv only!
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 

    private Action luaOnEnable;
    private Action luaStart;
    private Action luaFixedUpdate;
    private Action luaUpdate;
    private Action luaLateUpdate;
    private Action luaOnDisable;
    private Action luaOnDestroy;
    private Action luaOnGUI;

    [CSharpCallLua]
    public delegate int FDelegate(string a, string b, string c,string d);
    private FDelegate luaEventDesktop;
    private LuaTable scriptEnv;

    void Awake()
    {
        scriptEnv = luaEnv.NewTable();

        // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);
        foreach (var injection in injections)
        {
            scriptEnv.Set(injection.name, injection.value);
        }

        foreach (var property in properties)
        {
            scriptEnv.Set(property.name, property.value);
        }
        luaEnv.DoString(luaScript.text, luaScript.name, scriptEnv);

        Action luaAwake = scriptEnv.Get<Action>("Awake");
        scriptEnv.Get("OnEnable", out luaOnEnable);
        scriptEnv.Get("Start", out luaStart);
        scriptEnv.Get("FixedUpdate", out luaFixedUpdate);
        scriptEnv.Get("Update", out luaUpdate);
        scriptEnv.Get("LateUpdate", out luaLateUpdate);
        scriptEnv.Get("OnDisable", out luaOnDisable);
        scriptEnv.Get("OnDestroy", out luaOnDestroy);
        scriptEnv.Get("Event_Desktop", out luaEventDesktop);
        scriptEnv.Get("OnGUI", out luaOnGUI);

        if (luaAwake != null)
        {
            luaAwake();
        }

    }


    void OnEnable()
    {
        if (luaOnEnable != null)
        {
            luaOnEnable();
        }
    }

    // Use this for initialization
    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    void FixedUpdate()
    {
        if(luaFixedUpdate != null)
        {
            luaFixedUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }
        //todo what?
        if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            LuaBehaviour.lastGCTime = Time.time;
        }
    }

    void LateUpdate()
    {
        if(luaLateUpdate != null)
        {
            luaLateUpdate();
        }
    }

    void OnDisable()
    {
        if(luaOnDisable != null)
        {
            luaOnDisable();
        }
    }
    void OnGUI()
    {
        if (luaOnGUI != null)
        {
            luaOnGUI();
        }
    }
    void OnDestroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy();
        }
        luaEventDesktop = null;
        luaOnDestroy = null;
        luaOnDisable = null;
        luaLateUpdate = null;
        luaFixedUpdate = null;
        luaUpdate = null;
        luaOnEnable = null;
        luaStart = null;
        scriptEnv.Dispose();
        injections = null;
    }

    public int SendMessage(string id,string param1, string param2,string param3)
    {
        if (luaEventDesktop != null){
            return luaEventDesktop(id,param1,param2,param3);
        }
        return -1;
    }
    //todo
    public int Message(string id, string param1, string param2, string param3)
    {
        if (luaEventDesktop != null)
        {
            return luaEventDesktop(id, param1,param2,param3);
        }
        return -1;
    }
}
