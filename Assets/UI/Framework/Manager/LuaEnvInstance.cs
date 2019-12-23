using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using XLua;
public class LuaEnvInstance
{
    private static LuaEnv _instance;

    public static LuaEnv Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LuaEnv();
            }
            return _instance;
        }
    }
}
