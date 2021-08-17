using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core.Audio;
using UnityEngine;

[AddComponentMenu("UI/Noisy Button", 30)]
public class CustomButton : Button
{
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        AudioManager.Instance.PlayAudio(AudioTypeName.SFX_02);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        AudioManager.Instance.PlayAudio(AudioTypeName.SFX_02);
    }
}