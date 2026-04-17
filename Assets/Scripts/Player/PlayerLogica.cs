using UnityEngine;
using System.Collections.Generic;

public class PlayerLogica : MonoBehaviour
{
    [Header("Dados do Player")]
    public string nomePlayer;
    public int hpAtualPlayer;
    public int hpMaxPlayer;
    public List<Habilidade> habilidadesParaLutaPlayer;

    public Rive.Components.RivePanel visualRive;//para animação "conversar" com lógica

    private StatusPlayer dadosOriginais;//para registrar os dados durante o combate

    public void LerDados(StatusPlayer dadosStatusPlayer){
        if (dadosStatusPlayer == null){
            Debug.LogError("ERRO: O ScriptableObject de Status não chegou no PlayerLogica!");
            return;
        }

        dadosOriginais = dadosStatusPlayer;//para registrar os dados durante o combate

        nomePlayer = dadosStatusPlayer.nomePlayerSalvo;
        hpAtualPlayer = dadosStatusPlayer.hpAtualPlayerSalvo;
        hpMaxPlayer = dadosStatusPlayer.hpMaxPlayerSalvo;

        habilidadesParaLutaPlayer = new List<Habilidade>(dadosStatusPlayer.habilidadesAtuaisPlayerSalvo);//cópia do que foi salvo, para lutar

        Debug.Log($"Combate iniciado! {nomePlayer} tem {hpAtualPlayer} de HP e {habilidadesParaLutaPlayer.Count} habilidades.");
        // TODO: Chamar Rive para setar visual
    }

    public void ReceberDano(int danoInimigo){
        hpAtualPlayer -= danoInimigo;
        if (hpAtualPlayer < 0){hpAtualPlayer = 0;}//garante que não fica negativo
        
        //REGISTRO NO SCRIPTABLEOBJECT
        if (dadosOriginais != null){//evita crash
            dadosOriginais.hpAtualPlayerSalvo = hpAtualPlayer;
            // !!!TODO: PRECISA DE JSON/PlayerPrefs PARA SALVAR DE VERDADE, SE NÃO OS DADOS VÃO VOLTAR AO ORIGINAL AO REINICIAR O JOGO
            //| Opcional | Avisa Unity que o arquivo mudou (para não perder dados se o editor fechar)
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(dadosOriginais);
            #endif
        }
        Debug.Log($"Dano recebido! HP atual: {hpAtualPlayer}");

        if(hpAtualPlayer == 0){
            // TODO: ANIMAÇÃO e CENA de Morte no Rive e no resto do jogo pra dar um game over, não necessariamente escrito aqui, mas pelo menos o "link" pra ativar, um "sinal"
        }
    }

    //POR ENQUANTO USANDO SPRITESHEET, ENQUANTO RIVE NÃO VEM
    public Animator Animador; 

    public void AnimacaoAtaque(){
        if (Animador != null){
            Animador.SetTrigger("atacar"); 
        }
        else{
            Debug.LogError($"O objeto {gameObject.name} não tem um Animador linkado.");
        }
    }
}