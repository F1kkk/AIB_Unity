public abstract class SwitchDecorator : ISwitch
{
    protected ISwitch decoratedSwitch;

    public SwitchDecorator(ISwitch switchToDecorate)
    {
        decoratedSwitch = switchToDecorate;
    }

    public virtual void Toggle()
    {
        decoratedSwitch.Toggle();
    }

    public virtual bool IsOn()
    {
        return decoratedSwitch.IsOn();
    }
}