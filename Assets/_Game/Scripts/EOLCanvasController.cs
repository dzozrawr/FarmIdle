using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EOLCanvasController : MonoBehaviour
{
    public Button nextLevelButton=null;

    public void NextLevelButtonEffect(){
        int nextSceneIndex= (SceneManager.GetActiveScene().buildIndex+1)% SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
