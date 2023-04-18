using UnityEngine;

public class InGameMenu : MonoBehaviour
{

    [SerializeField]
    private Canvas myCanvas;
    public void Resume() 
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        myCanvas.GetComponent<Canvas>().gameObject.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        myCanvas.GetComponent<Canvas>().gameObject.SetActive(true);
    }
}