using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResLoader
{

	T Load<T>(string path) where T : UnityEngine.Object;

	IResAsyncLoadOperation<T> LoadAsync<T>(string path) where T : UnityEngine.Object;

}
