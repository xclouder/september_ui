
using System.Collections.Generic;

public interface IWindowStackable
{
	
}

public class StackWindowManagePlugin : UIManagerPluginBase
{

	private string _autoProcessingWinId;
	private Stack<WindowController> _stack = new Stack<WindowController>(10);

	public override void OnWillOpenWindow(WindowController ctrl, Window win)
	{
		base.OnWillOpenWindow(ctrl, win);
		
		if (ctrl is IWindowStackable)
		{
			if (ctrl.WinMetaInfo.WindowID == _autoProcessingWinId)
			{
				_autoProcessingWinId = string.Empty;
				return;
			}
			
			if (_stack.Count > 0)
			{
				var prevWinCtrl = _stack.Peek();
				
				//close prev win
				_autoProcessingWinId = prevWinCtrl.WinMetaInfo.WindowID;
				
				UIManager.Instance.CloseWindow(_autoProcessingWinId);
			}
			
			_stack.Push(ctrl);
		}
	}

	public override void OnDidCloseWindow(WindowController ctrl, Window win)
	{
		base.OnDidCloseWindow(ctrl, win);

		if (ctrl is IWindowStackable)
		{
			if (ctrl.WinMetaInfo.WindowID == _autoProcessingWinId)
			{
				_autoProcessingWinId = string.Empty;
				return;
			}

			if (_stack.Count > 0)
			{
				var currWinCtrl = _stack.Peek();

				if (currWinCtrl == ctrl)
				{
					_stack.Pop();

					if (_stack.Count > 0)
					{
						var prevWinCtrl = _stack.Peek();
						//open prev win
						_autoProcessingWinId = prevWinCtrl.WinMetaInfo.WindowID;
						UIManager.Instance.OpenWindow(_autoProcessingWinId);	
					}
				}

			}

		}
	}
}
