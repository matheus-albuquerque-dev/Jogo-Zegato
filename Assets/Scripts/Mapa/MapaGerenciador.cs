using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MapaGerenciador : MonoBehaviour
{
    [Header("Player Config")]
    public RectTransform playerIcone;
    public float periodoMovimento = 0.5f;

    [Header("Mapa Config")]
    public int estagioAtual = 0;
    public List<NoMapa> listaNos;

    void Start()
    {
        //PosicionarPlayerNoInicio();
        AtualizarMapa();
    }

    public void OnStageClicked(NoMapa noClicado)
    {
        if (noClicado.noIndice == estagioAtual + 1 || noClicado.noIndice == estagioAtual) 
        {
            Movimento(noClicado);
        }
    }

    void Movimento(NoMapa noDestino)
    {
        StopAllCoroutines();
        Vector2 destinoPos = noDestino.GetComponent<RectTransform>().anchoredPosition;

        if (noDestino.noIndice > estagioAtual) 
        {
            estagioAtual = noDestino.noIndice;
        }

        StartCoroutine(DeslizeMovimento(noDestino));
    }

    IEnumerator DeslizeMovimento(NoMapa noDestino)
    {
        float tempoPassado = 0;
        Vector2 posicaoInicial = playerIcone.anchoredPosition;
        Vector2 destinoPos = noDestino.GetComponent<RectTransform>().anchoredPosition;

        while (tempoPassado < periodoMovimento)//transição, "deslize"
        {
            playerIcone.anchoredPosition = Vector2.Lerp(posicaoInicial, destinoPos, tempoPassado / periodoMovimento);
            tempoPassado += Time.deltaTime;
            yield return null;
        }

        playerIcone.anchoredPosition = destinoPos;

        if(noDestino.tipo == TipoNo.Inimigo){
            GerenciadorPainel.instancia.IniciarCombate(noDestino.dadosDoEncontro);
        } 
        else if(noDestino.tipo == TipoNo.Evento){
            //XML DE EVENTO
        }

        estagioAtual++;

        //após terminar o "deslize", atualiza o mapa
        AtualizarMapa();
    }

    void AtualizarMapa()
    {
        foreach (NoMapa no in listaNos)
        {
            no.AtualizarClicavel(estagioAtual); 
        }
    }

    void PosicionarPlayerNoInicio()
    {
        if (listaNos.Count > 0 && playerIcone != null)
        {
            playerIcone.anchoredPosition = listaNos[0].GetComponent<RectTransform>().anchoredPosition;
        }
    }
}