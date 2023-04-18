using UnityEngine;

public class LevelNavigate : MonoBehaviour


{
    [SerializeField]
    private Canvas menu;
    [SerializeField]
    private Canvas OtherCanvas;
    // Start is called before the first frame update
    public void showLevels()
    {
        menu.GetComponent<Canvas>().gameObject.SetActive(false);
        OtherCanvas.GetComponent<Canvas>().gameObject.SetActive(true);
    }
    public void Back()
    {
        menu.GetComponent<Canvas>().gameObject.SetActive(true);
        OtherCanvas.GetComponent<Canvas>().gameObject.SetActive(false);
    }
}
