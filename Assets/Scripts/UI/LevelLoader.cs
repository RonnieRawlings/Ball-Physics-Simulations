// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    /// <summary> method <c>LoadLevel</c> takes a given string, uses the sceneManager to load the scene. Must be in build index. </summary>
    /// <param name="levelName">Name of the scene/level</param>
    public void LoadLevel(string levelName)
    {
        // Loads given scene.
        SceneManager.LoadScene(levelName);
    }
}
