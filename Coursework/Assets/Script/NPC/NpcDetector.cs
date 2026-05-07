using UnityEngine;

public class NpcDetector : MonoBehaviour
{
    public NpcOccupations ThisNpc;
    public NpcOccupations OtherNpc;

    private Animator playerAnim;
    private Animator otherNpcAnim;

    public bool IsAnotherNpcThere = false;
    public bool IsTrading = false;

    void Start()
    {
        ThisNpc = GetComponent<NpcOccupations>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerAnim == null)
            return;

        if (IsAnotherNpcThere && otherNpcAnim != null)
        {
            DialogueManager.Instance.Startdialogue(IsTrading, ThisNpc);
            if (IsTrading)
            {
                SwitchAnimations(true);
            }
            else
            {
                SwitchAnimations(false);
            }
        }
        else
        {
            ResetAnimations();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != gameObject.layer)
            return;

        if (!other.TryGetComponent(out NpcOccupations otherNpc))
            return;

        if (otherNpc == ThisNpc)
            return;

        OtherNpc = otherNpc;
        otherNpcAnim = OtherNpc.GetComponent<Animator>();
        IsAnotherNpcThere = true;
        IsTrading = CheckForMatch(ThisNpc, OtherNpc);

        if (IsTrading)
        {
            Debug.Log("Hello");
            NpcTrading.Instance.ExecuteTradeLogic(ThisNpc, OtherNpc);
        }
        else
        {
            Debug.Log("Bye");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != gameObject.layer)
            return;

        if (!other.TryGetComponent(out NpcOccupations otherNpc))
            return;

        if (otherNpc != OtherNpc)
            return;

        ResetAnimations();
        IsAnotherNpcThere = false;
        IsTrading = false;
        OtherNpc = null;
        otherNpcAnim = null;
    }

    bool CheckForMatch(NpcOccupations thisNpc, NpcOccupations otherNpc)
    {
        bool match = thisNpc.Product.ItemName == otherNpc.Preference;
        return match;
    }

    void SwitchAnimations(bool trading)
    {
        playerAnim.SetBool("Trading", trading);
        otherNpcAnim.SetBool("Trading", trading);

        playerAnim.SetBool("Greeting", !trading);
        otherNpcAnim.SetBool("Greeting", !trading);
    }

    void ResetAnimations()
    {
        if (playerAnim != null)
        {
            playerAnim.SetBool("Trading", false);
            playerAnim.SetBool("Greeting", false);
        }

        if (otherNpcAnim != null)
        {
            otherNpcAnim.SetBool("Trading", false);
            otherNpcAnim.SetBool("Greeting", false);
        }
    }
}
