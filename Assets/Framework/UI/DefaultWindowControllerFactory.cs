using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWindowControllerFactory : IWindowControllerFactory {
	
	public WindowController Create(WindowMetaInfo metaInfo)
	{
		var typ = metaInfo.WindowControllerCSharpClassType;
		
		return System.Activator.CreateInstance(typ) as WindowController;
	}
	
}
