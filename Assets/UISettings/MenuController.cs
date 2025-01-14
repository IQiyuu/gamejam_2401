using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnRagequitButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
