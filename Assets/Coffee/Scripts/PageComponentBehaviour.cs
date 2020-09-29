using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PageComponentBehaviour : MonoBehaviour
{
    public void TransitionPage(string sceneName)
    {
        PageScene pageScene = PageScene.Title;
        Enum.TryParse(sceneName, out pageScene);
        SceneTransitionManager.instance.SceneTransition(sceneName, (int)pageScene);
    }
}
