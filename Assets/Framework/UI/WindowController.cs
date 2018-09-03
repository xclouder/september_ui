using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
