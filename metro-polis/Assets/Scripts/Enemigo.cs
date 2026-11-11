using TMPro;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    //Variable que contiene la puntuacion del personaje
    private float puntos;
    // Varible que contiene el puntuador del personaje
    public TextMeshProUGUI puntuacion;
    // Varible que contiene el script que nos da la perdida
    public Control_Botones control;

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si la seccion que detecta cuando el personaje es aplastado por el prota, entonces se destruye el enemigo 
        if (collision.gameObject.CompareTag("Player"))
        {
            // Forzamos el valor actual de los puntos a int
            puntos =int.Parse(puntuacion.text);
            // Aumentamos la puntuacion
            puntos += 10;
            //Lo mostramos en la puntuacion
            puntuacion.text= puntos.ToString();
            // Destruimos el enemigo
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el enemigo toca al personaje, entonces destruimos al personaje
        if (collision.gameObject.CompareTag("Player"))
        {
            //Primero destruimos al prota
            Destroy(collision.gameObject);
            // Luego activamos el menu de perdiste
            control.Perdiste();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
