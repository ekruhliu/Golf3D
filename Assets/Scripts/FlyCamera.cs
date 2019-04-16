using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour 
{
        public float mouseSensitivity = 3f;
        public float speed = 15f;
        private Vector3 transfer;
        public float minimumX = -360f;
        public float maximumX = 360f;
        public float minimumY = -60f;
        public float maximumY = 60f;
        float rotationX = 0f;
        float rotationY = 0f;
        Quaternion originalRotation;
        public Camera camera;

        public bool freeCamOn;
        public bool followBall;
        public GameObject ball;
        
        void Awake(){
                camera.orthographic = false;
                followBall = true;
        }
        
        void Start(){
                originalRotation = transform.rotation;
        }
        
        void FixedUpdate(){
                if (freeCamOn)
                {
                        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
                        rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
                        rotationX = ClampAngle(rotationX, minimumX, maximumX);
                        rotationY = ClampAngle(rotationY, minimumY, maximumY);
                        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
                        transform.rotation = originalRotation * xQuaternion * yQuaternion;

                        if (Input.GetKeyDown(KeyCode.LeftShift))
                                speed *= 10;
                        else if (Input.GetKeyUp(KeyCode.LeftShift))
                                speed /= 10;

                        Vector3 newPos = new Vector3(0, 1, 0);
                        if (Input.GetKey(KeyCode.E))
                                transform.position += newPos * speed * Time.fixedDeltaTime;
                        else if (Input.GetKey(KeyCode.Q))
                                transform.position -= newPos * speed * Time.fixedDeltaTime;

                        transfer = transform.forward * Input.GetAxis("Vertical");
                        transfer += transform.right * Input.GetAxis("Horizontal");
                        transform.position += transfer * speed * Time.fixedDeltaTime;
                }
                else
                {
                        if (followBall)
                        {
                                transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y + 2f, ball.transform.position.z + 15f);
                                followBall = false;
                        }
                        if (Input.GetKey(KeyCode.A))
                                transform.RotateAround(ball.transform.position, new Vector3(0.0f, 1.0f, 0.0f), 100 * Time.fixedDeltaTime * 2);
                        if (Input.GetKey(KeyCode.D))
                                transform.RotateAround(ball.transform.position, new Vector3(0.0f, 1.0f, 0.0f), 100 * Time.fixedDeltaTime * -2);
                }

                if (Input.GetKey(KeyCode.F) && !freeCamOn)
                {
                        freeCamOn = true;
                        followBall = false;
                }
                else if (Input.GetKey(KeyCode.F) && freeCamOn)
                {
                        freeCamOn = false;
                        followBall = true;
                }
        }
        
        public static float ClampAngle (float angle, float min, float max){
                if (angle < -360F) angle += 360F;
                if (angle > 360F) angle -= 360F;
                return Mathf.Clamp (angle, min, max);
        }       
}
