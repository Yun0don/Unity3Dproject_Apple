using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public GameObject mapSelectPanel;
    
    public void ToggleMapSelectPanel()
    {
        if (mapSelectPanel != null)
            mapSelectPanel.SetActive(!mapSelectPanel.activeSelf);
    }

    public void ShowMapSelectPanel()
    {
        if (mapSelectPanel != null)
            mapSelectPanel.SetActive(true);
    }

    public void CloseMapSelectPanel()
    {
        if (mapSelectPanel != null)
            mapSelectPanel.SetActive(false);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Debug.Log("게임 종료!");
        Application.Quit();
    }
    public void RestartLevel()
    {
        var current = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(current);
    }
}