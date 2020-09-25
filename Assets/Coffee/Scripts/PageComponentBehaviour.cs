using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PageComponentBehaviour : MonoBehaviour
{
    [SerializeField]
    public int index = 7;

    public void SelectCommonScene(int num)
    {
        index = num;
    }
    
    public void TransitionTitle(string sceneName)
    {
        SceneTransitionManager.instance.SceneTransition(sceneName,index);
    }
}
