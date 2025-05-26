using UnityEngine;

public class SoundSwitchDecorator : SwitchDecorator
{
    private AudioSource audioSource;
    private AudioClip toggleSound;

    public SoundSwitchDecorator(ISwitch switchToDecorate, AudioSource source, AudioClip sound) : base(switchToDecorate)
    {
        audioSource = source;
        toggleSound = sound;
    }

    public override void Toggle()
    {
        base.Toggle();
        audioSource.PlayOneShot(toggleSound);
    }
}