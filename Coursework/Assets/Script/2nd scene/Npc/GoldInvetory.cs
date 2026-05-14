using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldInvetory : MonoBehaviour
{
    public GameObject Information;
    public GameObject Hud;

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

    public void GoldContinue()
    {
        Information.SetActive(false);
        Hud.SetActive(true);
    }

    public void NextScene()
    {
        SceneManager.LoadScene("CashEra");
    }
}
