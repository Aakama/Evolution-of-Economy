using TMPro;
using UnityEngine;

public class GoldNpcDetector : MonoBehaviour
{
    public GoldNpcOccupations ThisNpc;
    public GoldNpcOccupations OtherNpc;

    public GameObject BuyPanel;

    public TextMeshProUGUI Prompt;

    private Animator playerAnim;
    private Animator otherNpcAnim;

    public bool IsAnotherNpcThere = false;
    public bool IsTrading = false;

    public bool allow = false;

    void Start()
    {
        ThisNpc = GetComponent<GoldNpcOccupations>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerAnim == null)
            return;

        if (IsAnotherNpcThere && otherNpcAnim != null)
        {
            if (GoldDialogueManager.Instance != null)
            {
                GoldDialogueManager.Instance.GoldStartdialogue(IsTrading, OtherNpc);
            }
            if (IsTrading)
            {
                GoldSwitchAnimations(true);
            }
            else
            {
                GoldSwitchAnimations(false);
            }
        }
        else
        {
            GoldResetAnimations();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != gameObject.layer)
            return;

        if (!other.TryGetComponent(out GoldNpcOccupations otherNpc))
            return;

        if (otherNpc == ThisNpc)
            return;

        OtherNpc = otherNpc;
        otherNpcAnim = OtherNpc.GetComponent<Animator>();
        IsAnotherNpcThere = true;
        IsTrading = GoldCheckForMatch(ThisNpc, OtherNpc);

        if (IsTrading)
        {
            Debug.Log("Hello");

            SetAndLoadBuyPanel(OtherNpc);
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

        if (!other.TryGetComponent(out GoldNpcOccupations otherNpc))
            return;

        if (otherNpc != OtherNpc)
            return;

        GoldResetAnimations();
        IsAnotherNpcThere = false;
        IsTrading = false;
        OtherNpc = null;
        otherNpcAnim = null;
    }

    bool GoldCheckForMatch(GoldNpcOccupations thisNpc, GoldNpcOccupations otherNpc)
    {
        bool match = otherNpc.Product.Purchased;
        return !match;
    }

    void GoldSwitchAnimations(bool trading)
    {
        playerAnim.SetBool("Trading", trading);
        otherNpcAnim.SetBool("Trading", trading);

        playerAnim.SetBool("Greeting", !trading);
        otherNpcAnim.SetBool("Greeting", !trading);
    }

    void GoldResetAnimations()
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

    public void Trade(GoldNpcOccupations player, GoldNpcOccupations otherNpc, bool Allow)
    {
        if (!(otherNpc.Product.Purchased) && Allow)
        {
            player.Product.ItemValue -= otherNpc.Product.ItemValue;
            otherNpc.Product.Purchased = true;
            allow = false;
            BuyPanel.SetActive(false);
        }
    }

    void SetAndLoadBuyPanel(GoldNpcOccupations otherNpc)
    {
        if (BuyPanel != null && Prompt != null)
        {
            Prompt.text = otherNpc.Product.Prompt;
            BuyPanel.SetActive(true);
        }
    }

    public void AllowTrade()
    {
        allow = true;

        if (IsAnotherNpcThere && OtherNpc != null)
        {
            Trade(ThisNpc, OtherNpc, allow);
        }
    }

    public void CloseBuyPanel()
    {
        if (BuyPanel != null)
        {
            BuyPanel.SetActive(false);
        }
    }
}
