// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSFX : MonoBehaviour
{
    // On/Off Sound sprites.
    [SerializeField] private Sprite originalSprite, altSprite;

    // Relevant audio listener. 
    [SerializeField] private AudioSource audioSource;
    
    /// <summary> method <c>SwitchOnClick</c> switches the current sprite to the other on button click. </summary>
    public void SwitchOnClick()
    {
        // Get image comp to access sprite.
        Image imageComp = GetComponent<Image>();

        // Sets audio volume change & sprite to opposite.
        float audioVolume;
        if (imageComp.sprite == originalSprite) { audioVolume = 0.0f; imageComp.sprite = altSprite; }
        else { imageComp.sprite = originalSprite; audioVolume = 1.0f; }

        // Changes audio state.
        AdjustAudio(audioVolume);
    }
    
    /// <summary> method <c>AdjustAudio</c> changes the audio value depending on SFX state. </summary> 
    /// <param name="value">The audio volume to set.</param>
    public void AdjustAudio(float value)
    {
        // Adjusts audioSource volume.
        audioSource.volume = value;
    }
}