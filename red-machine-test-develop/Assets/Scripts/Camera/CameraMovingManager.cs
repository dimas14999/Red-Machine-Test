using System;
using Player;
using Player.ActionHandlers;
using UnityEngine;

namespace Camera
{
    public class CameraMovingManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _gameBoundaries;
        [SerializeField] private float _swipeSpeed;
        [SerializeField] private bool _canDrag = true;
        
        private ClickHandler _clickHandler; 
        private Bounds _cameraBounds;
        private Vector3 _currentPosition;

        private void Start()
        {
            _cameraBounds = _gameBoundaries.bounds;
            
            _clickHandler = ClickHandler.Instance;
            _clickHandler.DragEvent += OnDrag;
        }

        private void OnEnable()
        {
            _clickHandler.DragEvent -= OnDrag;
        }

        private void OnDrag(Vector3 currentPosition)
        {
            if (!CanScrolling() || CameraBoundsSmall() || !_canDrag)
                return;

            CameraHolder.Instance.MainCamera.transform.Translate(new Vector3(currentPosition.x * _swipeSpeed, currentPosition.y * _swipeSpeed, -10));
            ClampCameraToBounds();
        }

        private bool CameraBoundsSmall() =>
            _cameraBounds.size.x < CameraHolder.Instance.MainCamera.orthographicSize * 2f *
            CameraHolder.Instance.MainCamera.aspect ||
            _cameraBounds.size.y < CameraHolder.Instance.MainCamera.orthographicSize * 2f;
        
        private bool CanScrolling() => 
            PlayerController.PlayerState == PlayerState.Scrolling;

        private void ClampCameraToBounds()
        {
            Vector3 cameraPosition = CameraHolder.Instance.MainCamera.transform.position;
            Vector3 minBounds = _cameraBounds.min;
            Vector3 maxBounds = _cameraBounds.max;

            float cameraHalfHeight = CameraHolder.Instance.MainCamera.orthographicSize;
            float cameraHalfWidth = cameraHalfHeight * CameraHolder.Instance.MainCamera.aspect;

            cameraPosition.x = Mathf.Clamp(cameraPosition.x, minBounds.x + cameraHalfWidth, maxBounds.x - cameraHalfWidth);
            cameraPosition.y = Mathf.Clamp(cameraPosition.y, minBounds.y + cameraHalfHeight, maxBounds.y - cameraHalfHeight);

            CameraHolder.Instance.MainCamera.transform.position = new Vector3(cameraPosition.x , cameraPosition.y, -10);
        }
    } 
}
