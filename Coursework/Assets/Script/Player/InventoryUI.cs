using TMPro; // Use TextMeshPro for better looking text
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public NpcOccupations Player;

    public Timer TimerManager; // Reference to your Timer script

    [Header("UI Elements")]
    public TextMeshProUGUI DayText;

    public GameObject ToolTipPanel;

    public TextMeshProUGUI ToolTipText;

    public GameObject DialoguePanel;

    public TextMeshProUGUI DialogueText;

    public Slider TimerSlider;

    [Header("Item Icons")]
    public Image ItemIconDisplay;

    public Sprite MilkSprite;
    public Sprite ClothesSprite;
    public Sprite AppleSprite;
    public Sprite ShoesSprite;
    public Sprite GrainSprite;
    public Sprite SaltSprite;

    void Awake()
    {
        UpdateDayText();
    }

    void Update()
    {
        Cursor.visible = true;

        UpdateIcon();

        if (ToolTipPanel == null)
        {
            ToolTipPanel = GameObject.Find("ToolTip Background");
        }

        if (ToolTipPanel != null && !ToolTipPanel.activeSelf)
        {
            UpdateToolTip();
        }

        if (TimerSlider == null)
        {
            TimerSlider = GameObject.FindAnyObjectByType<UnityEngine.UI.Slider>();
        }

        if (TimerSlider != null)
        {
            UpdateSlider();
        }

        // UpdateBackgroundColor();
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

            TimerSlider.value = TimerManager.TimeRemaining / TimerManager.Duration;
        }
        else
        {
            TimerSlider.gameObject.SetActive(false);
        }
    }

    // void UpdateBackgroundColor()
    // {
    //     if (TimerManager != null && TimerManager.IsRunning)
    //     {
    //         if (TimerManager.TimeRemaining <= 0)
    //         {
    //             // ItemNameText.text = "SPOILED " + Player.Product.ItemName;
    //             BackgroundColor.color = Color.red;
    //         }
    //     }
    // }

    void UpdateDayText()
    {
        if (DayText != null)
        {
            DayText.text = "Day - " + NpcTrading.currentDay;
        }
    }

    void UpdateToolTip()
    {
        if (ToolTipText != null)
        {
            switch (Player.MyJobs)
            {
                case Occupations.Herder:
                    ToolTipText.text = "The GrainFarmer is up the stairs to the east.";
                    break;
                case Occupations.GrainFarmer:
                    ToolTipText.text =
                        "The Salt Trader is just at south of the island near the tower.";
                    break;
                case Occupations.SaltTrader:
                    ToolTipText.text =
                        "The Cobbler is just at the north west of the island near the tree.";
                    break;
                case Occupations.Cobbler:
                    ToolTipText.text =
                        "The Apple Farmer is just at the north east of the island near the tree.";
                    break;
            }
        }
    }

    public void SetAndShowToolTip()
    {
        if (ToolTipPanel.gameObject.activeSelf == false)
        {
            ToolTipPanel.gameObject.SetActive(true);
        }
        else
        {
            ToolTipPanel.gameObject.SetActive(false);
        }
    }

    public void SetAndShowDialogue(string dialogue)
    {
        if (DialoguePanel.gameObject.activeSelf == false)
        {
            DialoguePanel.gameObject.SetActive(true);
            DialogueText.text = dialogue;
        }
        else
        {
            DialoguePanel.gameObject.SetActive(false);
            DialogueText.text = "";
        }
    }
}
