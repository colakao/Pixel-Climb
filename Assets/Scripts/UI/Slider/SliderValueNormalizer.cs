using UnityEngine;

public static class SliderValueNormalizer 
{
    public static float Normalize(float value)
    {
        float newValue;

        if (value != 0)
        {
            newValue = value / 12;
        }
        else
        {
            newValue = 0.0001f;
        }

        return Mathf.Log10(newValue) * 20;
    }
}
