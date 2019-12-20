using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LoadLua : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaEnv luaenv = null;
        // Use this for initialization
        void Start()
        {
            luaenv = new LuaEnv();
            luaenv.DoString("require 'main'");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
