using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(Vector2 deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private Vector2 oldPosition;

    void Start()
    {
        oldPosition = transform.position;
    }

    void Update()
    {
        Vector2 newPosition = transform.position;
        if (newPosition != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                Vector2 delta = oldPosition - newPosition;
                onCameraTranslate(delta);
            }

            oldPosition = newPosition;
        }
    }
}
