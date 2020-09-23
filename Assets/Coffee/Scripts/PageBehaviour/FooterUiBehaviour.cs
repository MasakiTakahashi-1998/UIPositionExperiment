using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FooterUiBehaviour : MonoBehaviour
{
    public void TransitionTitle(string sceneName)
    {
        SceneTransitionManager.instance.LoadPage(sceneName);
    }
}
