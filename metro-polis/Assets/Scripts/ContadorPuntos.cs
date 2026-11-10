using TMPro;
using UnityEngine;

public class ContadorPuntos : MonoBehaviour
{
    // Variable que contiene la puntuacion del jugador
    public float puntos=0f;
    // Objeto que nos da puntos
    public TextMeshProUGUI puntuacion;

    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la moneda esta en contacto con el prota, entonces sumamos los puntos
        if (collision.CompareTag("Player"))
        {
            // Sumamos cinco puntos
            puntos += 5;
            // Cambiamos el texto que contiene la puntuacion
            puntuacion.text = puntos.ToString("F0");
            // Eliminamos el objeto
            Destroy(gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
