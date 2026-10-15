using UnityEngine;

public class Player : MonoBehaviour
{
    //Variable que contiene la velocidad de movimeinto lateral
    public float veloMove = 5f;
    //Variable que contiene la velocidad de salto
    public float veloSalto = 10f;
    //Variable que da capatidad de saltar
    private bool salto = true;
    //Variable que contiene las propiedades fisicas
    Rigidbody2D rb;
    //Variable que contiene el collider del protagonista
    CapsuleCollider capsuleCollider;
    //Objeto auxiliar que los da una referencia para la ubicacion
    public GameObject auxPosition;
    //Variable que contiene la posicicon del objeto auxiliar
    Transform transAux;


    void Start()
    {
        //Primero obtenemos las propiedades fisicas
        rb = GetComponent<Rigidbody2D>();
        //Luego obtenemos las propiedades del capsule collider
        capsuleCollider = GetComponent<CapsuleCollider>();
        //Obtenemos el transform del auxiliar
        transAux = auxPosition.GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        salto = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        salto = false;
    }

    void Update()
    {
        //Primero analizamos si solo 1 o 2 estan tocando la pantalla
        if (Input.touchCount == 1)
        {
            //Primwero declaramos una variable que contenga el unico toque que tenemos
            Touch toque = Input.GetTouch(0);
            //Luego hacemos un vector que contenga la ubicacion del toque
            Vector2 posicionActual = Camera.main.ScreenToWorldPoint(toque.position);
            //Si el toque esta mas a la derecha que el objeto de referencia, entonces movemos el objeto a la derecha
            if (transAux.position.x < posicionActual.x)
            {
                //Cambiamos la velocidad
                rb.linearVelocityX = veloMove;
                //Ponemos la orientacion como para que el personaje vea adelante
                transform.localScale= new Vector3(1,1, 1);
            }
            //Si el toque esta mas a la izquierda que el objeto de referencia, entonces movemos el objeto a la izquierda
            if (posicionActual.x < transAux.position.x)
            {
                //Cambiamos la velocidad
                rb.linearVelocityX = -veloMove;
                //Ponemos la orientacion como para que el personaje vea adelante
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        //Definimos que pasara si hay dos toques en la pantalla
        if (Input.touchCount == 2)
        {
            //Definimos una variable que contenga el primer toque
            Touch toque1 = Input.GetTouch(0);
            //Definimos una segunda variable que tenga el segundo toque
            Touch toque2 = Input.GetTouch(1);
            // Obtenemos la posicion del primer toque con la camara
            Vector2 posicion1 = Camera.main.ScreenToWorldPoint(toque1.position);
            // Obtenemos la posicion del segundo toque con la camara
            Vector2 posicion2 = Camera.main.ScreenToWorldPoint(toque2.position);
            //Si cada un toque esta a la derecha y el otro a la izquierda, entonces se da el salto
            if((posicion1.x<transAux.position.x && transAux.position.x < posicion2.x) || (posicion2.x < transAux.position.x && transAux.position.x<posicion1.x) )
            {
                //Cambiamos la velocidad en y
                rb.linearVelocityY = veloSalto;
            }
        }
        
    }
}
