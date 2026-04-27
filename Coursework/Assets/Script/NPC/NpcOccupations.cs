using UnityEngine;

// This are the occupations available
public enum Occupations {
    Herder,
    AppleFarmer,
    GrainFarmer,
    SaltTrader,
    Tailor,
    Cobbler
}

public class NpcOccupations : MonoBehaviour
{
    
    [Header("Identity")]
    public Occupations myJobs;

    // These are now Properties of occupations are visible
    [field: SerializeField] public string Product { get; private set; }
    [field: SerializeField] public string Preference { get; private set; }
    [field: SerializeField] public int ItemValue { get; private set; }
    [field: SerializeField] public float Perishability { get; private set; }
    [field: SerializeField] public bool IsPerishable { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NpcData();
    }

    void NpcData() {
        //Checks the occupation and gives the properties of the job
        switch (myJobs) {
            case Occupations.Herder:
                Product = "Milk";
                Preference = "Clothes";
                ItemValue = 1;
                Perishability = 8f;
                IsPerishable = true;
                gameObject.tag = "Herder";
                break;
            case Occupations.AppleFarmer:
                Product = "Apples";
                Preference = "shoes";
                ItemValue = 1;
                Perishability = 30f;
                IsPerishable = true;
                gameObject.tag = "AppleFarmer";
                break;
            case Occupations.GrainFarmer:
                Product = "Grains";
                Preference = "Milk";
                ItemValue = 2;
                Perishability = 60f;
                IsPerishable = true;
                gameObject.tag = "GrainFarmer";
                break;
            
            case Occupations.SaltTrader:
                Product = "Salt";
                Preference = "Grain";
                ItemValue = 2;
                Perishability = 0f;
                IsPerishable = false;
                gameObject.tag = "SaltTrader";
                break;
            
            case Occupations.Cobbler:
                Product = "Shoes";
                Preference = "Salt";
                ItemValue = 3;
                Perishability = 0f;
                IsPerishable = false;
                gameObject.tag = "Cobbler";
                break;
            
            case Occupations.Tailor:
                Product = "Clothes";
                Preference = "salt";
                ItemValue = 3;
                Perishability = 0f;
                IsPerishable = false;
                gameObject.tag = "Tailor";
                break;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
