using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;

public class LoadLua: MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Init()
    {
        var luaenv = LuaEnvInstance.Instance;
        luaenv.DoString("require 'Script/main'");
        Action main = luaenv.Global.Get<Action>("main");//映射到一个delgate，要求delegate加到生成列表，否则返回null，建议用法
        main();
    }
}
