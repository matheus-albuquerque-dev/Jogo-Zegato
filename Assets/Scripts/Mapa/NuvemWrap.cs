using UnityEngine;

public class NuvemWrap : MonoBehaviour
{
    public float fatorParallax = 0.2f;
    public float velocidadeVento = 0.05f;
    
    private Material materialNuvem;
    private Transform cam;
    private float offsetVento;

    void Start()
    {
        cam = Camera.main.transform;
        materialNuvem = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        offsetVento += velocidadeVento * Time.deltaTime;//movimento das nuvens(vento)
        //PARALLAX
        float xFinal = offsetVento + (cam.position.x * fatorParallax * 0.1f);
        float yFinal = (cam.position.y * fatorParallax * 0.1f);

        materialNuvem.mainTextureOffset = new Vector2(xFinal, yFinal);//offset na textura
    }
}