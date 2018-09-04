using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindowController : WindowController, IWindowStackable {
	
	public override void OnInit(WindowMetaInfo winMeta)
	{
		base.OnInit(winMeta);
		
		RegisterButtonCallback("Close", OnClickClose);
		RegisterButtonCallback("Buy", OnClickBuy);
	}

	public override void OnWindowOpen(BaseOpenWindowParams param)
	{
		base.OnWindowOpen(param);
		
		Debug.Log("open window: shop win");
	}

	void OnClickClose()
	{
		UIManager.Instance.CloseWindow(WinMetaInfo.WindowID);
	}

	void OnClickBuy()
	{
		Debug.Log("buy clicked");
	}
}
