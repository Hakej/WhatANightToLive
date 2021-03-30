using Handlers;
using UI;
using UnityEngine;

public class AudioDecoyHandler : Singleton<AudioDecoyHandler>
{
    public AudioDecoy CurrentDecoy;

    private void Start()
    {
        EventHandler.Instance.OnDestroyedAudioDecoy += OnDestroyedAudioDecoy;
    }

    private void OnDestroyedAudioDecoy(AudioDecoy destroyedDecoy)
    {
        if (CurrentDecoy == destroyedDecoy)
        {
            CurrentDecoy = null;
        }
    }

    public void ChangeAudioDecoy(AudioDecoy newAudioDecoy)
    {
        if (newAudioDecoy.IsDestroyed)
        {
            return;
        }

        CurrentDecoy?.ToggleDecoy(false);
        newAudioDecoy.ToggleDecoy(true);

        CurrentDecoy = newAudioDecoy;
    }

    public void ChangeAudioDecoy(AudioDecoyButton newAudioDecoyButton)
    {
        var oldAudioDecoy = CurrentDecoy;
        var newAudioDecoy = newAudioDecoyButton.AudioDecoy;

        if (newAudioDecoy.IsDestroyed)
        {
            return;
        }

        CurrentDecoy?.ToggleDecoy(false);

        if (CurrentDecoy == newAudioDecoy)
        {
            CurrentDecoy = null;
        }
        else
        {
            CurrentDecoy = newAudioDecoy;
            CurrentDecoy.ToggleDecoy(true);
        }

        EventHandler.Instance.ChangedAudioDecoy(oldAudioDecoy, CurrentDecoy);
    }
}
