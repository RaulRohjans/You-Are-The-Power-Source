using UnityEngine;
using UnityEngine.UI;

public class Device : MonoBehaviour
{
    public float maxEnergy = 100f;
    public float depletionRate = 5f;
    public float chargeRate = 5f;

    public Image chargeImg;

    [HideInInspector] public float currentEnergy;
    [HideInInspector] public bool isPowered = false;

    public GameObject particleObj;
   
    private void Start()
    {
        currentEnergy = maxEnergy;
    }

    private void Update()
    {
        if (!GameManager.Instance.gameStarted) return;

        if (!isPowered)
        {
            currentEnergy -= depletionRate * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
            chargeImg.fillAmount = currentEnergy / maxEnergy;

            if (currentEnergy <= 0)
                GameManager.Instance.GameOver();
        }
        else
        {
            currentEnergy += chargeRate * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
            chargeImg.fillAmount = currentEnergy / maxEnergy;
        }
    }

    public void PowerOn()
    {
        isPowered = true;
    }

    public void PowerOff()
    {
        isPowered = false;
    }
}
