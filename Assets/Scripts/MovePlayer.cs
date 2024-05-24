using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float movementSpeed = 5;

    void Update()
    {
        float input = Input.GetAxis("Vertical");

        transform.position += new Vector3(0 , 0 , input * Time.deltaTime * movementSpeed);
    }
}
