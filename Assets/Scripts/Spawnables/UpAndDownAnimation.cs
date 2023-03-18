using UnityEngine;

public class UpAndDownAnimation : MonoBehaviour
{
    [SerializeField] private float upAndDownSpeed;
    [SerializeField] private float downPosition;
    [SerializeField] private float upPosition;

    private bool goingUp;

    private void Awake()
    {
        goingUp = true;
    }

    private void Update()
    {
        float ydifference = upAndDownSpeed * Time.deltaTime;
        if (goingUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + ydifference, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - ydifference, transform.position.z);
        }

        if (transform.position.y > upPosition)
        {
            goingUp = false;
        }
        else if (transform.position.y < downPosition)
        {
            goingUp = true;
        }
    }
}
