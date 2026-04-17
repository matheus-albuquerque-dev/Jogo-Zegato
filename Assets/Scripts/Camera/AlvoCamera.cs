using UnityEngine;
using UnityEngine.InputSystem;

public class AlvoCamera : MonoBehaviour
{
    private Vector3 origem;
    private Vector3 posInicial;
    private bool arrasto = false;
    public Camera cam;
    public SeguirAlvo scriptCamera; 

    void Awake(){
        if (cam == null) cam = Camera.main;
        //se a câmera não tiver sido atribuída no Inspector, tenta usar 'Main Camera'. Se ainda assim não tiver, avisa erro
        if (cam == null) Debug.LogError("AlvoCamera sem câmera atrelada. Disponha uma em seu Inspector.");
    }

    void Update(){
        if (scriptCamera != null && scriptCamera.travaCombate){return;} //EM COMBATE, trava o alvo

        var mouse = Mouse.current;
        if (mouse == null) {return;} //garantia de que tem mouse

        if (mouse.leftButton.wasPressedThisFrame) //assim que clicar
        {
            arrasto = true; //começo do arrasto
            posInicial = transform.position; //guarda posição inicial do AlvoCamera
            Vector2 mousePos = mouse.position.ReadValue(); //leitura do deslocamente do mouse
            origem = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f)); //conversão para posição no mundo
        }

        if (mouse.leftButton.isPressed && arrasto){//durante o arrasto
            Vector2 mousePos = mouse.position.ReadValue(); //atualização constante da posição mouse
            Vector3 mouseAtual = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f)); //conversão para posição no mundo
            Vector3 diferenca = mouseAtual - origem; //calcula diferença entre posição atual do mouse e posição onde começou o arrasto

            //cálculo da nova posição [inversão (-) para deixar natural]
            //soma dos vetores <AlvoCamera-distância original clique> + <clique-posição do mouse no mundo>, durante o arrasto
            Vector3 novaPos = new Vector3(posInicial.x - diferenca.x, posInicial.y - diferenca.y, 0);

            if (scriptCamera != null)//trava das bordas APENAS se o scriptCamera existir
            {
                novaPos.x = Mathf.Clamp(novaPos.x, scriptCamera.minX, scriptCamera.maxX);
                novaPos.y = Mathf.Clamp(novaPos.y, scriptCamera.minY, scriptCamera.maxY);
            }
            transform.position = novaPos;//aplicação ao AlvoCamera
        }

        if (mouse.leftButton.wasReleasedThisFrame) arrasto = false;//mouse solto, sem movimento
    }
}