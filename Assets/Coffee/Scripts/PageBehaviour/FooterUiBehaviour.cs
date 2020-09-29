using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FooterUiBehaviour : MonoBehaviour
{   
    public void TransitionPage(string sceneName)
    {
        PageScene pageScene = PageScene.Title;
        Enum.TryParse(sceneName, out pageScene);
        SceneTransitionManager.instance.SceneTransition(sceneName,(int)pageScene);
    }
}
