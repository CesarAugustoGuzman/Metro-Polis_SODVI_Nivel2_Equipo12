using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control_Botones : MonoBehaviour
{
    // Menu de pausa
    public GameObject MenuP;
    // Boton de pausa
    public GameObject BotonP;
    // Boton de reanudar
    public GameObject BotonR;
    //Variable que contiene si se a completado el nivel 2
    private float disponible2;
    // Variable que contiene si el nivel 3 esta completo
    private float disponible3;
    // Varible que contiene la escena actual en la cual estamos trabajando
    private string sceneActual;
    // Variable que contiene el texto que identifica si estas en pausa o perdiste
    public TextMeshProUGUI idPausa;

    private void Start()
    {
        // Obtenemos la escena actual
        Scene scene = SceneManager.GetActiveScene();
        // Ponemos como escena actual 
        sceneActual= scene.name;
        // Si el juego ya ha sido jugado, entonces tomamos el estado del nivel
        if (PlayerPrefs.HasKey("Estado_N2"))
        {
            // Obtenemos de la memoria el estado del nivel
            disponible2 = PlayerPrefs.GetFloat("Estado_N2");
        } else
        {
            // Si no esta registrado el estado, entonces tomamos el nivel como no completado
            disponible2 = 0;
        }
        // Vemos di el juego tiene registrado el estado del nivel 3
        if (PlayerPrefs.HasKey("Estado_N3"))
        {
            // Obtenemos el estado del nivel 3
            disponible3 = PlayerPrefs.GetFloat("Estado_N3");
        }
        else
        {
            // Si no hay registro, entonces tomamos como nivel aun no disponible
            disponible3 = 0;
        }

    }
    public void Jugar()
    {
        // Cuando se presiona el boton nos dirigmos al menu de los niveles
        SceneManager.LoadScene("Niveles");
    }
    public void Regresar()
    {
        //Al regresar el tiempo lo ponemos de manera estandar
        Time.timeScale = 1;
        // Y volvemos al menu principal de los niveles
        SceneManager.LoadScene("Niveles");
    }
    public void RegresarPrincipal()
    {
        // Al presionar el boton regresamos al menu del incio
        SceneManager.LoadScene("Menu_principal");
    }
    public void Pausa()
    {
        // Al pausar el tiempo lo ponemos en 0
        Time.timeScale = 0;
        // Desactivamos el boton de pausa
        BotonP.SetActive(false);
        //Activamos el menu de pausa
        MenuP.SetActive(true);
    }
    public void Reanudar()
    {
        // Activamos el tiempo de manera estandar
        Time.timeScale = 1;
        // Desactivamos el emnu de pausa
        MenuP.SetActive(false);
        // Reactivamos el boton de pausa
        BotonP.SetActive(true);
    }
    public void Reiniciar()
    {
        // Inicializamos el tiempo de manera normal
        Time.timeScale = 1;
        // Volemos a empezar la escena en la que estamos
        SceneManager.LoadScene(sceneActual);
    }
    public void Perdiste()
    {
        // Iniciamlizamos el menu de pausa
        Pausa();
        //Desactivamos el boton de reanudar
        BotonR.SetActive(false);
        // Cambiamos el texto del identificador
        idPausa.text = "Perdiste";

    }
    public void Nivel1()
    {
        // Ingresamos al nivel 1
        SceneManager.LoadScene("SampleScene");
    }
    public void Nivel2()
    {
        //Primero vemos si el nivel 2 esta disponible
        if (disponible2==1)
        {
            // Si disponible tiene el valor de 1, entonces jugamos el nivel 2
            SceneManager.LoadScene("Nivel2");
        }
    }
    public void Nivel3()
    {
        //Primero vemos si el nivel 3 esta disponible
        if (disponible3 == 1)
        {
            // Si disponible tiene el valor de 1, entonces jugamos el nivel 3
            SceneManager.LoadScene("Nivel3");
        }
    }
}
