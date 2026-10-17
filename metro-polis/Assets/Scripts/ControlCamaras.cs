using UnityEngine;

public class ControlCamaras : MonoBehaviour
{
    public Transform Player;
    public float veloCam=0.025f;
    public Vector3 desplazamiento;
    public Transform puntoReferencia;
    private void LateUpdate()
    {
        if (puntoReferencia.position.x < Player.position.x)
        {
            Vector3 posicionDeseada = Player.position + desplazamiento;
            Vector3 suavizarPosicion = Vector3.Lerp(transform.position, posicionDeseada, veloCam);
            transform.position = suavizarPosicion;
        }

    }
}
