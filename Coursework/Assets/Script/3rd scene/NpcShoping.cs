using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NpcShoping : MonoBehaviour
{
    [Header("Check out")]
    public Animator Anim1;
    public Animator Anim2;
    public Animator Anim3;

    [Header("Check out")]
    public GameObject CheckOut;

    [Header("Exit")]
    public GameObject Exit;

    [Header("Looking")]
    public GameObject Looking1;
    public GameObject Looking2;
    public GameObject Looking3;
    public GameObject Looking4;

    [Header("Customers")]
    public NavMeshAgent Customer1;
    public NavMeshAgent Customer2;
    public NavMeshAgent Customer3;

    [Header("Buying Panel")]
    public GameObject BuyPanel;

    [Header("Buying Panel")]
    public static int TurnOver;

    [HideInInspector]
    public int price;
    public TextMeshProUGUI text;

    [Header("UI")]
    public TextMeshProUGUI Cashier;

    void Start()
    {
        Invoke("Start1", 2f);
        Invoke("Start2", 7f);
        Invoke("Start3", 20f);
    }

    void Update()
    {
        GenerateCashier();
        UpdateAnimations(Customer1, Anim1);
        UpdateAnimations(Customer2, Anim2);
        UpdateAnimations(Customer2, Anim3);
    }

    void Start1()
    {
        Move(Customer1);
    }

    void Start2()
    {
        Move(Customer2);
    }

    void Start3()
    {
        Move(Customer3);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Customers")
        {
            return;
        }
        PauseForCheckOut();

        ExitShop(other.GetComponent<NavMeshAgent>());
    }

    void Move(NavMeshAgent customer)
    {
        customer.GetComponent<NavMeshAgent>();

        switch (Random.Range(0, 4))
        {
            case 0:
                customer.SetDestination(Looking1.transform.position);
                price = 1000;
                break;
            case 1:
                customer.SetDestination(Looking2.transform.position);
                price = 2500;
                break;
            case 2:
                customer.SetDestination(Looking3.transform.position);
                price = 2000;
                break;
            case 3:
                customer.SetDestination(Looking4.transform.position);
                price = 5000;
                break;
        }

        StartCoroutine(WaitAndMove(customer, Random.Range(5f, 15f)));
        // Invoke("CheckoutMove(customer)", Random.Range(4f, 15f));
    }

    IEnumerator WaitAndMove(NavMeshAgent customer, float delay)
    {
        yield return new WaitForSeconds(delay);
        CheckOutMove(customer);
    }

    void CheckOutMove(NavMeshAgent customer)
    {
        customer.GetComponent<NavMeshAgent>();
        customer.SetDestination(CheckOut.transform.position);
    }

    void PauseForCheckOut()
    {
        GenerateText();
        Time.timeScale = 0f;
        BuyPanel.SetActive(true);
    }

    public void Bought()
    {
        TurnOver += price;
        UnpauseGameForCheckout();
    }

    public void UnpauseGameForCheckout()
    {
        Time.timeScale = 1f;
        BuyPanel.SetActive(false);
    }

    void GenerateText()
    {
        text.text =
            $"I want to buy this jewellery for {price} and my credit score is {Random.Range(300, 850)}";
    }

    void GenerateCashier()
    {
        Cashier.text = $"$-{TurnOver}";
    }

    void ExitShop(NavMeshAgent other)
    {
        other.SetDestination(Exit.transform.position);
    }

    void UpdateAnimations(NavMeshAgent Agent, Animator Anim)
    {
        float speed = Agent.velocity.magnitude;

        Anim.SetFloat("Speed", speed);
    }
}
