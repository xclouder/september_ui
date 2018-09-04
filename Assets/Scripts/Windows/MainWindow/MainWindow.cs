using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : Window
{

	public Button shopBtn;

	public override void Bind(WindowController wc)
	{
		base.Bind(wc);

		shopBtn.onClick.AddListener(wc.UICallbackMap.GetButtonCallback("ShowShop"));
	}

}
