using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent Agent;

    private Animator Anim;

    [Header("Movement Settings")]
    public float MoveSpeed = 10f;

    [Header("Input Setting")]
    [SerializeField]
    float SampleDistance = 0.5f;

    [SerializeField]
    LayerMask GroundLayer;

    [Header("Visual Feedback")]
    public GameObject ClickMarkerPrefab;
    public GameObject FailedClickMarkerPrefab;

    public static event System.Action<Vector3> OnGroundTouch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Anim = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();

        Agent.speed = MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Anim != null && Agent != null)
        {
            UpdateAnimations();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, GroundLayer))
            {
                if (
                    NavMesh.SamplePosition(
                        hit.point,
                        out NavMeshHit navMeshHit,
                        SampleDistance,
                        NavMesh.AllAreas
                    )
                )
                {
                    Agent.SetDestination(navMeshHit.position);

                    OnGroundTouch?.Invoke(navMeshHit.position);

                    if (ClickMarkerPrefab != null)
                    {
                        // Calculate the position slightly above ground
                        Vector3 spawnPos = navMeshHit.position + new Vector3(0, 0.1f, 0);

                        // Instantiate creates a brand new copy of the Prefab in the scene
                        GameObject newMarker = Instantiate(
                            ClickMarkerPrefab,
                            spawnPos,
                            Quaternion.identity
                        );

                        // Destroy it after 1 second so your game doesn't get cluttered
                        Destroy(newMarker, 0.5f);
                    }
                }
                else
                {
                    Debug.Log("Clicked point is not on a walkable area.");
                    if (FailedClickMarkerPrefab != null)
                    {
                        // Calculate the position slightly above ground
                        Vector3 spawnPos = transform.position + new Vector3(0, -1f, 0);

                        // Instantiate creates a brand new copy of the Prefab in the scene
                        GameObject newMarker = Instantiate(
                            FailedClickMarkerPrefab,
                            spawnPos,
                            Quaternion.identity
                        );

                        // Destroy it after 1 second so your game doesn't get cluttered
                        Destroy(newMarker, 0.5f);
                    }
                }
            }
        }
    }

    void UpdateAnimations()
    {
        float speed = Agent.velocity.magnitude;

        Anim.SetFloat("Speed", speed);
    }
}
