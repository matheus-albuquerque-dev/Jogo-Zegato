using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GerenciadorMapa : MonoBehaviour
{
    [Header("Player Config")]
    public Transform playerIcone;
    public float periodoMovimento = 0.5f;

    [Header("Mapa Config")]
    public int estagioAtual = 0;
    public List<NoMapa> listaNos;

    void Start(){AtualizarMapa();}

    #if UNITY_EDITOR//ATUALIZA LISTA NÓS NO MAPA AUTOMATICAMENTE DE FORMA SEGURA, SEM RISCO DE LOOP INFINITO
        private void OnValidate(){
            if (Application.isPlaying){return;}//Unity ocupada ou em PlayMode

            UnityEditor.EditorApplication.delayCall += () => {//atualiza no próximo "respiro" da Unity
                if (this == null){return;}//se o objeto foi deletado, cancela
                ConfigurarMapa();
            };
        }

        private void ConfigurarMapa(){
            //busca e ordenação dos nós, sem renomear para não loopar
            NoMapa[] nosEncontrados = FindObjectsByType<NoMapa>(FindObjectsInactive.Include);
            
            if (nosEncontrados.Length > 0){
                List<NoMapa> tempLista = new List<NoMapa>(nosEncontrados);
                tempLista.Sort((a, b) => a.noIndice.CompareTo(b.noIndice));
                
                //só atualiza se houve mudança na quantidade
                if (listaNos == null || listaNos.Count != tempLista.Count){
                    listaNos = tempLista;
                    Debug.Log("Mapa configurado: " + listaNos.Count + " nós encontrados.");
                }
            }
        }
    #endif

    public void OnStageClicked(NoMapa noClicado){
        if (noClicado.noIndice == estagioAtual){
            Movimento(noClicado);
        }
    }

    void Movimento(NoMapa noDestino){
        StopAllCoroutines();
        StartCoroutine(DeslizeMovimento(noDestino));
    }

    IEnumerator DeslizeMovimento(NoMapa noDestino)
    {
        float tempoPassado = 0;
        Vector3 posicaoInicial = playerIcone.position;
        Vector3 destinoPos = noDestino.transform.position;

        while (tempoPassado < periodoMovimento){
            playerIcone.position = Vector3.Lerp(posicaoInicial, destinoPos, tempoPassado / periodoMovimento);
            tempoPassado += Time.deltaTime;
            yield return null;
        }

        playerIcone.position = destinoPos;

        if(noDestino.tipoNo == TipoNo.Inimigo){
            if (noDestino.dadosDoEncontro == null){
                Debug.LogError($"O nó {noDestino.name} não tem ScriptableObject atribuído.");
            } else{
                GerenciadorTela.instanciaGerenciadorTela.IniciarCombate(noDestino.dadosDoEncontro);
            }
        }
        //TODO:
        //if(noDestino.tipo == TipoNo.Choice){
        //if(noDestino.tipo == TipoNo.MiniBoss){
        //if(noDestino.tipo == TipoNo.Boss){

        // Se o nó clicado era o próximo, sobe o nível do mapa
        if (noDestino.noIndice == estagioAtual){
            estagioAtual++;
        }

        AtualizarMapa();
    }

    void AtualizarMapa(){
        foreach (NoMapa no in listaNos){
            no.AtualizarClicavel(estagioAtual); 
        }
    }
}