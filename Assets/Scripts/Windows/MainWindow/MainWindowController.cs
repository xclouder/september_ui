using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWindowController : WindowController {
	public override void OnInit(WindowMetaInfo winMeta)
	{
		base.OnInit(winMeta);
		
		RegisterButtonCallback("ShowShop", OnClickShowShop);
	}

	public override void OnWindowClosed(BaseCloseWindowParams param)
	{
		base.OnWindowClosed(param);
		
		Debug.Log("main window created");
	}

	void OnClickShowShop()
	{
		UIManager.Instance.OpenWindow(WindowType.ShopWindow);
	}
	
}
