using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Slider healthBar;
    public TextMeshProUGUI killCounterText;
    public GameObject jumpScare;

    private int enemyKilled = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealth(float current, float max)
    {
        float percent = Mathf.Clamp01(current / max);
        healthBar.value = percent;
    }

    public void AddKill()
    {
        enemyKilled++;
        killCounterText.text = $"Enemies: {enemyKilled}";
    }

    public void ResetKills()
    {
        enemyKilled = 0;
        killCounterText.text = "Enemies: 0";
    }

    public void OpenJumpScare()
    {
        if (jumpScare != null)
            jumpScare.SetActive(true);
    }
}
