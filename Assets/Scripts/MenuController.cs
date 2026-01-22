using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string cenaDoJogo = "CacaAoDiploma"; 

    public void Play()
    {
        SceneManager.LoadScene(cenaDoJogo);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
