using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // 对应 Endless Mode 按钮
    public void PlayEndlessMode()
    {
        LoadSceneWithCheck("Endless Mode");
    }

    // 对应 Story Mode 按钮
    public void PlayStoryMode()
    {
        LoadSceneWithCheck("Story Mode");
    }

    // 对应 Quit Game 按钮
    public void QuitGame()
    {
        // 编辑器下停止播放，打包后退出游戏
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // 内部检查：防止场景没加进 Build Settings 报错
    private void LoadSceneWithCheck(string sceneName)
    {
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);
        if (buildIndex == -1)
        {
            Debug.LogError($"场景 '{sceneName}' 未添加到 Build Settings！请去 File -> Build Settings 里添加该场景。");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}