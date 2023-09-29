using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
  public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quitapp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }

}



