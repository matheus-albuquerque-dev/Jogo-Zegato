using UnityEngine;

public class SeguirAlvo : MonoBehaviour
{
    public Transform alvo;
    public float suavidade = 0.125f;

    [Header("Bordas do Mapa")]
    public SpriteRenderer spriteDoMapa;
    
    [HideInInspector] public float minX, maxX, minY, maxY;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        CalcularLimites();
    }
    //BORDAS
    public void CalcularLimites()
    {
        if (spriteDoMapa == null) return;
        float alturaCam = cam.orthographicSize;
        float larguraCam = alturaCam * cam.aspect;
        Bounds limitesMapa = spriteDoMapa.bounds;
        minX = limitesMapa.min.x + larguraCam;
        maxX = limitesMapa.max.x - larguraCam;
        minY = limitesMapa.min.y + alturaCam;
        maxY = limitesMapa.max.y - alturaCam;
    }
    //Seguir o alvo
    void LateUpdate()
    {
        if (alvo == null) return;

        Vector3 posicaoDesejada = new Vector3(alvo.position.x, alvo.position.y, transform.position.z);

        //Clamp conforme limites da imagem (mapa)
        posicaoDesejada.x = Mathf.Clamp(posicaoDesejada.x, minX, maxX);
        posicaoDesejada.y = Mathf.Clamp(posicaoDesejada.y, minY, maxY);

        Vector3 posicaoSuave = Vector3.Lerp(transform.position, posicaoDesejada, suavidade);
        transform.position = posicaoSuave;
    }
}