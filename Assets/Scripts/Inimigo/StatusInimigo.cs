using UnityEngine;

[CreateAssetMenu(fileName = "NovoStatusInimigo", menuName = "COMBATE/StatusInimigo")]
public class StatusInimigo : ScriptableObject
{
    public string nomeInimigoSalvo;
    public int hpAtualInimigoSalvo;
    public int hpMaxInimigoSalvo;
    public int danoInimigoSalvo;

    /*public void ResetarStatus()
    {
        hpAtualInimigoSalvo = hpMaxInimigoSalvo;
    }*/
}