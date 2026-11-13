using UnityEngine;

public class Electrificado : MonoBehaviour
{
    // Variable que contiene el control de los botones
    public Control_Botones control;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el personaje esta en contacto con el objeto electrificado, pierde
        if (collision.gameObject.CompareTag("Player"))
        {
            // Activamos la pantalla de perdida
            control.Perdiste();
        }
    }
}
