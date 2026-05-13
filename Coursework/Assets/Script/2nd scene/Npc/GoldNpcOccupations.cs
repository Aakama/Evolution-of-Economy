using UnityEngine;

// This are the occupations available
public enum Merchants
{
    Blacksmith,
    Baker,
    Butcher,
    Fisherman,
    FruitVendor,
    Player,
}

public class MerchantManager
{
    public string ItemName;
    public int ItemValue;
    public bool Purchased = false;
    public string Dialogue;
    public string Prompt;

    public MerchantManager(string Item, int Value, string Dialogue, string Prompt)
    {
        this.ItemName = Item;
        this.ItemValue = Value;
        this.Dialogue = Dialogue;
        this.Prompt = Prompt;
    }

    public MerchantManager(string Item, int Value, bool Purchased)
    {
        this.ItemName = Item;
        this.ItemValue = Value;
        this.Purchased = Purchased;
    }
}

public class GoldNpcOccupations : MonoBehaviour
{
    [Header("Identity")]
    public Merchants MyJobs;

    // These are now Properties of occupations are visible
    [field: SerializeField]
    public MerchantManager Product { get; set; }

    [field: SerializeField]
    public MerchantManager ItemValue { get; private set; }

    public string Dialogue;
    public string Prompt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GoldEraNpcData();
    }

    public void GoldEraNpcData()
    {
        //Checks the occupation and gives the properties of the job
        switch (MyJobs)
        {
            case Merchants.Blacksmith:
                Dialogue =
                    "Blacksmith: My armours are flying of the self cause of the new gold currency";
                Prompt = "The Armor costs 30 gold coins";
                Product = new MerchantManager("Armour", 30, Dialogue, Prompt);
                break;
            case Merchants.Baker:
                Dialogue = "Baker: I can finally make some real profit with the new gold coins";
                Prompt = "The Bread costs 10 gold coins";
                Product = new MerchantManager("Bread", 10, Dialogue, Prompt);
                break;
            case Merchants.FruitVendor:
                Dialogue =
                    "FruitVendor: Uhh these fresh fruits are gonna be hit with the new rise in demand.";
                Prompt = "The Fruit costs 5 gold coins";
                Product = new MerchantManager("Fruit", 5, Dialogue, Prompt);
                break;
            case Merchants.Fisherman:
                Dialogue =
                    "Fisherman: I can finally expand my fishing business with the new surge in demand.";
                Prompt = "The Fish costs 15 gold coins";
                Product = new MerchantManager("Fish", 15, Dialogue, Prompt);
                break;
            case Merchants.Butcher:
                Dialogue =
                    "Butcher: New markets are opened everywhere cause of the rise in demands";
                Prompt = "The Meat costs 20 gold coins";
                Product = new MerchantManager("Meat", 20, Dialogue, Prompt);
                break;
            case Merchants.Player:
                Product = new MerchantManager("Gold", 100, true);
                break;
        }
    }
}
