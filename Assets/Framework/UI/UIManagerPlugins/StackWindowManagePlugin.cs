
using System.Collections.Generic;

public class StackWindowManagePlugin : UIManagerPluginBase
{

	private Stack<WindowController> _stack = new Stack<WindowController>(10);

	public override void OnDidCloseWindow(WindowController ctrl, Window win)
	{
		base.OnDidCloseWindow(ctrl, win);
		
		
	}
}
