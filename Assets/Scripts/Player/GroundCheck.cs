namespace Player 
{
    using UnityEngine;

    public class GroundCheck : MonoBehaviour
    {
        private PlayerController _player;
        public Collider2D col;

        public LayerMask layerMask;
        public float maxDistance;

        private Vector3 boxCastSize;
        private Vector3 boxCastOrigin;

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
        }
        public bool IsGrounded()
        {
            boxCastSize = new Vector3(col.bounds.size.x, transform.lossyScale.y * .1f, transform.lossyScale.z);
            boxCastOrigin = new Vector3(col.bounds.center.x, col.bounds.center.y - col.bounds.extents.y, col.bounds.center.z);
            RaycastHit2D hit2D = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0, Vector2.down, maxDistance, layerMask);
            bool isHit = hit2D;

            return isHit;
        }

        private void OnDrawGizmos()
        {
            boxCastSize = new Vector3(col.bounds.size.x, transform.lossyScale.y * .1f, transform.lossyScale.z);
            boxCastOrigin = new Vector3(col.bounds.center.x, col.bounds.center.y - col.bounds.extents.y, col.bounds.center.z);
            RaycastHit2D hit2D = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0, Vector2.down, maxDistance, layerMask);
            bool isHit = hit2D;

            if (isHit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube((Vector2)boxCastOrigin + Vector2.down * hit2D.distance, boxCastSize);
            }
        }
    }
}