using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class WindowMetaInfoAttribute : System.Attribute
{
	public string WindowId;
	public string WindowResPath;
	public WindowControllerTypeEnum ControllerTypeEnum;
	public System.Type WindowType;
	public System.Type WindowControllerCSharpType;
	public string LuaScriptPath;
	
	public WindowMetaInfoAttribute(string resPath, System.Type winCtrlCsharpType, System.Type winType = null)
	{
		WindowResPath = resPath;
		ControllerTypeEnum = WindowControllerTypeEnum.CSharp;
		WindowType = winType;
		WindowControllerCSharpType = winCtrlCsharpType;
	}
	
	public WindowMetaInfoAttribute(string resPath, string luaScriptPath, System.Type winType = null)
	{
		WindowResPath = resPath;
		ControllerTypeEnum = WindowControllerTypeEnum.Lua;
		WindowType = winType;
		LuaScriptPath = luaScriptPath;
	}

}
