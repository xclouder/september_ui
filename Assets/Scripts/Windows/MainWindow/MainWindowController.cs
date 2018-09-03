using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWindowController : WindowController {
	
	public override void OnWindowClosed(BaseCloseWindowParams param)
	{
		base.OnWindowClosed(param);
		
		Debug.Log("main window created");
	}
	
}
