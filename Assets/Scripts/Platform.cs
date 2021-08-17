using UnityEngine;
using Core;

public class Platform : MonoBehaviour
{
    [Header("Pendiente"), Tooltip("0.02 es un buen valor.")]
    public float h = 0.02f;
    public float minPlatformMove = 5;
    [Range(0, 1f)]
    public float maxProbability;

    private float chance;

    public float platformSpeed;

    private bool moves;
    private float minPlatformX;
    private float maxPlatformX;

    private SpriteRenderer _sprite;

    private Vector3 vel;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        if (Random.value < SpawnChance())
            moves = true;

        minPlatformX = WorldBorder.Instance.minX + _sprite.size.x / 2;
        maxPlatformX = WorldBorder.Instance.maxX - _sprite.size.x / 2;

         vel = new Vector3(platformSpeed, 0, 0);
    }

    float SpawnChance()
    {
        chance = h * GameManager.Instance.score - minPlatformMove * h;
        return Mathf.Clamp(chance, 0, maxProbability);
    }
    private void Update()
    {
        if (!moves) return;
        transform.Translate(GetDirection() * Time.deltaTime);
    }

    Vector3 GetDirection()
    {
        if (transform.position.x <= minPlatformX)
            vel = new Vector3(platformSpeed, 0, 0);
        if (transform.position.x >= maxPlatformX)
            vel = new Vector3(-platformSpeed, 0, 0);

        return vel;
    }
}
