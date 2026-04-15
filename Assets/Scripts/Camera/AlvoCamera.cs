using UnityEngine;
using UnityEngine.InputSystem;

public class AlvoCamera : MonoBehaviour
{
    private Vector3 origem;
    private Vector3 posInicial;
    private bool arrasto = false;
    public Camera cam;
    public SeguirAlvo scriptCamera; 

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            arrasto = true;
            posInicial = transform.position;
            Vector2 mousePos = mouse.position.ReadValue();
            origem = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        }

        if (mouse.leftButton.isPressed && arrasto)
        {
            Vector2 mousePos = mouse.position.ReadValue();
            Vector3 mouseAtual = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            Vector3 diferenca = mouseAtual - origem;

            Vector3 novaPos = new Vector3(posInicial.x - diferenca.x, posInicial.y - diferenca.y, 0);

            if (scriptCamera != null)
            {
                novaPos.x = Mathf.Clamp(novaPos.x, scriptCamera.minX, scriptCamera.maxX);
                novaPos.y = Mathf.Clamp(novaPos.y, scriptCamera.minY, scriptCamera.maxY);
            }

            transform.position = novaPos;
        }

        if (mouse.leftButton.wasReleasedThisFrame) arrasto = false;
    }
}