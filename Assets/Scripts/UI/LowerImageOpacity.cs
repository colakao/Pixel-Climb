using UnityEngine;
using UnityEngine.UI;

public class LowerImageOpacity : MonoBehaviour
{
    public Image img;

    public void OnButtonPressed()
    {
        var tempColor = img.color;
        tempColor.a = .05f;
        img.color = tempColor;
    }
}
