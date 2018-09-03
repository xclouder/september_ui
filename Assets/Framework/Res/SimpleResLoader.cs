using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleResLoader : IResLoader {
	
	public T Load<T>(string path) where T : UnityEngine.Object
	{
		return Resources.Load<T>(path);
	}

	public IResAsyncLoadOperation<T> LoadAsync<T>(string path) where T : UnityEngine.Object
	{
		var req = Resources.LoadAsync<T>(path);
		return new SimpleResAsyncLoadOperation<T>(path, req);
	}
}

public class SimpleResAsyncLoadOperation<T> : IResAsyncLoadOperation<T> where T : UnityEngine.Object
{
	private string _path;
	private ResourceRequest _innerLoadOp;
	public SimpleResAsyncLoadOperation(string path, ResourceRequest op)
	{
		_path = path;
		_innerLoadOp = op;
	}
	
	public string ResPath
	{
		get { return _path; }
	}

	public T Obj
	{
		get { return _innerLoadOp.asset as T; }
	}

	public bool IsDone
	{
		get { return _innerLoadOp.isDone; }
	}
}