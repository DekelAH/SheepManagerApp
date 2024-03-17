using UnityEngine;
using UnityEngine.SceneManagement;

public static class SectionHandler
{
    #region Methods

    public static void LoadSection(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    #endregion
}
