using UnityEngine;
using UnityEngine.SceneManagement;

public class UIReady : MonoBehaviour
{
    public ScreenManager uiManager;

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        int idScene = SceneManager.GetActiveScene().buildIndex;
        LoadScene(idScene);
    }

    public void Pause(bool activ)
    {
        if (activ)
            uiManager.ChangePage(PageType.Pause, false);
        else
            uiManager.ChangePage(PageType.Game, true);
    }

    public void LoadScene(int idScene)//добавить возможность асинхронной загрузки
    {
        SceneManager.LoadScene(idScene);
    }

    private void OnValidate()
    {
        uiManager = FindFirstObjectByType<ScreenManager>();
    }
}
