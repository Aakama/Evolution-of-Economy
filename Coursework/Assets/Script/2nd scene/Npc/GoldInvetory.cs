using TMPro;
using UnityEngine;

public class GoldInvetory : MonoBehaviour
{
    public TextMeshProUGUI counter;

    public GoldNpcOccupations goldNpcOccupations;

    void Start()
    {
        if (counter == null)
        {
            Debug.LogError("Error");
        }
        else
        {
            counter.text = "x" + goldNpcOccupations.Product.ItemValue.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter.text = "x" + goldNpcOccupations.Product.ItemValue.ToString();
    }
}
