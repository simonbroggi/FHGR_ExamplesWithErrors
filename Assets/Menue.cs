using UnityEngine;

public class Menue : MonoBehaviour
{
    public Canvas canvas;
    public void ShowIt()
    {
        canvas.gameObject.SetActive(true);
    }
}
