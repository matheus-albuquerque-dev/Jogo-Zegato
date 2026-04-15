using UnityEngine;
using UnityEngine.UI;

public enum TipoNo 
    { 
        Inimigo, 
        Evento, 
        Descanso, //se houver
        Loja //se houver
    }

public class NoMapa : MonoBehaviour
{
    public Button botao;
    private MapaGerenciador gerenciador;

    public int noIndice;
    public bool isDesbloqueado;

    public TipoNo tipo;
    public ScriptableObject dadosDoEncontro; //inimigo/evento/coisa específica

    void Awake(){
        botao = GetComponent<Button>();
        gerenciador = FindAnyObjectByType<MapaGerenciador>();
        botao.onClick.AddListener(AoClicar);
    }

    void AoClicar() => gerenciador.OnStageClicked(this);

    public void AtualizarClicavel(int estagioMaximo){
        isDesbloqueado = noIndice <= estagioMaximo;
        botao.interactable = isDesbloqueado;
    }
}