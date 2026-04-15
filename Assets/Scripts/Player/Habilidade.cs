using UnityEngine;

[CreateAssetMenu(fileName = "NovaHabilidade", menuName = "COMBATE/Habilidade")] //permite criar Habilidades diferentes no menu Assets
public class Habilidade : ScriptableObject
{
    public string nomeHabilidade;
    public string descricaoHabilidade;
    public int danoHabilidade;
    public int custoStaminaHabilidade;
    public Sprite iconeHabilidade;
}