using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public void ReturnToTitle()
    {
        sceneTransition.ChangeScene(0);
        sceneTransition.TransitionOut();
    }
}
