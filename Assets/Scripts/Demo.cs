﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		UIManager.Instance.OpenWindow(WindowType.MainWindow);
	}
	
}
