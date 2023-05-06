using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    public void RotateX(float amount)
    {
        Vector3 r = new Vector3(amount*360*-rotateSpeed, 0, 0);
        
        Quaternion localRotation = transform.localRotation;
        if (transform.localRotation.x*360 + r.x > 180)
        {
            //localRotation = new Quaternion(0.5f, localRotation.y, localRotation.z, localRotation.w);
            localRotation.x = 0.5f;
            transform.localRotation = localRotation;
            
        }else if ( transform.localRotation.x*360 + r.x < -180)
        {
            //localRotation = new Quaternion(-0.5f, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
            localRotation.x = -0.5f;
            transform.localRotation = localRotation;

        }
        else
        {
            var transformLocalPosition = transform.localPosition;
            transformLocalPosition.z = transform.localRotation.x*360;
            transform.localPosition = transformLocalPosition;
            print(transformLocalPosition + " local");
            transform.Rotate(r, Space.Self);
        }
    } 
}
