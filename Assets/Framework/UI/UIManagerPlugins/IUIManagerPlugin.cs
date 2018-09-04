using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIManagerPlugin
{
    void OnWillCreateWindow(WindowController ctrl, Window win);
    void OnWindowCreated(WindowController ctrl, Window win);
    void OnWillOpenWindow(WindowController ctrl, Window win);
    void OnDidOpenWindow(WindowController ctrl, Window win);
    void OnWillCloseWindow(WindowController ctrl, Window win);
    void OnDidCloseWindow(WindowController ctrl, Window win);
    void OnWillDestroyWindow(WindowController ctrl, Window win);
    void OnDidDestroyWindow(WindowController ctrl, Window win);
}
