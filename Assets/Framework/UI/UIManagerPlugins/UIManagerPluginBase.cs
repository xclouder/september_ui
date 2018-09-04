using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerPluginBase : IUIManagerPlugin {
	
	public virtual void OnWillCreateWindow(WindowController ctrl, Window win)
	{
		
	}

	public virtual void OnWindowCreated(WindowController ctrl, Window win)
	{
		
	}

	public virtual void OnWillOpenWindow(WindowController ctrl, Window win)
	{
		
	}

	public virtual void OnDidOpenWindow(WindowController ctrl, Window win)
	{
		
	}

	public virtual void OnWillCloseWindow(WindowController ctrl, Window win)
	{

	}

	public virtual void OnDidCloseWindow(WindowController ctrl, Window win)
	{

	}

	public virtual void OnWillDestroyWindow(WindowController ctrl, Window win)
	{

	}

	public virtual void OnDidDestroyWindow(WindowController ctrl, Window win)
	{

	}
}
