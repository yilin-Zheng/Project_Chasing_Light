using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FeatherLib
{
    public class MouseLook : MonoBehaviour
    {
        public float mouseSensitivity = 5;
        public float smoothTime = 0.2f;
        public float minimumX = -85F;
        public float maximumX = 85F;
        public float minimumY = -360F;
        public float maximumY = 360F;
        private bool _pauseLook;
        public Transform _playerBody;
        private float _mouseX;
        private float _mouseY;
        private bool _cursorIsLocked;
        private float _xRot = 0;
        private float _yRot;
        private Quaternion _targetXRot;
        private Quaternion _targetYRot;
        private Quaternion _derivX;
        private Quaternion _derivY;
        //reference transform axes to use for quaternion rotation
        private Transform _refTransformCam;
        private Transform _refTransformChar;
        // Start is called before the first frame update
        void Start()
        {
            _pauseLook = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            _playerBody = transform.parent;
            //_targetYRot = _playerBody.rotation;
            _yRot = _playerBody.rotation.eulerAngles.y;
            Debug.Log("Start Y Rot: "+ _yRot);
        }

        // Update is called once per frame
        void Update()
        {
            _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            _mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            //this is for freezing when the game is paused
            //I don't do the calculation on timescale 0 because it will put the rotation at 0.
            if (!_pauseLook)
            {
                _xRot -= _mouseY*Time.timeScale;
                _yRot += _mouseX*Time.timeScale;
                //clamp the angle to the user-set limits
                _xRot = ClampAngle(_xRot, minimumX, maximumX);
                _yRot = ClampAngle(_yRot, minimumY, maximumY);
            
            
            
            //applying the rotation angle to the relative quaternion
            _targetXRot = Quaternion.AngleAxis(_xRot, Vector3.right);
            _targetYRot = Quaternion.AngleAxis(_yRot, Vector3.up);

            //transform.localRotation = _targetXRot;
            //_playerBody.rotation = _targetYRot;
            
            //now targeting that rotation!
            transform.localRotation = transform.localRotation.SmoothDamp(_targetXRot, ref _derivX, smoothTime );
            _playerBody.rotation = _playerBody.rotation.SmoothDamp(_targetYRot, ref _derivY, smoothTime );
            }
            
            InternalLockUpdate();
        }
        
        float ClampAngle (float angle, float min, float max)
        {
            angle = angle % 360;
            if ((angle >= -360F) && (angle <= 360F)) {
                if (angle < -360F) {
                    angle += 360F;
                }
                if (angle > 360F) {
                    angle -= 360F;
                }			
            }
            return Mathf.Clamp (angle, min, max);
        }
        
        private void InternalLockUpdate()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                _cursorIsLocked = false;
            }
            else if(Input.GetMouseButtonUp(0))
            { 
                _cursorIsLocked = true;
            }

            if (_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void Pause()
        {
            _pauseLook = !_pauseLook;
            if (_pauseLook)
            {
                transform.localRotation = Quaternion.AngleAxis(_xRot, Vector3.right);
                _playerBody.rotation = Quaternion.AngleAxis(_yRot, Vector3.up);
            }
            
        }
    }
}

