// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuChanges : MonoBehaviour
{
    // Image to adjust.
    [SerializeField] private Image imageToChange;

    /// <summary> method <c>SwitchAlpha</c> changes the original alpha of image to given value. </summary>
    /// <param name="newAlpha">Alpha to adjust to.</param>
    public void SwitchAlpha(float newAlpha)
    {
        // Updates alpha.
        Color originalColour = imageToChange.color;
        originalColour.a = newAlpha;
        imageToChange.color = originalColour;
    }
}
