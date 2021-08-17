using UnityEngine;

public class PlatformSpriteSwap : MonoBehaviour
{
    public Sprite spriteToSwap;

    public void Swap()
    {
        GetComponent<SpriteRenderer>().sprite = spriteToSwap;
    }
}
