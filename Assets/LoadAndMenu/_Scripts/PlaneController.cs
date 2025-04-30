using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private float _speed = 5.0f;
    private float _rotationSpeed = 5;

    [SerializeField] private GameObject _camera;

    private float _cameraSpeedMultiplier = 30.2f;
    private float _cameraReturnSpeed = 5.0f;
    private float _joystickReleaseThreshold = 0.1f;
    private float _returnTolerance = 0.1f;
    private float _maxDistance = 4.0f;
    private float _desiredDistance = 3.0f;

    private Vector2 _lastDirection = Vector2.up;
    private Vector2 _currentDirection = Vector2.up;

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0))
        {
            SetDirection(Input.mousePosition);
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                SetDirection(touch.position);
            }
        }
#endif
    }

    void FixedUpdate()
    {
        // Движение
        transform.Translate(_currentDirection.normalized * _speed * Time.deltaTime, Space.World);

        // Камера
        Vector3 cameraToPlane = transform.position - _camera.transform.position;
        cameraToPlane.z = 0;

        if (_currentDirection.magnitude > _joystickReleaseThreshold)
        {
            if (cameraToPlane.magnitude < _maxDistance)
            {
                Vector3 targetPosition = transform.position + (Vector3)(_currentDirection * _speed * _cameraSpeedMultiplier * Time.deltaTime);
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, 0.1f);
            }
            else
            {
                if (_currentDirection != _lastDirection)
                {
                    Vector3 targetPosition = transform.position + (Vector3)(_currentDirection * _speed * Time.deltaTime);
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, 0.1f);

                    Vector3 desiredPosition = transform.position - (cameraToPlane.normalized * _desiredDistance);
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, desiredPosition, 0.1f);
                }
                else
                {
                    Vector3 targetPosition = transform.position + (Vector3)(_currentDirection * _speed * Time.deltaTime);
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, 0.1f);
                }
            }
        }
        else
        {
            if (cameraToPlane.magnitude > _returnTolerance)
            {
                Vector3 desiredPosition = transform.position - new Vector3(0, 0, 10);
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, desiredPosition, _cameraReturnSpeed * Time.deltaTime);
            }
        }

        Vector3 cameraPosition = _camera.transform.position;
        cameraPosition.z = -10;
        _camera.transform.position = cameraPosition;

        if (_currentDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _currentDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed);
        }

        _lastDirection = _currentDirection;
    }

    private void SetDirection(Vector3 screenPosition)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPos.z = transform.position.z;

        Vector2 dir = worldPos - transform.position;
        if (dir.magnitude > 0.01f)
        {
            _currentDirection = dir.normalized;
        }
    }
}