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
    //Camara 1 que que vizualiza al personaje
    public Camera cam1;
    //Varieble que contiene la segunda camara que ve al personaje
    public Camera cam2;
    //Objeto auxiliar que los da una referencia para la ubicacion
    public GameObject auxPosition;
    //Variable que contiene la posicicon del objeto auxiliar
    Transform transAux;
    //Variable auxiliar que contiene la posicion original en la que se encontraba la camara
    Vector3 posCam;
    //Variable auxiliar que contiene la posicion en la cual se empieza a mover la camara
    Vector3 puntoMove;
    //Variable que contiene el script de control de camaras
    public ControlCamaras controladorCamaras;


    void Start()
    {
        //Primero obtenemos las propiedades fisicas
        rb = GetComponent<Rigidbody2D>();
        //Luego obtenemos las propiedades del capsule collider
        capsuleCollider = GetComponent<CapsuleCollider>();
        //Obtenemos el transform del auxiliar
        transAux = cam1.GetComponent<Transform>();
        //Obtenemos la posicion inicial de la camara
        posCam = cam1.transform.position;
        //Punto donde se empieza a mover la camara
        puntoMove=auxPosition.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso") || collision.gameObject.CompareTag("Plataforma"))
        {
            salto = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Piso") || collision.gameObject.CompareTag("Plataforma"))
        {
            salto = false;
        }
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
                //Nota el siguiente proceso se hace con el fin de que la camara este estaticas hasta que lleguemos al punto donde se empieza a mover
                //Para que el movimiento de la camara con el personaje se sienta mas natural y alineado
                //Si el jugador esta antes de el punto de referencia, entonces la camara esta estatica
                if (puntoMove.x < transform.position.x&&!cam2.gameObject.activeSelf)
                {
                    //Fijamos la posicion de la camara
                    controladorCamaras.activarCamara2();
                }
                //Cambiamos la velocidad
                rb.linearVelocityX = veloMove;
                //Ponemos la orientacion como para que el personaje vea adelante
                transform.localScale= new Vector3(1,1, 1);
            }
            //Si el toque esta mas a la izquierda que el objeto de referencia, entonces movemos el objeto a la izquierda
            if (posicionActual.x < transAux.position.x)
            {
                //Si el jugador esta antes de el punto de referencia, entonces la camara esta estatica
                if (puntoMove.x < transform.position.x && !cam2.gameObject.activeSelf)
                {
                    //Fijamos la posicion de la camara
                    controladorCamaras.activarCamara2();
                }
                //Cambiamos la velocidad
                rb.linearVelocityX = -veloMove;
                
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
            if(((posicion1.x<transAux.position.x && transAux.position.x < posicion2.x) || (posicion2.x < transAux.position.x && transAux.position.x<posicion1.x)) && Mathf.Abs(rb.linearVelocityY) < 0.01f&&salto==true)
            {
                //Cambiamos la velocidad en y
                rb.linearVelocityY = veloSalto;
                //Cambiamos la capacidad de salto a false
                salto = false;
            }
        }
        
    }
}
