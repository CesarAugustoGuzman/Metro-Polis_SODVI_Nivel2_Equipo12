using UnityEngine;

public class ControlCamaras : MonoBehaviour
{
    // Variable que contiene el transform de el jugador
    public Transform Player;
    // Variable que contiene la velocidad con la cual se movera la camara
    public float veloCam=0.025f;
    // Vector que contiene la posicion fija de la camara
    public Vector3 desplazamiento;
    // Punto dede el que se empieza a mover la camara
    public Transform puntoReferencia;
    // Posiion original de y
    private float yInicial;
    private void Start()
    {
        yInicial=transform.position.y;
    }
    private void LateUpdate()
    {
        // Si el jugador supera el punto de referencia, entonces se empieza a mover la camara
        if (puntoReferencia.position.x < Player.position.x)
        {
            // Posicion la cual se quiere que se mueva la camara con el personaje
            Vector3 posicionDeseada = Player.position + desplazamiento;
            //Vector que contiene la posicion ya suavizada
            Vector3 suavizarPosicion = Vector3.Lerp(transform.position, posicionDeseada, veloCam);
            // Fijamos la coordenada en y
            suavizarPosicion.y = yInicial;
            //Cambiamos la posicion
            transform.position = suavizarPosicion;
        }

    }
}
