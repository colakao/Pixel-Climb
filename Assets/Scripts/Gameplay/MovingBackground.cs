using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    [SerializeField] Vector2 parallaxEffectMultiplier;
    Transform cameraTransform;
    Vector3 lastCameraPosition;
    float textureUnitSizeY;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        transform.position -= new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;

        if (cameraTransform.position.y - transform.position.y >= textureUnitSizeY)
        {
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y);
        }
        else if (transform.position.y - cameraTransform.position.y >= textureUnitSizeY)
        {
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y);
        }
    }
}
