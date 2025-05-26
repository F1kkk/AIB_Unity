using UnityEngine;

public class BasicSwitch : MonoBehaviour, ISwitch
{
    private bool isOn = false;
    private GameManager gameManager;
    private int switchIndex;

    public void Initialize(GameManager manager, int index)
    {
        gameManager = manager;
        switchIndex = index;
    }

    public void Toggle()
    {
        isOn = !isOn;
        gameManager.ToggleSwitch(switchIndex);
    }

    public bool IsOn()
    {
        return isOn;
    }
}