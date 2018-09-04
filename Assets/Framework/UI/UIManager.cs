using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class UIManager
{
	private static IWindowMetaInfoProvider _metaInfoProvider; 
	
	private static UIManager _ins;
	public static UIManager Instance
	{
		get
		{
			if (_ins == null)
			{
				_ins = new UIManager();
			}

			return _ins;
		}
	}


	private List<IUIManagerPlugin> _plugins = new List<IUIManagerPlugin>(8);

	public void AddPlugin(IUIManagerPlugin plugin)
	{
		_plugins.Add(plugin);
	}
	
	private Transform _windowRootTr;
	public UIManager()
	{
		//create winroot
		GameObject windowRoot = GameObject.Find("UIRoot");
		GameObject.DontDestroyOnLoad(windowRoot);
		_windowRootTr = windowRoot.transform;
		
		//init data
		_metaInfoDict = new Dictionary<string, WindowMetaInfo>(100);
		_openedWindowDict = new Dictionary<string, WindowController>(20);
		_aliveWindowDict = new Dictionary<string, WindowController>(100);
		
		SetWindowControllerFactory(new DefaultWindowControllerFactory());

		LoadMetaInfos();
		
		LoadDefaultPlugins();
	}

	private void LoadDefaultPlugins()
	{
		AddPlugin(new StackWindowManagePlugin());
	}

	public static void SetWindowMetaInfoProvider(IWindowMetaInfoProvider provider)
	{
		_metaInfoProvider = provider;
	}

	private void LoadMetaInfos()
	{
		if (_metaInfoProvider == null)
		{
			_metaInfoProvider = new DefaultWindowMetaInfoProvider();
		}
		_metaInfoProvider.Load(ref _metaInfoDict);
	}
	
	private Dictionary<string, WindowMetaInfo> _metaInfoDict;
	private Dictionary<string, WindowController> _openedWindowDict;
	private Dictionary<string, WindowController> _aliveWindowDict;

	public bool IsWindowOpened(string winId)
	{
		return _openedWindowDict.ContainsKey(winId);
	}
	
	public WindowController OpenWindow(string winId, BaseOpenWindowParams param = null)
	{
		WindowController winCtrl;
		if (_aliveWindowDict.TryGetValue(winId, out winCtrl))
		{
			if (_openedWindowDict.ContainsKey(winId))
			{
				return winCtrl;	
			}
			else
			{
				//reopen it
				_OpenWindow(winCtrl, param);
			}

			return winCtrl;
		}

		WindowMetaInfo metaInfo;
		if (_metaInfoDict.TryGetValue(winId, out metaInfo))
		{
			winCtrl = CreateWindowController(metaInfo);
			winCtrl.OnInit(metaInfo);
			
			winCtrl.OnWindowWillCreate();
			
			
			foreach (var p in _plugins)
			{
				p.OnWillCreateWindow(winCtrl, null);
			}
			
			
			var win = CreateWindow(metaInfo);
			winCtrl.OnWindowCreated(win);
			_aliveWindowDict.Add(winId, winCtrl);
			
			
			foreach (var p in _plugins)
			{
				p.OnWindowCreated(winCtrl, win);
			}
			

			_OpenWindow(winCtrl, param);
			
			return winCtrl;
		}
		else
		{
			throw new WindowMetaInfoNotFoundException(); 
		}
		
	}

	private void _OpenWindow(WindowController winCtrl, BaseOpenWindowParams param)
	{
		foreach (var p in _plugins)
		{
			p.OnWillOpenWindow(winCtrl, winCtrl.Window);
		}
		
		
		//some strategy to show window
		winCtrl.Window.gameObject.SetActive(true);
		var tr = winCtrl.Window.gameObject.transform;

		tr.localPosition = Vector3.zero;
		tr.localScale = Vector3.one;
		var rectTr = tr as RectTransform;
		rectTr.sizeDelta = Vector2.zero;
		
		winCtrl.OnWindowOpen(param);
		_openedWindowDict.Add(winCtrl.WinMetaInfo.WindowID, winCtrl);
		
		
		foreach (var p in _plugins)
		{
			p.OnDidOpenWindow(winCtrl, winCtrl.Window);
		}
	}

    public void OpenWindowAsync(string winId, System.Object token, BaseOpenWindowParams param = null)
    {
        throw new NotImplementedException();
    }

	public void CloseWindow(string winId, BaseCloseWindowParams param = null)
	{
		WindowController theWinCtrl;
		if (_openedWindowDict.TryGetValue(winId, out theWinCtrl))
		{
			foreach (var p in _plugins)
			{
				p.OnWillCloseWindow(theWinCtrl, theWinCtrl.Window);
			}
			
			
			//这里抽象不同打开/关闭策略
			theWinCtrl.Window.gameObject.SetActive(false);
			
			theWinCtrl.OnWindowClosed(param);

			_openedWindowDict.Remove(winId);
			
			
			foreach (var p in _plugins)
			{
				p.OnDidCloseWindow(theWinCtrl, theWinCtrl.Window);
			}
		}
		else
		{
			Debug.LogWarning("window not open state:" + winId);
		}
	}

	private void DestroyWindow(WindowController winCtrl)
	{
		foreach (var p in _plugins)
		{
			p.OnWillDestroyWindow(winCtrl, winCtrl.Window);
		}
		
		
		GameObject.Destroy(winCtrl.Window);
		winCtrl.OnWindowDestroyed();

		var winId = winCtrl.WinMetaInfo.WindowID;
		
		Debug.Assert(_aliveWindowDict.ContainsKey(winId));
		_aliveWindowDict.Remove(winId);
		
		
		foreach (var p in _plugins)
		{
			p.OnDidDestroyWindow(winCtrl, winCtrl.Window);
		}
	}


	#region create window logic

	private IWindowControllerFactory _winCtrlFactory;

	public void SetWindowControllerFactory(IWindowControllerFactory fac)
	{
		_winCtrlFactory = fac;
	}
	
	private WindowController CreateWindowController(WindowMetaInfo metaInfo)
	{
		var ctrl = _winCtrlFactory.Create(metaInfo);
		return ctrl;
	}
	
	private Window CreateWindow(WindowMetaInfo metaInfo)
	{
		var path = metaInfo.WindowResourcePath;

		var prefab = ResMgr.Load<GameObject>(path);
		
		var winObj = GameObject.Instantiate(prefab);

		var tr = winObj.transform;
		tr.SetParent(_windowRootTr);

		var win = winObj.GetComponent(metaInfo.WindowType) as Window;

		if (win == null)
		{
			Debug.Log(string.Format("cannot get WindowComponent:{0} from window object:{1}", metaInfo.WindowType, path));

			win = winObj.AddComponent(metaInfo.WindowType) as Window;

			if (win == null)
			{
				Debug.LogError("cannot add WindowComponent to gameobject:" + metaInfo.WindowType);
			}
		}
		
		return win;
		
	}

	#endregion
	
}

public class BaseOpenWindowParams
{
	
}

public class BaseCloseWindowParams
{
	
}


public enum WindowControllerTypeEnum
{
	CSharp,
	Lua
}

public class WindowMetaInfo
{

	public string WindowID;
	public string WindowResourcePath;

	public System.Type WindowType;

	public WindowControllerTypeEnum ControllerType;
	public System.Type WindowControllerCSharpClassType;

	public string LuaScriptPath;

}

public interface IWindowMetaInfoProvider
{
	void Load(ref Dictionary<string, WindowMetaInfo> dict);
}

public class DefaultWindowMetaInfoProvider : IWindowMetaInfoProvider
{
	public void Load(ref Dictionary<string, WindowMetaInfo> dict)
	{
		var assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (var assembly in assemblies)
		{
			var typs = assembly.GetExportedTypes();
			foreach (var t in typs)
			{
				var arrts = Attribute.GetCustomAttributes(t);
				foreach (var a in arrts)
				{
					if (a is WindowDefineAttribute)
					{
						_Load(t, ref dict);
					}
				}
			}			
		}
	}

	private void _Load(System.Type clsTyp, ref Dictionary<string, WindowMetaInfo> dict)
	{
		FieldInfo[] fields = clsTyp.GetFields();
		foreach (var f in fields)
		{
			var objs = f.GetCustomAttributes(typeof(WindowMetaInfoAttribute), false);
			foreach (var attr in objs)
			{
				var metaAttr = attr as WindowMetaInfoAttribute;
				var metaInfo = GetMetaInfoFromAttr(metaAttr);
				var winId = f.GetValue(null) as string;
				metaInfo.WindowID = winId;
				
				dict.Add(winId, metaInfo);
			}
		}
	}

	private WindowMetaInfo GetMetaInfoFromAttr(WindowMetaInfoAttribute attr)
	{
		var info = new WindowMetaInfo();
		info.ControllerType = attr.ControllerTypeEnum;
		info.WindowResourcePath = attr.WindowResPath;
		info.WindowType = attr.WindowType;
		info.WindowControllerCSharpClassType = attr.WindowControllerCSharpType;
		info.LuaScriptPath = attr.LuaScriptPath;
		
		return info;
	}
}