using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorAudio : MonoBehaviour
{
    public static GerenciadorAudio instance;

    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip menuMusica;
    public AudioClip jogoMusica;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip hoverSom;
    public AudioClip cliqueSom;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        AtualizarMusica(SceneManager.GetActiveScene().name);
    }

    //Unity
    void OnEnable()
    {
        SceneManager.sceneLoaded += CenaCarregada;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= CenaCarregada;
    }

    //Intermédio
    void CenaCarregada(Scene scene, LoadSceneMode mode)
    {
        AtualizarMusica(scene.name);
    }

    //Lógica
    void AtualizarMusica(string sceneName)//usado ao trocar de cena
    {
        if (sceneName == "Menu")
        {
            Musica(menuMusica);
        }
        else if (sceneName == "Mapa")
        {
            Musica(jogoMusica);
        }
    }

    public void Musica(AudioClip newMusic)
    {
        if (musicSource.clip == newMusic) return;//garantia se chamar música atual

        musicSource.Stop();//para música antiga
        musicSource.clip = newMusic;//põe nova
        musicSource.loop = true;//loopa
        musicSource.Play();//toca
    }

    public void Hover()
    {
        sfxSource.PlayOneShot(hoverSom);
    }

    public void Clique()
    {
        sfxSource.PlayOneShot(cliqueSom);
    }
}