using UnityEngine;
using System.Collections.Generic;

public class InimigoLogica : MonoBehaviour
{
    [Header("Dados do Inimigo")]
    public string nomeInimigo;
    public int hpAtualInimigo;
    public int hpMaxInimigo;
    public int danoInimigo;

    public Rive.Components.RivePanel visualRive;//para animação "conversar" com lógica

    private StatusInimigo dadosOriginais; //para registrar os dados durante o combate

    public void LerDados(StatusInimigo dadosStatusInimigo)
    {
        if (dadosStatusInimigo == null) {
            Debug.LogError("ERRO: O ScriptableObject de Status não chegou no InimigoLogica!");
            return;
        }

        dadosOriginais = dadosStatusInimigo; //para registrar os dados durante o combate

        nomeInimigo = dadosStatusInimigo.nomeInimigoSalvo;
        hpAtualInimigo = dadosStatusInimigo.hpAtualInimigoSalvo;
        hpMaxInimigo = dadosStatusInimigo.hpMaxInimigoSalvo;
        danoInimigo = dadosStatusInimigo.danoInimigoSalvo;

        Debug.Log($"Combate iniciado! {nomeInimigo} tem {hpAtualInimigo} de HP.");
        // TODO: Chamar Rive para setar visual
    }

    public void ReceberDano(int danoPlayer)
    {
        hpAtualInimigo -= danoPlayer;
        if (hpAtualInimigo < 0) hpAtualInimigo = 0;//garante que não fica negativo

        //REGISTRO NO SCRIPTABLEOBJECT
        if (dadosOriginais != null)//evita crash
        {
            dadosOriginais.hpAtualInimigoSalvo = hpAtualInimigo;
            // !!!TODO: PRECISA DE JSON/PlayerPrefs PARA SALVAR DE VERDADE, SE NÃO OS DADOS VÃO VOLTAR AO ORIGINAL AO REINICIAR O JOGO
            //| Opcional | Avisa Unity que o arquivo mudou (para não perder dados se o editor fechar)
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(dadosOriginais);
            #endif
        }
        Debug.Log($"Dano recebido! HP atual: {hpAtualInimigo}");

        if(hpAtualInimigo == 0) 
        {
            // TODO: ANIMAÇÃO e CENA de Morte no Rive e no resto do jogo pra dar um game over, não necessariamente escrito aqui, mas pelo menos o "link" pra ativar, um "sinal"
        }
    }

    public Animator Animador; 

    public void AnimacaoAtaque()
    {
        if (Animador != null)
        {
            Animador.SetTrigger("atacar"); 
        }
    }
}