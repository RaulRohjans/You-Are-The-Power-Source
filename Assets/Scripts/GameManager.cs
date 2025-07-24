using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Device currentlyPowered;

    public bool gameStarted = false;

    // Powerline effect 
    public GameObject powerLinePrefab;
    private GameObject activePowerLine;
    public Transform plugTransform;

    // Audio
    public AudioSource connectionSFX;

    // Timer
    public TextMeshPro timerText;
    private float survivalTime = 0f;
    private bool isGameOver = false;

    private void Awake()
    {
        Time.timeScale = 1f;
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        gameStarted = false;
    }

    private void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameStarted = true;
                UIManager.Instance.HideIntro();
                MusicManager.Instance.PlayMusic();
            }
            return;
        }

        if (!isGameOver)
        {
            survivalTime += Time.deltaTime;
            UpdateTimerUI();
        }

        HandleMouseClick();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(survivalTime / 60f);
        int seconds = Mathf.FloorToInt(survivalTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                Device clickedDevice = hit.collider.GetComponent<Device>();
                if (clickedDevice != null)
                {
                    SwitchPowerTo(clickedDevice);
                }
            }
        }
    }

    void SwitchPowerTo(Device newDevice)
    {
        if (currentlyPowered != null)
            currentlyPowered.PowerOff();

        currentlyPowered = newDevice;
        currentlyPowered.PowerOn();

        if (activePowerLine != null) Destroy(activePowerLine);

        activePowerLine = Instantiate(powerLinePrefab);
        PowerLine pl = activePowerLine.GetComponent<PowerLine>();
        pl.start = plugTransform;
        pl.end = newDevice.transform;

        // Play sound effect
        if (connectionSFX != null) connectionSFX.Play();
    }

    public void GameOver()
    {
        MusicManager.Instance.StopMusic();

        UIManager.Instance.failPanel.SetActive(true);
        UIManager.Instance.ShowFinalTime(survivalTime);

        Time.timeScale = 0;
        gameStarted = false;
    }
}
