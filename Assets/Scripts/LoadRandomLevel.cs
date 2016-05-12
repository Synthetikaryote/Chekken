using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadRandomLevel : MonoBehaviour
{
    public string[] levels;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levels[Random.Range(0, levels.Length)], LoadSceneMode.Single);
    }
}
