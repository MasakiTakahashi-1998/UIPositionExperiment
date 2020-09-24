using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PageComponentBehaviour : MonoBehaviour
{
    [SerializeField]
    public int index = (int)CommonScene.Everything;

    public void TransitionTitle(string sceneName)
    {
        SceneTransitionManager.instance.LoadPage(sceneName);
    }
}
