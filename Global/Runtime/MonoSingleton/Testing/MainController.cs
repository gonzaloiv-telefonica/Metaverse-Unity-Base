using Meta.Global.MonoSingleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoSingleton<MainController>
{
    public event EventHandler OnSpacePress;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed Inoke");
            OnSpacePress?.Invoke(this, EventArgs.Empty);
        }
    }
}
