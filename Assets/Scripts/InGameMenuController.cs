using Unity.VisualScripting;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    public GameObject mainCanvas;

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            mainCanvas.SetActive(!mainCanvas.activeSelf);
            if (mainCanvas.activeSelf)
                Time.timeScale = 1f;
            else
                Time.timeScale = 0f;
        }
    }

    public void OnResumeButtonClick()
    {
        menuCanvas.SetActive(false);
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("MenuScene");
    }
}






