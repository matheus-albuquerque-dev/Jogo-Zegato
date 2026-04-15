using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorMenu : MonoBehaviour
{
    public void NovoJogo()
    {
        SceneManager.LoadScene("Mapa");
    }

    public void Continuar()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void Sair()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}