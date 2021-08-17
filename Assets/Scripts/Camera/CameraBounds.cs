using UnityEngine;
using Player;

public class CameraBounds : MonoBehaviour
{
    public SpriteRenderer floor;
    public SpriteRenderer background;
    public SpriteRenderer sawBlades;

    private PlayerController _player;

    void Start()
    {
        if (PlayerController.Instance != null)
            _player = PlayerController.Instance;
        else
            Debug.LogWarning("Could not load an Instance of PlayerController");

        AddCollider();
        CameraPixelToWorldSpace();
    }

    void CameraPixelToWorldSpace()
    {
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;

        floor.size = new Vector2(halfWidth * 2, floor.size.y);
        background.size = new Vector2(halfWidth * 2, background.size.y);
        sawBlades.size = new Vector2(halfWidth * 2, sawBlades.size.y);
    }

    void AddCollider()
    {
        if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

        var cam = Camera.main;
        if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

        var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

        // add or use existing EdgeCollider2D
        var edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

        var edgePoints = new[] { bottomLeft, topLeft, topRight, bottomRight};
        edge.points = edgePoints;

        var offset = _player.transform.position - Camera.main.transform.position;

        edge.offset = offset;
    }
}
