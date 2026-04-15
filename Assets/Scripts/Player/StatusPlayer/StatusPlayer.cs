using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NovoStatusPlayer", menuName = "COMBATE/StatusPlayer")]
public class StatusPlayer : ScriptableObject
{
    public string nomePlayerSalvo;
    public int hpAtualPlayerSalvo;
    public int hpMaxPlayerSalvo;
    public List<Habilidade> habilidadesAtuaisPlayerSalvo;

    /*public void ResetarStatus()
    {
        hpAtualPlayerSalvo = hpMaxPlayerSalvo;
        habilidadesAtuaisPlayerSalvo.Clear();
    }*/

    public void AprenderHabilidade(Habilidade nova)
    {
        if (habilidadesAtuaisPlayerSalvo.Count < 4)
        {
            habilidadesAtuaisPlayerSalvo.Add(nova);
        }
        else
        {
            Debug.Log("Limite atingido! Escolha uma para trocar.");
        }
    }
}