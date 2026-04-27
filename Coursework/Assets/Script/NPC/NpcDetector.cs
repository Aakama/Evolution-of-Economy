using UnityEngine;

public class NpcDetector : MonoBehaviour
{
    public NpcOccupations ThisNpc;
    public NpcOccupations OtherNpc;

    string temp = "Bye";

    public bool IsAnotherNpcThere = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        // This gets the field of the npc
        ThisNpc = GetComponent<NpcOccupations>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (IsAnotherNpcThere) {
        //     Debug.Log(temp);
        // }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == gameObject.layer) {
            IsAnotherNpcThere = true;


            OtherNpc = other.GetComponent<NpcOccupations>();

            if (OtherNpc.Product == ThisNpc.Preference || ThisNpc.Product == OtherNpc.Preference) {
                Debug.Log("Hello");

                NpcTrading.Instance.ExecuteTradeLogic(ThisNpc, OtherNpc);
            }
            else {
                Debug.Log("Bye");
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == gameObject.layer) {
            IsAnotherNpcThere = false;
        }
    }
}
