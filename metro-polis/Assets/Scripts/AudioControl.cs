using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    // Variable que contiene el audio mixer a usar
    public AudioMixer audioMixer;
    // Variable que contiene el slider que controla la musica
    public Slider musica;
    //Variable que contiene el slider de los efectos
    public Slider efectos;
    void Start()
    {
        //Primero obtenemos los volumenes ya guardados, sino hay entonces ´ponemos 0.75 como defaut
        //Volumen de la musica
        float volMusic = PlayerPrefs.GetFloat("Musica", 0.75f);
        //Volumen de los efectos
        float volEffect = PlayerPrefs.GetFloat("Efectos", 0.75f);
        // Modificamos e implementamos en el mixer el volumen
        SetMixerVolume(volMusic,"Musica");
        SetMixerVolume(volEffect,"Efectos");
        //Guardamos las preferencias
        PlayerPrefs.Save();
        //Ponemos el volumen establecido en musica
        if (musica != null)
        {
            musica.value = volMusic;
        }
        //Implementamos en el slider los efectos
        if (efectos != null)
        {
            efectos.value = volEffect;
        }
    }
    public void SetMusicVolume(float volume)
    {
        SetMixerVolume(volume, "Musica");
        PlayerPrefs.Save();
    }
    public void SetEffectsVolume(float volume)
    {
        SetMixerVolume(volume, "Efectos");
        PlayerPrefs.Save();
    }
    private void SetMixerVolume(float volume, string categoria)
    {
        // Protegemos el código: si el volumen es muy cercano a 0, le asignamos -80dB (silencio absoluto)
        PlayerPrefs.SetFloat(categoria, volume);
        if (volume <= 0.001f)
        {
            audioMixer.SetFloat(categoria, -80f);
        }
        else
        {
            // Usamos la fórmula logarítmica de manera segura evitando el 0 absoluto
            float fixedVolume = Mathf.Log10(volume) * 20.0f;
            audioMixer.SetFloat(categoria, fixedVolume);
            //PlayerPrefs.SetFloat(categoria, fixedVolume);
        }
        
    }
    public void CondicionesIn() 
    {
        //Primero obtenemos los volumenes ya guardados, sino hay entonces ´ponemos 0.75 como defaut
        //Volumen de la musica
        float volMusic = PlayerPrefs.GetFloat("Musica", 0.75f);
        //Volumen de los efectos
        float volEffect = PlayerPrefs.GetFloat("Efectos", 0.75f);
        // Modificamos e implementamos en el mixer el volumen
        SetMixerVolume(volMusic, "Musica");
        SetMixerVolume(volEffect, "Efectos");
        //Guardamos las preferencias
        PlayerPrefs.Save();
    }
}
