using UnityEngine;

public class SeguirAlvo : MonoBehaviour
{
    public Transform alvoCameraPos;
    public float suavidade = 0.125f;


    [Header("Bordas do Mapa")]
    public SpriteRenderer spriteDoMapa;
    [HideInInspector] public float minX, maxX, minY, maxY;
    private Camera cam;


    [Header("Troca Combate/Mapa")]
    public Vector3 posicaoCombate = new Vector3(0f, 16.7f, -15f);//posição da câmera durante o combate, pode ser alterada no inspector
    public float tamanhoCameraCombate = 3.604f;
    private readonly float tamanhoCameraMapa = 6.7f;

    [HideInInspector] public bool travaCombate = false;//pode ser mudada pelo "SetarModoCombate(bool)" caso o save carregado sinalize que estava em combate


    void Awake(){
        cam = GetComponent<Camera>();//autoreferenciamento
    }

    void Start(){
        if (!travaCombate)//se não estiver em combate
        {
            cam.orthographicSize = tamanhoCameraMapa;//põe no tamanho do mapa
        }
        CalcularLimites();//já calcular bordas para quando voltar/for ao mapa
    }

    // COMBATE
    public void SetarModoCombate(bool emCombate){
        travaCombate = emCombate;
        cam.orthographicSize = emCombate ? tamanhoCameraCombate : tamanhoCameraMapa;

        if (emCombate){
            MoverCameraEAlvo(posicaoCombate);
        }
        else{
            CalcularLimites();//recalcula limites pois o tamanho da camera mudou
            
            //"teleporte instantâneo" para AlvoCamera no mapa para evitar o deslize suave vindo do combate
            if (alvoCameraPos != null){
                MoverCameraEAlvo(new Vector3(alvoCameraPos.position.x, alvoCameraPos.position.y, transform.position.z));
            }
        }
    }

    //Método auxiliar, move ambos mantendo o Z correto
    private void MoverCameraEAlvo(Vector3 novaPos){
        transform.position = new Vector3(novaPos.x, novaPos.y, novaPos.z);
        if (alvoCameraPos != null){
            alvoCameraPos.position = new Vector3(novaPos.x, novaPos.y, 0);
        }
    }

    //Seguir alvoCameraPos
    void LateUpdate(){
        if (travaCombate || alvoCameraPos == null){return;}//garantia ou se estiver em combate, não segue AlvoCamera

        Vector3 posicaoDesejada = new Vector3(alvoCameraPos.position.x, alvoCameraPos.position.y, transform.position.z);

        //Clamp conforme limites da imagem (mapa)
        posicaoDesejada.x = Mathf.Clamp(posicaoDesejada.x, minX, maxX);
        posicaoDesejada.y = Mathf.Clamp(posicaoDesejada.y, minY, maxY);

        //faz o movimento suave
        transform.position = Vector3.Lerp(transform.position, posicaoDesejada, suavidade);
    }

    // BORDAS
    public void CalcularLimites(){
        if (spriteDoMapa == null){return;}//garantia
        float alturaCam = cam.orthographicSize;
        float larguraCam = alturaCam * cam.aspect;
        Bounds limitesMapa = spriteDoMapa.bounds;
        minX = limitesMapa.min.x + larguraCam;
        maxX = limitesMapa.max.x - larguraCam;
        minY = limitesMapa.min.y + alturaCam;
        maxY = limitesMapa.max.y - alturaCam;
    }
}