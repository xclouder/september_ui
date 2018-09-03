using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WindowType
{

	[WindowMetaInfo("MainWindow", typeof(MainWindowController), typeof(MainWindow))]
	public static string MainWindow = "MainWindow";
	
	[WindowMetaInfo("ShopWindow", typeof(MainWindowController), typeof(ShopWindow))]
	public static string ShopWindow = "ShopWindow";
	
	
}
