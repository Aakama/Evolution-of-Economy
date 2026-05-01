using TMPro; // Use TextMeshPro for better looking text
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public NpcOccupations Player;

    public Timer TimerManager; // Reference to your Timer script

    [Header("UI Elements")]
    public TextMeshProUGUI ItemNameText;

    public Slider TimerSlider;

    public Image BackgroundColor; // Optional: change color when spoiled

    [Header("Item Icons")]
    public Image ItemIconDisplay; // Drag your new 'ItemIcon' UI Image here

    // Drag your actual 2D pictures into these slots in the Inspector
    public Sprite MilkSprite;
    public Sprite ClothesSprite;
    public Sprite AppleSprite;
    public Sprite ShoesSprite;
    public Sprite GrainSprite;
    public Sprite SaltSprite; // Add as many as you need!

    void Update()
    {
        UpdateIcon();

        UpdateSlider();

        UpdateBackgroundColor();
    }

    void UpdateIcon()
    {
        switch (Player.MyJobs)
        {
            case Occupations.Herder:
                ItemIconDisplay.sprite = MilkSprite;
                break;
            case Occupations.Tailor:
                ItemIconDisplay.sprite = ClothesSprite;
                break;
            case Occupations.AppleFarmer:
                ItemIconDisplay.sprite = AppleSprite;
                break;
            case Occupations.Cobbler:
                ItemIconDisplay.sprite = ShoesSprite;
                break;
            case Occupations.SaltTrader:
                ItemIconDisplay.sprite = SaltSprite;
                break;
            case Occupations.GrainFarmer:
                ItemIconDisplay.sprite = GrainSprite;
                break;
        }
    }

    void UpdateSlider()
    {
        if (TimerManager != null && TimerManager.IsRunning)
        {
            TimerSlider.gameObject.SetActive(true);

            // Percentage = Current / Max (e.g., 4s / 8s = 0.5)
            TimerSlider.value = TimerManager.TimeRemaining / TimerManager.Duration;
        }
        else
        {
            TimerSlider.gameObject.SetActive(false);
        }
    }

    void UpdateBackgroundColor()
    {
        if (TimerManager != null && TimerManager.IsRunning)
        {
            if (TimerManager.TimeRemaining <= 0)
            {
                ItemNameText.text = "SPOILED " + Player.Product.ItemName;
                BackgroundColor.color = Color.red; // Uncomment if you have a background image to change color
            }
        }
    }
}
