using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Debug.Log(SceneManager.sceneCountInBuildSettings);
        for(int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (SceneManager.GetSceneByName(SceneManager.GetSceneByBuildIndex(i).name) == null)
            {
                SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
            }
        }
    }
}
