using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionOpener : MonoBehaviour
{
    public GameObject canvas;
    private bool menuOpen=false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuOpen)
        {
            OpenOptionMenu();
        }else if (Input.GetKeyDown(KeyCode.Escape) && menuOpen)
        {
            CloseOptionMenu();
        }
    }
    public void OpenOptionMenu()
    {
        Time.timeScale = 0f;
        canvas.SetActive(true);
        GameObject.FindObjectOfType<ScriptPlayer>().isPaused = true;
        menuOpen = true;
    }

    public void CloseOptionMenu()
    {
        Time.timeScale = 1;
        canvas.SetActive(false);
        GameObject.FindObjectOfType<ScriptPlayer>().isPaused = false;
        menuOpen = false;
    }
}
