using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoSons : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public GerenciadorAudio audioManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioManager.Hover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioManager.Clique();
    }
}