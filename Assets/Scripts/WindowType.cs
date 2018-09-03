using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WindowDefine]
public static class WindowType
{

	[WindowMetaInfo("Windows/MainWindow", typeof(MainWindowController), typeof(MainWindow))]
	public static string MainWindow = "MainWindow";
	
	[WindowMetaInfo("Windows/ShopWindow", typeof(MainWindowController), typeof(ShopWindow))]
	public static string ShopWindow = "ShopWindow";
	
	
}
