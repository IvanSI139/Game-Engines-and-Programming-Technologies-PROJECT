using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public Vector2 parallaxFactor; // Separate factors for X and Y movement.

    public void Move(Vector2 delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta.x * parallaxFactor.x; // Apply X-axis parallax.
        newPos.y -= delta.y * parallaxFactor.y; // Apply Y-axis parallax.

        transform.localPosition = newPos;
    }
}

