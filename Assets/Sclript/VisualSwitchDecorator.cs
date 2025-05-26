using UnityEngine;
using UnityEngine.UI;

public class VisualSwitchDecorator : SwitchDecorator
{
    private Button button;
    private Color onColor = Color.green;
    private Color offColor = Color.red;

    public VisualSwitchDecorator(ISwitch switchToDecorate, Button button) : base(switchToDecorate)
    {
        this.button = button;
        UpdateVisual();
    }

    public override void Toggle()
    {
        base.Toggle();
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        button.image.color = IsOn() ? onColor : offColor;
    }
}  