using UnityEngine;

public class WorldBorder : MonoBehaviour
{
    [HideInInspector] public float minX;
    [HideInInspector] public float maxX;
    public static WorldBorder Instance { get; private set; }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        minX = Camera.main.ViewportToWorldPoint(Vector2.zero).x;
        maxX = Camera.main.ViewportToWorldPoint(Vector2.one).x;
    }
}
