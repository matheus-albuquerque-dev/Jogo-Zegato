using UnityEngine;

public enum TipoNo 
    { 
        Inimigo, 
        Evento, 
        Descanso,
        Loja
    }

public class NoMapa : MonoBehaviour
{
    private GerenciadorMapa gerenciadorMapa;
    public int noIndice;
    public bool isDesbloqueado;
    public TipoNo tipoNo;
    public ScriptableObject dadosDoEncontro;

    void Awake(){
        gerenciadorMapa = FindAnyObjectByType<GerenciadorMapa>();//linka sozinho ao GerenciadorMapa
    }

    //HOVER
    void OnMouseEnter()
    {
        if (isDesbloqueado) GetComponent<SpriteRenderer>().color = Color.skyBlue;
    }
    void OnMouseExit()
    {
        if (isDesbloqueado) GetComponent<SpriteRenderer>().color = Color.white;
    }

    void OnMouseDown(){
        if(isDesbloqueado){
            gerenciadorMapa.OnStageClicked(this);//informa ao gerenciador que clicou neste nó
        }
    }

    public void AtualizarClicavel(int estagioPermitido){
        isDesbloqueado = (noIndice == estagioPermitido);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = isDesbloqueado ? Color.white : new Color(0.3f, 0.3f, 0.3f, 1f);
        }
    }
}