using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : Window
{
	public Button btnBuy;
	public Button btnClose;
	
	public override void Bind(WindowController wc)
	{
		base.Bind(wc);
		
		btnBuy.onClick.AddListener(wc.UICallbackMap.GetButtonCallback("Buy"));
		btnClose.onClick.AddListener(wc.UICallbackMap.GetButtonCallback("Close"));
	}
	
}
