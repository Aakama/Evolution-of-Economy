using UnityEngine;

public class NpcLookAt : MonoBehaviour
{
    public GameObject player;

    public int lookDistance = 7;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < lookDistance)
        {
            Vector3 tmp = player.transform.position;
            tmp.y = this.transform.position.y;
            Quaternion targetRotation = Quaternion.LookRotation(tmp - transform.position);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * 5f
            );
        }
    }
}
