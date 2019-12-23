using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;

public class LoadLua: MonoBehaviour
{
    public static LuaEnv luaenv = new LuaEnv();
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void Init()
    {
        //LuaEnv luaenv = null;
        // Use this for initialization
        //luaenv = new LuaEnv();
        luaenv.DoString("require 'main'");
        Action main = luaenv.Global.Get<Action>("main");//映射到一个delgate，要求delegate加到生成列表，否则返回null，建议用法
        main();
    }
}
