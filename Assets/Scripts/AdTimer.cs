using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Core.Menu.MenuTypes;

public class AdTimer : MonoBehaviour
{
    private float _currentTime;
    private Image _img;

    public float timeToCloseAd;

    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _currentTime = timeToCloseAd;
    }

    private void Update()
    {
        if (_currentTime > 0)
        {
            _img.fillAmount = _currentTime / timeToCloseAd;
            _currentTime -= Time.unscaledDeltaTime;
        }
        else 
        {
            CloseWindow();
        }
    }

    void CloseWindow()
    {
        Debug.Log("Closed");
        PromptAdMenu.Instance.OnBackPressed();
    }
}
