using UnityEngine;
using UnityEngine.UI;

public class BarraController : MonoBehaviour
{
    [SerializeField] private Image barraFill;
    [SerializeField] private bool circularidade = true;//deve ser marcado se a barra for circular, desmarcado se for reta
    [SerializeField] private float fatiaDoCirculo = 0.25f;
    [SerializeField] private float velocidadeLerp = 5f;

    //TESTE
    public float hpAtualTeste = 67f;
    public float hpMaxTeste = 100f;

    private float pontoFinal;

    public void AtualizarBarra(float valorAtual, float valorMaximo){
        float percentual = valorAtual / (valorMaximo <= 0 ? 1 : valorMaximo);//evita crash
        if (circularidade){pontoFinal = percentual * fatiaDoCirculo;}//barra circular
        else{pontoFinal = percentual;}//barra reta
    }

    void Update(){
        //TESTE
        AtualizarBarra(hpAtualTeste, hpMaxTeste);
        if (barraFill != null && !Mathf.Approximately(barraFill.fillAmount, pontoFinal)){//transição suave não redundante
            barraFill.fillAmount = Mathf.Lerp(barraFill.fillAmount, pontoFinal, Time.deltaTime * velocidadeLerp);
        }
    }
}