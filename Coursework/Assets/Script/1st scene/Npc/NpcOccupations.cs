using UnityEngine;

// This are the occupations available
public enum Occupations
{
    Herder,
    AppleFarmer,
    GrainFarmer,
    SaltTrader,
    Tailor,
    Cobbler,
}

public class ItemManager
{
    public string ItemName;
    public int ItemValue;
    public bool IsPerishable;
    public float Perishability;

    public ItemManager(string Item, int Value, bool Perishable, float Perishability)
    {
        this.ItemName = Item;
        this.ItemValue = Value;
        this.IsPerishable = Perishable;
        this.Perishability = Perishability;
    }

    public ItemManager(string Item, int Value, bool Perishable)
    {
        this.ItemName = Item;
        this.ItemValue = Value;
        this.IsPerishable = Perishable;
    }
}

public class NpcOccupations : MonoBehaviour
{
    [Header("Identity")]
    public Occupations MyJobs;

    // These are now Properties of occupations are visible
    [field: SerializeField]
    public ItemManager Product { get; set; }

    [field: SerializeField]
    public string Preference { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NpcData();
    }

    public void NpcData()
    {
        //Checks the occupation and gives the properties of the job
        switch (MyJobs)
        {
            case Occupations.Herder:
                Product = new ItemManager("Milk", 1, true, 10f);
                Preference = "Clothes";
                gameObject.tag = "Herder";
                break;
            case Occupations.AppleFarmer:
                Product = new ItemManager("Apple", 1, false);
                Preference = "Shoes";
                gameObject.tag = "AppleFarmer";
                break;
            case Occupations.GrainFarmer:
                Product = new ItemManager("Grain", 2, true, 60f);
                Preference = "Milk";
                gameObject.tag = "GrainFarmer";
                break;

            case Occupations.SaltTrader:
                Product = new ItemManager("Salt", 2, false);
                Preference = "Grain";
                gameObject.tag = "SaltTrader";
                break;

            case Occupations.Cobbler:
                Product = new ItemManager("Shoes", 3, false);
                Preference = "Salt";
                gameObject.tag = "Cobbler";
                break;

            case Occupations.Tailor:
                Product = new ItemManager("Clothes", 3, false);
                Preference = "Apple";
                gameObject.tag = "Tailor";
                break;
        }
    }
}
