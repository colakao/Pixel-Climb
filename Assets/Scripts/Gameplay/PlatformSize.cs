using UnityEngine;

public class PlatformSize : MonoBehaviour
{
    //Max value needs to be sprite's pixel per unit value
    [Range(3,16)]
    public int screenCoverPercent;

    [Range(1,3)]
    public int pixelBorder;

    BoxCollider2D _col;
    SpriteRenderer _sprite;

    private void Awake()
    {
        SetPlatformSize();
    }

    void SetPlatformSize()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _col = GetComponent<BoxCollider2D>();

        float camHeight = Camera.main.orthographicSize * 2;
        float camWidth = Camera.main.aspect * camHeight;

        _sprite.size = new Vector2(Mathf.FloorToInt(camWidth) * screenCoverPercent/_sprite.sprite.pixelsPerUnit, _sprite.size.y);
        _col.size = new Vector2(
            _sprite.size.x * ((_sprite.sprite.pixelsPerUnit - pixelBorder) /_sprite.sprite.pixelsPerUnit),
            _sprite.size.y
            );
    }
}
