using UnityEngine;

public class GerenciadorPlayer : MonoBehaviour{

    public static GerenciadorPlayer instance;
    public StatusPlayer statusAtuais;

    void Awake(){
        //SINGLETON
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//mantém o objeto ao mudar de cena
        }
        else
        {
            Destroy(gameObject);
        }
    }

}