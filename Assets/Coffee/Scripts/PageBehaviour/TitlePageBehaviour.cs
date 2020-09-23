using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePageBehaviour : MonoBehaviour
{
    public void TransitionMain()
    {
        SceneTransitionManager.instance.LoadPage("Main");
    }
}
