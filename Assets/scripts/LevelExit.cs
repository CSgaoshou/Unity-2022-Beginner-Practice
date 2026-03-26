using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            int buildIndex = SceneUtility.GetBuildIndexByScenePath(nextSceneName);
            if (buildIndex == -1)
            {
                Debug.LogError($"场景 '{nextSceneName}' 未添加到 Build Settings！");
                return;
            }
            SceneManager.LoadScene(nextSceneName);
        }
    }
}