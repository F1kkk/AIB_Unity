using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer bulb; // Gambar bohlam
    public Sprite bulbOnSprite; // Gambar bohlam menyala
    public Sprite bulbOffSprite; // Gambar bohlam mati
    public Button[] switchButtons; // Tombol saklar
    public AudioClip toggleSound; // Suara klik
    public TextMeshProUGUI timerText; // Teks timer
    public TextMeshProUGUI resultText; // Teks hasil

    private ISwitch[] switches;
    private int correctSwitch;
    private bool[] switchStates;
    private bool[] bulbWasOn;
    private float timer = 60f;
    private bool gameOver = false;

    void Start()
    {

        switchStates = new bool[3];
        bulbWasOn = new bool[3];
        correctSwitch = Random.Range(0, 3); // Pilih saklar benar secara acak
        bulb.sprite = bulbOffSprite; // Bohlam mulai mati

        switches = new ISwitch[3];
        for (int i = 0; i < switchButtons.Length; i++)
        {
            int index = i;
            BasicSwitch basicSwitch = switchButtons[i].gameObject.AddComponent<BasicSwitch>();
            basicSwitch.Initialize(this, index);

            AudioSource audioSource = switchButtons[i].gameObject.AddComponent<AudioSource>();
            ISwitch decoratedSwitch = new SoundSwitchDecorator(basicSwitch, audioSource, toggleSound);

            decoratedSwitch = new VisualSwitchDecorator(decoratedSwitch, switchButtons[i]);

            switches[i] = decoratedSwitch;

            switchButtons[i].onClick.AddListener(() => switches[index].Toggle());
        }
    }

    void Update()
    {
        if (!gameOver)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
            if (timer <= 0)
            {
                GameOver(false);
            }
        }
    }

    public void ToggleSwitch(int switchIndex)
    {
        if (gameOver) return;

        switchStates[switchIndex] = !switchStates[switchIndex];

        if (switchIndex == correctSwitch)
        {
            bulb.sprite = switchStates[switchIndex] ? bulbOnSprite : bulbOffSprite;
            if (switchStates[switchIndex])
                bulbWasOn[switchIndex] = true;
        }
        else
        {
            bulb.sprite = bulbOffSprite;
        }
    }

    public void CheckBulb()
    {
        if (gameOver) return;

        if (bulb.sprite == bulbOnSprite)
        {
            for (int i = 0; i < switchStates.Length; i++)
            {
                if (switchStates[i] && i == correctSwitch)
                {
                    GameOver(true);
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < bulbWasOn.Length; i++)
            {
                if (bulbWasOn[i] && i == correctSwitch)
                {
                    GameOver(true);
                    return;
                }
            }
        }

        GameOver(false);
    }

    void GameOver(bool win)
    {
        gameOver = true;
        resultText.text = win ? "You Win!" : "Game Over!";
    }
}