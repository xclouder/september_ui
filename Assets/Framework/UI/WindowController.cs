using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WindowController
{
	private Window _window;

	public Window Window
	{
		get { return _window; }
	}
	private WindowMetaInfo _winMetaInfo;

	public WindowMetaInfo WinMetaInfo
	{
		get { return _winMetaInfo; }
	}

	private void SetWindow(Window w)
	{
		_window = w;
	}

	/// <summary>
	/// WindowController的初始化，此时window并没有创建完成
	/// </summary>
	public virtual void OnInit(WindowMetaInfo winMeta)
	{
		_winMetaInfo = winMeta;
		
		
	}
	
	public virtual void OnWindowWillCreate()
	{
		
	}
	
	public virtual void OnWindowCreated(Window w)
	{
		w.Bind(this);
		SetWindow(w);	
	}

	public virtual void OnWindowOpen(BaseOpenWindowParams param)
	{
		
	}
	
	public virtual void OnWindowClosed(BaseCloseWindowParams param)
	{
		
	}

	public virtual void OnWindowDestroyed()
	{
		_window = null;
	}

	#region ui callback regist

	private UICallbackMap _uiCallbackMap = new UICallbackMap();
	public UICallbackMap UICallbackMap
	{
		get { return _uiCallbackMap; }
	}
	
	protected void RegisterButtonCallback(string key, UnityAction cb)
	{
		_uiCallbackMap.RegisterButtonCallback(key, cb);
	}
	
	#endregion
	

}

public class UICallbackMap
{
	private Dictionary<string, UnityAction> _btnCbDict;

	public void RegisterButtonCallback(string key, UnityAction cb)
	{
		if (_btnCbDict == null)
		{
			_btnCbDict = new Dictionary<string, UnityAction>(8);
		}

		_btnCbDict[key] = cb;
	}

	public UnityAction GetButtonCallback(string key)
	{
		if (_btnCbDict == null)
		{
			return null;
		}
		
		UnityAction cb;
		_btnCbDict.TryGetValue(key, out cb);
		return cb;
	}
}
