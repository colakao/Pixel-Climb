using UnityEngine;
using Player;
using Core;

public class MoveUp : MonoBehaviour
{
    //public float speed;
    public float desiredMinDistance;

    [HideInInspector] public Vector3 startingPosition { get; private set; }

    private BoxCollider2D _col;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _col = GetComponent<BoxCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();

        var colNewSize = _sprite.size;
        colNewSize.y = _sprite.size.y + _col.offset.y * 2;
        _col.size = colNewSize;
        startingPosition = transform.position;
    }

    private void Update()
    {
        CorrectPositionY();
        MoveSawBlades();
    }

    void MoveSawBlades()
    {
        transform.Translate(Vector2.up * Difficulty.Instance.GetDifficulty() * Time.deltaTime);
    }

    void CorrectPositionY()
    {
        var distance = PlayerController.Instance.transform.position.y - (transform.position.y + _col.bounds.extents.y);
        if (distance > desiredMinDistance)
        {
            var offsetDistance = distance - desiredMinDistance;
            transform.position = transform.position + Vector3.up * offsetDistance;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.GameOver();
    }
}
