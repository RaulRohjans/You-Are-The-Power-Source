using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Device currentlyPowered;

    public bool gameStarted = false;

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
            }
            return;
        }

        HandleMouseClick();
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
    }

    public void GameOver()
    {
        Debug.Log("Game Over! A critical device ran out of power.");

        // You could add UI for Game Over here
        UIManager.Instance.failPanel.SetActive(true);
        Time.timeScale = 0;
        gameStarted= false; 
    }
}
