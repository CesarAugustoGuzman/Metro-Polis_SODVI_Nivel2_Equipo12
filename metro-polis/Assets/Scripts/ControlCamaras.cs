using UnityEngine;

public class ControlCamaras : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    private void activarCamara1()
    {
        if (cam1 != null&& cam2 != null)
        {
            cam2.gameObject.SetActive(false);
            cam1.gameObject.SetActive(true);
        }
    }
    private void activarCamara2() 
    {
        if (cam1 != null && cam2 != null)
        {
            cam1.gameObject.SetActive(false);
            cam2.gameObject.SetActive(true);
        }
    }
}
