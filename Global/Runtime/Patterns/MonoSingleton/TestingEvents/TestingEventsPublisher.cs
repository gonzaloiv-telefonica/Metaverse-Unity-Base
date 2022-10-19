using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEventsPublisher : MonoBehaviour
{

    public event EventHandler OnSpacePress;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed!!");
            OnSpacePress?.Invoke(this, EventArgs.Empty);
        }
    }
}
