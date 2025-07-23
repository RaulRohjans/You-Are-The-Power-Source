using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject introPanel;
    public GameObject failPanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (introPanel != null)
            introPanel.SetActive(true);
    }

    public void HideIntro()
    {
        if (introPanel != null)
            introPanel.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
