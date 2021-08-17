using UnityEngine;
using Core;
using UnityEngine.UI;

public class WatchAdButtonActive : MonoBehaviour
{
    public void OnEnable()
    {
        if (!GameManager.Instance.revivedWithAd) return;
        Debug.Log("Revived with Ad called!");
        GetComponent<Button>().interactable = false;
    }
}
