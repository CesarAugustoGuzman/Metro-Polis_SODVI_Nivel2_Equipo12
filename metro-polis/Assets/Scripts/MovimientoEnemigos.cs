using UnityEngine;

public class MovimientoEnemigos : MonoBehaviour
{
    // Variable que define la distancia en la cual el personaje y el enemigo tienen que estar separados
    //Para que se mueva
    public float distancia;
    // Variable que contiene el radio de la vecindad en la cual se mueve el personaje
    public float vecindad;
    //Velocidad a la que camina el enemigo
    public float velo;
    //Variable auxiliar que autoriza un giro de direccion del personaje
    private bool giro = true;
    // Variable que contiene si el personaje avanza o no
    private bool avanza = false;
    //Varibale que contiene el rigidbody del enemigo
    Rigidbody2D rb;
    // Variable que contiene la posicion inicial del enemigo
    Vector3 posicionInicial;
    // Variable que contiene la posicion del jugador
    public Transform Player;
    void Start()
    {
        //Obtenemos el rigidbody del personaje
        rb=GetComponent<Rigidbody2D>();
        //Obtenemos la posicion inicial del enemigo
        posicionInicial = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto contra el cual se choca es un obstaculos, entonces se cambia la direccion
        //Esto solo sucede si es una multitud
        if (collision.gameObject.CompareTag("Obstaculo") && gameObject.CompareTag("Multitud"))
        {
            //Invertimos la velocidad
            velo = -1 * velo;
        }
    }
    void Update()
    {
        //Separamos las acciones por el tipo de tag que tiene el personaje
        switch (gameObject.transform.tag) 
        {
            //Caso donde estamos controlando una cucaracha
            case "Cucaracha":
                //Invocamos el movimiento de la cucaracha
                MoveCucaracha();
                break;
            case "Multitud":
                //Invocamos el movimiento de la multitud
                MoveMultitud();
                break;
            case "Rata":
                //Invocamos el movimiento de la rata
                MoveRat();
                break;
        }
    }
    private void DetectorProta()
    {
        //Punto desde el cual se detecta al personaje por la derecha
        float puntoA= posicionInicial.x+distancia;
        //Punto por el cual se detecta al personaje por la izquierda
        float puntoB= posicionInicial.x-distancia;
        //Si el personaje esta dentro del area de avanzar entonces se puede empezar a mover
        if (puntoB<Player.position.x && Player.position.x<puntoA) 
        { 
            //Autorizamos el avance
            avanza= true;
        }
        else //En caso contrario no se puede avanzar
        {
            //Denegamos el avance
            avanza= false;
        }
    }
    private void MoveCucaracha()
    {
        //Descripcion de comportamiento: La cucaracha se mueve de un lado a otro alrededro de una vecindad de radio determinado
        // centrado en el punto inicial donde estaba la cuacaracha
        //Proceso:
        //Primero llamamos a la funcion que detecta al protagonista
        DetectorProta();
        //Variable auxiliar que contiene el limite izquierdo
        float limIz = posicionInicial.x - vecindad;
        //Variable auxiliar que contiene el limite derecho
        float limDe = posicionInicial.x + vecindad;
        //Iniciamos el movimiento si se autoriza el avance
        //Si el movimiento no esta autorizado, entonces la velocidad no cambia
        if (!avanza)
        {
            rb.linearVelocityX =0f;
        }
        else //Si esta autorizado, empezamos el moviento
        {
            //Si el enemigo se encuentra en la vecindad la velocidad sigue siendo la misma
            if (limIz < transform.position.x && transform.position.x < limDe)
            {
                //Su velocidad es la misma 
                rb.linearVelocityX = velo;
                //Cambiamos o mantenemos el giro como true
                giro = true;
            }
            else//Sino, invertimos la velocidad para que vaya para el otro lado
            {
                // Si el giro es verdadero, entonces invertimos la velocidad
                if (giro)
                {
                    //Primero paramos al enemigo
                    rb.linearVelocityX = 0f;
                    //Invertimos la velocidad
                    velo = -1 * velo;
                    // Luego invertimos la velocidad 
                    rb.linearVelocityX = velo;
                    // Ya no se puede hacer mas giros, hasta que se vuelva a la vecindad
                    giro = false;
                }
            }
        }
    }
    public void MoveMultitud()
    {
        //Descripcion del movimiento: las multitudes solo caminan en uns direccion cuando detenctan que el prota esta cerca
        // Este solo cambia su direccion si choca contra algun obstaculo
        //Primero detectamos si el prota esta cerca
        DetectorProta();
        //Si el avance no esta autorizado, entonces la multitud se queda quieta
        if (!avanza)
        {
            //Mantenemos quieto a la multitud
            rb.linearVelocityX = 0f;
        }
        else //En caso contrario comenzamos con el avance
        {
            //Cambiamos la velocidad
            rb.linearVelocityX =velo;
        }
    }
    public void MoveRat()
    {
        // Descripcion de movimiento: la rata al percibir que el prota esta cerca lo empieza ha seguir hasta ser eliminado o atraparlo
        // Aclaracion: la eliminacion del prota esta en otro script
        //Primero detectamos si el prota esta cerca
        DetectorProta();
        //Variable auxiliar que contiene el movimiento del prota
        Vector2 movimiento= Vector2.zero;
        // Ahora validamos las acciones dependiendo si wl movimiento esta autorizado o no
        if (avanza)
        {
            //Nota: para encontrar la trayectoria que nos lleve al prota necesitamos determinar la recta que es el desplazamiento
            // a ella. Como tenemos la posicion del prota y la rata, entonces podemos determinar la recta
            //Calculamos el vector direccion donde ira la rata
            Vector2 direccion = (Player.position - transform.position).normalized;
            //Ahora hacemos el vector que representa el movimiento de la cucaracha
            movimiento = new Vector2(direccion.x, 0);
        }
        else //En caso contrario no hay movimiento
        {
            //Vector que tiene el movimiento del prota
            movimiento = Vector2.zero;
        }
        //Ahora movemos al prota
        rb.MovePosition(rb.position+movimiento*Mathf.Abs(velo)*Time.deltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        //Color de la circunferencia
        Gizmos.color = Color.red;
        //Dibujamos el radio
        Gizmos.DrawWireSphere(posicionInicial,distancia);
        // Color de la segunda circunferencia
        Gizmos.color= Color.green;
        //Dibujamos el segundo circulo
        Gizmos.DrawWireSphere(posicionInicial, vecindad);
    }
}
