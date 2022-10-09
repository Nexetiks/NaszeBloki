using UnityEngine;

public class ThrowedObject : MonoBehaviour
{
    public Vector3 enemy = default;
    private void Update()
    {
        if(Mathf.Abs(gameObject.transform.position.z- enemy.z) < 0.5)
        {
            Destroy(this.gameObject);
        }
    }
}
