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

    // Update is called once per frame
    void Update()
    {
        Movimiento();
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
    private void Movimiento()
    {
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
}
