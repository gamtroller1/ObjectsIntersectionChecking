using UnityEngine;
using UnityEngine.UI;

public class ObjectsIntersectionRatioChecker : MonoBehaviour
{
    public bool Debug;
    public Text PercentageText;

    float percentageInside;
    private void Update()
    {
        if (Debug)
            PercentageText.text = "objects overlapping: " + percentageInside + "%";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Calculate the percentage of this object inside the player object
            percentageInside = CalculateIntersectionPercentage(other.gameObject , gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Completely exited
            percentageInside = 0;
        }
    }

    private float CalculateIntersectionPercentage(GameObject obj1 , GameObject obj2)
    {
        // Calculate the intersection volume or area and return the percentage
        // This calculation depends on the shapes of the objects

        // Get the intersection bounds between the colliders of the two objects
        Bounds intersectionBounds = GetIntersectionBounds(obj1.GetComponent<Collider>().bounds , obj2.GetComponent<Collider>().bounds);

        // Calculate the volume of intersection
        float intersectionVolume = intersectionBounds.size.x * intersectionBounds.size.y * intersectionBounds.size.z;

        // Calculate the volume of obj1
        Bounds obj1Bounds = obj1.GetComponent<Collider>().bounds;
        float totalVolumeObj1 = obj1Bounds.size.x * obj1Bounds.size.y * obj1Bounds.size.z;

        // Calculate the percentage
        return Mathf.Clamp((intersectionVolume / totalVolumeObj1) * 100f , 0f , 100f);
    }

    private Bounds GetIntersectionBounds(Bounds bounds1 , Bounds bounds2)
    {
        // Get the intersection bounds between two sets of bounds
        Vector3 min = Vector3.Max(bounds1.min , bounds2.min);
        Vector3 max = Vector3.Min(bounds1.max , bounds2.max);
        return new Bounds((min + max) / 2f , max - min);
    }
}
