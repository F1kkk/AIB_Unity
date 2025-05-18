using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LightBulbGame : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bulbSprite;
    [SerializeField] private SpriteRenderer doorSprite;
    [SerializeField] private Button[] switchButtons;
    [SerializeField] private Button doorButton;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameStatusText;

    private bool isDoorClosed = true;
    private int correctSwitchIndex;
    private bool isGameActive = false;
    private float timeRemaining = 30f;

    private void Start()
    {
        InitializeGame();
        doorButton.onClick.AddListener(ToggleDoor);
        for (int i = 0; i < switchButtons.Length; i++)
        {
            int index = i;
            switchButtons[i].onClick.AddListener(() => OnSwitchClicked(index));
        }
    }

    private void InitializeGame()
    {
        correctSwitchIndex = Random.Range(0, switchButtons.Length);
        isGameActive = true;
        timeRemaining = 30f;
        isDoorClosed = true;
        bulbSprite.color = Color.gray;
        doorSprite.color = Color.green; // Pintu tertutup
        gameStatusText.text = "";
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (!isGameActive) return;

        timeRemaining -= Time.deltaTime;
        UpdateTimerDisplay();

        if (timeRemaining <= 0)
        {
            EndGame(false);
        }
    }

    private void ToggleDoor()
    {
        isDoorClosed = !isDoorClosed;
        doorSprite.color = isDoorClosed ? Color.green : Color.red;
        CheckBulbState();
    }

    private void OnSwitchClicked(int switchIndex)
    {
        if (!isGameActive) return;

        if (isDoorClosed)
        {
            if (switchIndex == correctSwitchIndex)
            {
                bulbSprite.color = Color.yellow;
                EndGame(true);
            }
            else
            {
                EndGame(false);
            }
        }
        else
        {
            gameStatusText.text = "Tutup pintu terlebih dahulu!";
        }
    }

    private void CheckBulbState()
    {
        if (!isDoorClosed)
        {
            bulbSprite.color = Color.gray;
        }
    }

    private void UpdateTimerDisplay()
    {
        timerText.text = $"Waktu: {Mathf.CeilToInt(timeRemaining)}";
    }

    private void EndGame(bool won)
    {
        isGameActive = false;
        bulbSprite.color = won ? Color.yellow : Color.gray;
        gameStatusText.text = won ? "Menang! Lampu Menyala!" : "Kalah! Coba Lagi.";
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        InitializeGame();
    }
}