using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    int sceneIndex = 0;

    public void ChangeScene(int index)
    {
        sceneIndex = index;
        StartCoroutine(WaitToTransition());
    }

    IEnumerator WaitToTransition()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(sceneIndex);
    }
}
