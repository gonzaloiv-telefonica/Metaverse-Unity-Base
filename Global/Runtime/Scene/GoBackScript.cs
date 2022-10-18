using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoBackScript : MonoBehaviour
{

    public void goBack()
    {
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
    }

}