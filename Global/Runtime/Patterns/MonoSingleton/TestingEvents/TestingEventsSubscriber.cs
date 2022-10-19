using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEventsSubscriber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MainController.Instance.OnSpacePress += Instance_OnSpacePress;
    }

    private void Instance_OnSpacePress(object sender, System.EventArgs e)
    {
        Debug.Log("Space pressed!");
    }
}
