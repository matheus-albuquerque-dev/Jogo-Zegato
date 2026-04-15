using UnityEngine;
using System.Collections;

public enum EstadoCombate{
    LOADING, //GARANTE QUE SÓ COMEÇA DEPOIS DE CONFIGURAR TUDO
    TURNO_PLAYER, 
    TURNO_INIMIGO, 
    VITORIA, 
    DERROTA 
}

public class GerenciadorCombate : MonoBehaviour 
{
    private WaitForSeconds pausaAnimacao;//evitar aviso das coroutines
    private WaitForSeconds pausaLog;//evitar aviso das coroutines

    public EstadoCombate estadoAtual;
    
    // Referências aos combatentes
    public PlayerLogica player;
    public InimigoLogica inimigo;

    public StatusPlayer dadosDoPlayer;
    public StatusInimigo dadosDoInimigo;

    void Awake(){
        //garante que funciona corretamente na Unity
        pausaAnimacao = new WaitForSeconds(4.0f);
        pausaLog = new WaitForSeconds(1.0f);
    }

    void Start(){
        estadoAtual = EstadoCombate.LOADING;
        if(player != null && inimigo != null){//garantia de que ambos estão linkados
            CarregarCombate();
        }
        else{
            Debug.LogError("Player/Inimigo não linkados no GerenciadorCombate. Verificar Inspector.");
        }
    }

    void CarregarCombate(){
        player.LerDados(dadosDoPlayer);
        inimigo.LerDados(dadosDoInimigo);//TODO: deverá ser passado pelo nó

        estadoAtual = EstadoCombate.TURNO_PLAYER;
        StartCoroutine(TurnoPlayer());
    }

    /*FUTURAMENTE, O PLAYER VAI ACIONAR ISSO
    public void ComandoAtacar(){
        if (estadoAtual != EstadoCombate.TURNO_PLAYER) return;

        StartCoroutine(TurnoPlayer());
    }
    */

    IEnumerator TurnoPlayer(){
        if (estadoAtual != EstadoCombate.TURNO_PLAYER) yield break;//evita spam do player

        player.AnimacaoAtaque();//ANIMAÇÃO COM SPRITESHEE, ENQUANTO RIVE NAO TA PRONTO
        yield return pausaAnimacao;//DEVE SER O TEMPO DA ANIMAÇÃO DO RIVE

        inimigo.ReceberDano(5);//TODO: O DANO DEVE VIR DA HABILIDADE SELECIONADA, "10" É SÓ PRA TESTAR
        yield return pausaLog;//TEMPO PARA "LOG" ("PLAYER ATACOU / INIMIGO SOFREU X DANO")

        //checa se o inimigo morreu
        if (inimigo.hpAtualInimigo <= 0){
            estadoAtual = EstadoCombate.VITORIA;
            FinalizarCombate();
        } else{
            estadoAtual = EstadoCombate.TURNO_INIMIGO;
            StartCoroutine(TurnoInimigo());
        }
    }

    IEnumerator TurnoInimigo(){
        if (estadoAtual != EstadoCombate.TURNO_INIMIGO) yield break;//evita spam do inimigo

        inimigo.AnimacaoAtaque();//ANIMAÇÃO COM SPRITESHEE, ENQUANTO RIVE NAO TA PRONTO
        yield return pausaAnimacao;//DEVE SER O TEMPO DA ANIMAÇÃO DO RIVE

        player.ReceberDano(inimigo.danoInimigo);
        yield return pausaLog;//TEMPO PARA "LOG" ("PLAYER ATACOU / INIMIGO SOFREU X DANO")

        //checa se o player morreu
        if (player.hpAtualPlayer <= 0){
            estadoAtual = EstadoCombate.DERROTA;
            FinalizarCombate();
        } else {
            estadoAtual = EstadoCombate.TURNO_PLAYER;
            StartCoroutine(TurnoPlayer());
        }
    }

    void FinalizarCombate(){
        if (estadoAtual == EstadoCombate.VITORIA) Debug.Log("Ganhou!");
        else Debug.Log("Perdeu!");
    }
}