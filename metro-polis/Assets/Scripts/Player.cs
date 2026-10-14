using UnityEngine;

public class Player : MonoBehaviour
{
    //Variable que contiene la velocidad de movimeinto lateral
    public float veloMove=5f;
    //Variable que contiene la velocidad de salto
    public float veloSalto=10f;
    //Variable que da capatidad de saltar
    private bool salto=true;
    //Variable que contiene las propiedades fisicas
    Rigidbody2D rb;
    //Variable que contiene el collider del protagonista
    CapsuleCollider capsuleCollider;
   
    void Start()
    {
       //Primero obtenemos las propiedades fisicas
       rb = GetComponent<Rigidbody2D>();
       //Luego obtenemos las propiedades del capsule collider
       capsuleCollider=GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        
    }
}
