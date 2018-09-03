using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindowController : WindowController {
	public override void OnWindowOpen(BaseOpenWindowParams param)
	{
		base.OnWindowOpen(param);
		
		Debug.Log("open window: shop win");
	}
}
