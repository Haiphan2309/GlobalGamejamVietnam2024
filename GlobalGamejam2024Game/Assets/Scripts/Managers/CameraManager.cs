using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityUtilities;

public class CameraManager : SingletonMonoBehaviour<CameraManager>
{
    private Camera _camera;

    [Header("Drag")]
    [SerializeField] private bool _canDrag = true;
    [SerializeField] private float _dragSpeed = 500f; // Speed of camera movement
    [SerializeField] private float _smoothTime = 0.125f;
    [SerializeField] private RangeFloat _verticalBoundPosition, _horizontalBoundPosition;
    private Vector3 _currentVelocity;
    private Vector3 _lastMousePosition;
    public bool IsDraggingMouse = false;

    [Header("Zoom")] 
    [SerializeField] private bool _canZoom = true;
    [SerializeField] private float _zoomSpeed = 20f; // Speed of camera zooming
    [SerializeField] private RangeFloat _zoomBound = new RangeFloat(5f, 20f); // distance for zooming
    private float _originZoomDistance;

    [Header("Scale with camera")] [SerializeField]
    private List<GameObject> _scaleWithCameraZoomObjects;

    [Header("Focus")] [SerializeField] private float _focusTweenDuration = 0.5f;
    [SerializeField] private Ease _focusEase;
    [SerializeField] private AudioClip _whoseSfx;

    [Header("Shake")] [SerializeField] private float _shakeDuration = 0.5f;
    [SerializeField] private float _shakeStrength = 0.2f;

    private bool _isShaking = false;
    private Vector3 _originalCameraPosition;

    public Action<GameObject> OnFinishFocusGameObject;
    public Action OnFinishShakeCamera; 
    
    
    
    private void Awake()
    {
        _camera = Camera.main;
        _originZoomDistance = _camera.orthographicSize;

        _originalCameraPosition = _camera.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartDragCamera();
        }

        if (Input.GetMouseButton(1))
        {
            DragCamera();
        }

        if (_canZoom) 
            HandleZooming();
    }


    // Store the initial mouse position
    public void StartDragCamera()
    {
        _lastMousePosition = _camera.ScreenToViewportPoint(Input.mousePosition);
        IsDraggingMouse = true;
    }

    public void DragCamera()
    {
        if (!IsDraggingMouse) return;

        // Calculate the mouse movement delta
        Vector3 currentMousePosition = _camera.ScreenToViewportPoint(Input.mousePosition);
        Vector3 mouseDelta = currentMousePosition - _lastMousePosition;

        // Calculate the camera movement direction based on mouse movement
        Vector3 dragDirection = new Vector3(-mouseDelta.x, -mouseDelta.y, 0f);
        Vector3 targetPosition = _camera.transform.position + dragDirection * _dragSpeed;
        Vector3 boundedTargetPosition =
            new Vector3(
                Mathf.Clamp(targetPosition.x, _horizontalBoundPosition.From, _horizontalBoundPosition.To),
                Mathf.Clamp(targetPosition.y, _verticalBoundPosition.From, _verticalBoundPosition.To),
                targetPosition.z);

        // Apply the drag movement
        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, boundedTargetPosition,
            ref _currentVelocity, _smoothTime);
        _lastMousePosition = currentMousePosition;
    }

    public void HandleZooming()
    {
        // Get the scroll wheel input
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the new zoom distance
        float newZoomDistance = _camera.orthographicSize - (scrollWheelInput * _zoomSpeed);

        newZoomDistance = Mathf.Clamp(newZoomDistance, _zoomBound.From, _zoomBound.To);

        // Update the camera position for zooming
        _camera.orthographicSize = newZoomDistance;

        foreach (var scaleWithCameraObject in _scaleWithCameraZoomObjects)
        {
            scaleWithCameraObject.transform.localScale = Vector3.one * (newZoomDistance / _originZoomDistance);
        }
    }

    public void SetActiveZoom(bool isActive)
    {
        _canZoom = isActive;
    }
    
    public void SetActiveDrag(bool isActive)
    {
        _canDrag = isActive;
    }

    public void FocusOnGameObject(GameObject targetGameObject)
    {
        // Calculate the target position for the camera
        Vector3 targetPosition = targetGameObject.transform.position;
        Vector3 boundedTargetPosition =
            new Vector3(
                Mathf.Clamp(targetPosition.x, _horizontalBoundPosition.From, _horizontalBoundPosition.To),
                Mathf.Clamp(targetPosition.y, _verticalBoundPosition.From, _verticalBoundPosition.To),
                transform.position.z);

        // Animate camera movement to the target position
        _camera.transform.DOMove(boundedTargetPosition, _focusTweenDuration).SetEase(_focusEase);

        // Calculate the new zoom distance for the camera
        float newZoomDistance = _originZoomDistance * (targetGameObject.transform.localScale.x);
        newZoomDistance = Mathf.Clamp(newZoomDistance, _zoomBound.From, _zoomBound.To);

        // Animate camera zoom to the new zoom distance
        _camera.DOOrthoSize(newZoomDistance, _focusTweenDuration).SetEase(_focusEase).OnComplete(
            () => { OnFinishFocusGameObject?.Invoke(targetGameObject);}
        );
        
    }


    public void ShakeCamera()
    {
        if (!_isShaking)
        {
            _isShaking = true;
            _originalCameraPosition = _camera.transform.position;

            _camera.transform.DOShakePosition(_shakeDuration, _shakeStrength)
                .OnComplete(() =>
                {
                    _camera.transform.position = _originalCameraPosition;

                    _isShaking = false;
                    
                    OnFinishShakeCamera?.Invoke();
                });
        }
    }
}