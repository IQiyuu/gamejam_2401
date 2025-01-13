using Unity.VisualScripting;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public GameObject menuCanvas;

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






