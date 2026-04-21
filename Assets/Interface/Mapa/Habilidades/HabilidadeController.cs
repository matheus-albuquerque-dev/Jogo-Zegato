using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CartaController : MonoBehaviour
{
    private Vector3 posOriginal;
    private Quaternion rotOriginal;
    private Vector3 escalaOriginal;
    private int indexOriginal;
    private Transform paiOriginal;
    
    private bool isSelecionada = false;
    private bool estaAnimando = false;////evita spam
    
    [SerializeField] private float escalaZoom = 3.2f;
    [SerializeField] private float tempoAnimacao = 0.5f; // Ajuste o tempo se quiser mais rápido
    private Transform canvas;//para pôr no centro

    void Start(){
        escalaOriginal = transform.localScale;
        rotOriginal = transform.localRotation;
        canvas = GetComponentInParent<Canvas>().transform;
        paiOriginal = transform.parent;
        indexOriginal = transform.GetSiblingIndex();
    }

    public void SelecaoHabilidade(){//clicando na carta
        if (estaAnimando) return;//evita spam

        if (isSelecionada) StartCoroutine(UnzoomAnimado());//volta
        else StartCoroutine(ZoomAnimado());//ida
    }

    IEnumerator ZoomAnimado()
    {
        estaAnimando = true;
        isSelecionada = true;

        //posição original da carta
        posOriginal = transform.position;

        ///para aparecer na frente
        transform.SetParent(canvas, true); 
        transform.SetAsLastSibling();

        //centro da tela
        Vector3 centroDaTela = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        float tempoDecorrido = 0;
        while (tempoDecorrido < tempoAnimacao)
        {
            tempoDecorrido += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, tempoDecorrido / tempoAnimacao);//animação suave

            transform.position = Vector3.Lerp(posOriginal, centroDaTela, t);//movimento para o centro
            transform.localScale = Vector3.Lerp(escalaOriginal, escalaOriginal * escalaZoom, t);//aumento de tamanho
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 360, t));//giro
            
            yield return null;
        }
        estaAnimando = false;
    }

    IEnumerator UnzoomAnimado()
    {
        estaAnimando = true;
        isSelecionada = false;

        Vector3 centroDaTela = transform.position;

        float tempoDecorrido = 0;
        while (tempoDecorrido < tempoAnimacao)
        {
            tempoDecorrido += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, tempoDecorrido / tempoAnimacao);//animação suave

            transform.position = Vector3.Lerp(centroDaTela, posOriginal, t);//movimento de volta para a posição original
            transform.localScale = Vector3.Lerp(escalaOriginal * escalaZoom, escalaOriginal, t);//diminuição de tamanho
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(360, 720, t));//giro inverso
            
            yield return null;
        }

        //volta na hierarquia (layout group)
        transform.SetParent(paiOriginal, true);
        transform.SetSiblingIndex(indexOriginal);
        transform.localScale = escalaOriginal;
        transform.localRotation = rotOriginal;
        
        estaAnimando = false;
    }
}