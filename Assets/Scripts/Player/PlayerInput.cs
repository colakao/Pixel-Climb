namespace Player
{
    using UnityEngine;

    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput Instance { get; private set; }

        private Camera _mainCamera;
        private PlayerController _player;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            _mainCamera = Camera.main;
            _player = PlayerController.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var _touchPosition = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var jumpDirection = _touchPosition - (Vector2)_player.transform.position;

                _player.UpdateJump(jumpDirection);
            }

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    var position = _mainCamera.ScreenToWorldPoint(touch.position);
                    var jumpDirection = position - _player.transform.position;

                    _player.UpdateJump(jumpDirection);
                }
            }            
        }
    }
}