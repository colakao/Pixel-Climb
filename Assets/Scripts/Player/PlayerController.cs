using UnityEngine;
using UnityEngine.EventSystems;
using Core;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [Header("Physics")]
        public float gravity = 9.81f;
        public float jumpForce;

        [Header("Raycast Variables")]
        public Vector2 rayOriginOffset;
        public LayerMask layerMask;
        public float rayDistance;

        [Header("Game Over")]
        public float speedToDie;

        [HideInInspector] public Rigidbody2D _rb;
        private GroundCheck _check;
        private SpriteRenderer _sprite;
        private bool secondJumpActive;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            _rb = GetComponent<Rigidbody2D>();
            _check = GetComponent<GroundCheck>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            Gravity();
            DieCondition();
            SpriteDirection();
        }

        void SpriteDirection()
        {
            if (_rb.velocity.x > 0)
                _sprite.flipX = true;
            else if (_rb.velocity.x < 0)
                _sprite.flipX = false;
        }

        void DieCondition()
        {
            if (_rb.velocity.y <= -speedToDie)
                GameManager.Instance.GameOver();
        }
        void Gravity()
        {
            if (!_check.IsGrounded())
                _rb.velocity += Vector2.down * gravity * Time.deltaTime;

            if (_check.IsGrounded() && secondJumpActive == false)
                secondJumpActive = true;
        }

        public void UpdateJump(Vector2 direction)
        {
            //if (transform.parent != null) transform.SetParent(null);
            //_rb.velocity = Vector2.zero;
            if (_check.IsGrounded() && !EventSystem.current.IsPointerOverGameObject(0) && Time.timeScale != 0)
            {
                _rb.velocity = direction.normalized * jumpForce;
            }
            else if (secondJumpActive && !EventSystem.current.IsPointerOverGameObject(0) && Time.timeScale != 0 && _rb.velocity.y > -gravity)
            {
                secondJumpActive = false;
                _rb.velocity = Vector2.zero;
                _rb.velocity = Vector2.down * gravity;
            }
        }

        public void Respawn(Vector3 pos)
        {
            _rb.velocity = Vector2.zero;
            transform.position = pos;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Platform>())
                transform.SetParent(collision.transform);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Platform>())
                transform.parent = null;
        }
    }
}