using UnityEngine;

public class GerenciadorPainel : MonoBehaviour
{
    public static GerenciadorPainel instancia;
    //public StatusPlayer statusPlayer;

    [Header("Configurações de UI")]
    public GameObject painelMapa;
    public GameObject painelCombate;

    void Awake()//Singleton
    {
        if (instancia == null) {
            instancia = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void VoltarMapa()
    {
        painelCombate.SetActive(false);
        painelMapa.SetActive(true);
    }

    //COMBATE
    public ScriptableObject inimigoAtual; //lê isso para saber o inimigo

    public void IniciarCombate(ScriptableObject dadosInimigo)
    {
        inimigoAtual = dadosInimigo;
        painelMapa.SetActive(false);
        painelCombate.SetActive(true);
        
        //pega PlayerLogica dentro de PainelCombate
        PlayerLogica scriptPlayer = painelCombate.GetComponentInChildren<PlayerLogica>();

        //se encontrar, usa GerenciadorPlayer pra pegar os dados salvos
        if (scriptPlayer != null && GerenciadorPlayer.instance != null)
        {
            scriptPlayer.LerDados(GerenciadorPlayer.instance.statusAtuais);
        }
        else
        {
            Debug.LogError("Erro: O script PlayerLogica no PainelCombate / GerenciadorPlayer não existe");
        }
    }
}