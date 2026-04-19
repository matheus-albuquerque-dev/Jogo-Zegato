using UnityEngine;

public class GerenciadorTela : MonoBehaviour
{
    public static GerenciadorTela instanciaGerenciadorTela;

    [Header("Configurações de UI")]
    public GameObject objetoMapa;
    public GameObject objetoCombate;

    [Header("Câmera do Jogo")]
    public SeguirAlvo scriptDaCamera;

    [Header("Interfaces")]
    public GameObject hudMapa;
    public GameObject hudCombate;

    private void CenaCombate(bool emCombate){
        hudCombate.SetActive(emCombate);
        hudMapa.SetActive(!emCombate);
        objetoCombate.SetActive(emCombate);
        objetoMapa.SetActive(!emCombate);
    }

    void Awake(){//Singleton
        if (instanciaGerenciadorTela == null){
            instanciaGerenciadorTela = this;
        } else{
            Destroy(gameObject);
        }
    }

    //COMBATE
    public ScriptableObject inimigoAtual;//lê isso para saber o inimigo

    public void IniciarCombate(ScriptableObject dadosInimigo){
        inimigoAtual = dadosInimigo;

        if(scriptDaCamera != null){
            scriptDaCamera.SetarModoCombate(true);
        } else {
            Debug.LogError("Faltou linkar a câmera no gerenciador de tela.");
        }

        //pega PlayerLogica dentro de objetoCombate
        PlayerLogica scriptPlayer = objetoCombate.GetComponentInChildren<PlayerLogica>();

        //se encontrar, usa GerenciadorPlayer pra pegar os dados salvos
        if (scriptPlayer != null && GerenciadorPlayer.instance != null){
            scriptPlayer.LerDados(GerenciadorPlayer.instance.statusAtuais);
        }
        else{
            Debug.LogError("Erro: O script PlayerLogica no objetoCombate/GerenciadorPlayer não existe.");
            return;
        }

        //ativa HUD e objeto de combate
        CenaCombate(true);
    }

    public void VoltarMapa(){
        objetoCombate.SetActive(false);
        objetoMapa.SetActive(true);
        if(scriptDaCamera != null){
            scriptDaCamera.SetarModoCombate(false);
        }
    }
}