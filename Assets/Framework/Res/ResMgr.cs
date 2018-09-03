using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class ResMgr
{
	private IResLoader _resLoader = new SimpleResLoader();

	private T _Load<T>(string path) where T : UnityEngine.Object
	{
		return _resLoader.Load<T>(path);
	}

	//simple impl
	private IResAsyncLoadOperation<T> _LoadAsync<T>(string path) where T : UnityEngine.Object
	{
		return _resLoader.LoadAsync<T>(path);
	}

	private static ResMgr _ins;

	private static ResMgr Instance
	{
		get
		{
			if (_ins == null)
			{
				_ins = new ResMgr();
			}

			return _ins;
		}
	}

	public static T Load<T>(string path) where T : UnityEngine.Object
	{
		var o = Instance._Load<T>(path);
		if (o == null)
		{
			Debug.LogError("res is null, path:" + path);
		}

		return o;
	}

	public static IResAsyncLoadOperation<T> LoadAsync<T>(string path) where T : UnityEngine.Object
	{
		return Instance._LoadAsync<T>(path);
	}
}

public interface IResAsyncLoadOperation<T>
{
	string ResPath { get; }

	T Obj { get; }

	bool IsDone { get; }
}
