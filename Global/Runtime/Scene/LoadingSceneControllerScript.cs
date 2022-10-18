using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Meta.Configuration;
public class LoadingSceneControllerScript : MonoBehaviour
{

    [SerializeField]
    private Text versionNumber;

    [SerializeField]
    ConfigurationEnviroments enviromentVars;
    void Start()
    {
        versionNumber.text = enviromentVars.GetHostForId(VariablesIds.ENVIROMENT_NAME) +" "+ Application.version;
        // GoVideoTestScene();
    }

    public void goToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    
    public void reloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void quitApp()
    {
        Application.Quit();
    }

    public void GoVideoTestScene()
    {
        SceneManager.LoadScene("Video2DScene",  LoadSceneMode.Single);
    }


}