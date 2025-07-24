using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject introPanel;
    public GameObject failPanel;
    
    public TextMeshProUGUI finalTimeText;
    
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
        SceneManager.LoadScene("MainGame");
    }

    public void ShowFinalTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        finalTimeText.text = $"You survived {minutes:00} mins and {seconds:00} secs";
    }
}
