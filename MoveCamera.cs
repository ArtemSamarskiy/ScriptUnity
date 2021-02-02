using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField, Header("Follow")]
    private Transform follow;

    [SerializeField, Header("Zoom controll")]
    private Vector2 zoomClamp = new Vector2(.5f, 70f);
    private float zoomSpeed = 10f;
    
    private Camera _camera;
    private Vector2 _posPivot;
    
    
    private void Awake() => _camera = GetComponent<Camera>();

    private void Update()
    {
        var positionMouse = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);

        #region Zoom

        // Zoom
        _camera.orthographicSize += -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, zoomClamp.x, zoomClamp.y);

        #endregion

        #region Move

        if (follow)
            transform.Translate((Vector2)follow.position - (Vector2)transform.position);
        else
        {
            if (Input.GetMouseButtonDown(2))
                _posPivot = positionMouse;

            if (Input.GetMouseButton(2))
                transform.Translate(_posPivot - positionMouse);
        }

        #endregion
    }
}
