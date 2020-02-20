// mostly https://nielson.dev/2015/08/the-pixel-grid-better-2d-in-unity-part-1

using UnityEngine;

public class SnapToPixelGrid : MonoBehaviour {
    [SerializeField]
    private int pixelsPerUnit = 32;

    /// <summary>
    /// Snap the object to the pixel grid determined by the given pixelsPerUnit.
    /// Using the parent's world position, this moves to the nearest pixel grid location by 
    /// offseting this GameObject by the difference between the parent position and pixel grid.
    /// </summary>
    private void LateUpdate() {
        transform.position = new Vector3(
            Mathf.Round(transform.position.x * pixelsPerUnit) / pixelsPerUnit,
            Mathf.Round(transform.position.y * pixelsPerUnit) / pixelsPerUnit
        );
    }
}