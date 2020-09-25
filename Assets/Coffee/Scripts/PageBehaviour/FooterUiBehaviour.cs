using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FooterUiBehaviour : MonoBehaviour
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
