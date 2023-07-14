using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void NextLevelString(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
