using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartBtnScript : MonoBehaviour
{
    [Header("Next Scene load settings")]
    [SerializeField] private int nextSceneBuildInd;
    [Header("Config load settings")]
    [SerializeField] private string configFilename;
    [SerializeField] private Config gameplayConfig;

    private Thread LoadGameplayConfigFile()
    {
        var t = new Thread(
            new ParameterizedThreadStart(gameplayConfig.LoadFromFileThreading)
            );
        t.Start(configFilename);
        return t;
    }

    public void LoadResources()
	{
        var loadThreads = new List<Thread>();

        loadThreads.Add(LoadGameplayConfigFile());

        // here will be many recource load operations

        foreach(var thread in loadThreads)
        {
            thread.Join();
		}
    }

    public void NextSceneTransition()
	{
        AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(nextSceneBuildInd);
        sceneLoadOperation.allowSceneActivation = false;
        
        LoadResources();

        sceneLoadOperation.allowSceneActivation = true;
    }
}
