using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Layout;
    [SerializeField] private GameObject[] windows;
    // Start is called before the first frame update
    void Awake()
    {
        Layout = this;
        foreach(GameObject win in windows)
        {
            win.SetActive(false);
        }
    }

    void Start()
    {
        OpenLayout("Loading");
    }

    public void OpenLayout(string name)
    {
        foreach (GameObject win in windows)
        {
            if (win.name == name) win.SetActive(true);
            else win.SetActive(false);
        }
    }
}
