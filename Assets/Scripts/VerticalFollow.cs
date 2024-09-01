using UnityEngine;

public class VerticalFollow : MonoBehaviour
{
    public Transform target;  
    public float damping = 5.0f;
    public float offsetY = 2.0f;  

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, target.position.y + offsetY, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        }
    }
}