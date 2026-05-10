using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextAsset nonTradingDialogueFile;
    public string[] NonTradingDialogueLines;

    private string currentDialogue;

    public float textSpeed;

    public int Index;

    public GameObject DialoguePanel;

    public TextMeshProUGUI DialogueText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (nonTradingDialogueFile != null)
        {
            NonTradingDialogueLines = nonTradingDialogueFile.text.Split('\n');
        }
    }

    String RandomizeNonTradingDialogue()
    {
        if (NonTradingDialogueLines.Length == 0)
            return "";

        int randomIndex = UnityEngine.Random.Range(0, NonTradingDialogueLines.Length);
        currentDialogue = NonTradingDialogueLines[randomIndex];

        return currentDialogue;
    }

    String TradingDialogue(NpcOccupations Player)
    {
        switch (Player.MyJobs)
        {
            case Occupations.Herder:
                if (NpcTrading.currentDay == 1)
                {
                    currentDialogue =
                        "Herder: I heard that the Grain Farmer is looking for Milk. Maybe you should have a chat with him? He is just up the stairs to the east.";
                }
                else
                {
                    currentDialogue =
                        "Herder: The Milk spoils really fast. You should trade it as soon as possible.";
                }
                return currentDialogue;
            case Occupations.GrainFarmer:
                if (NpcTrading.currentDay == 1)
                {
                    currentDialogue =
                        "Grain Farmer: The Salt Trader is always looking for Grain. Maybe you should have a chat with him? He is just at south of the island near the tower.";
                }
                else
                {
                    currentDialogue =
                        "Grain Farmer: The self life of this grain is pretty low. You should trade it while it is still fresh.";
                }
                return currentDialogue;
            case Occupations.SaltTrader:
                if (NpcTrading.currentDay == 1)
                {
                    currentDialogue =
                        "Salt Trader: The Cobbler was searching for salt the other day. Maybe you should have a chat with him? He is just at the north west of the island near the tree.";
                }
                else
                {
                    currentDialogue =
                        "Salt Trader: The Salt is pretty stable, you can take your time to trade it.";
                }
                return currentDialogue;
            case Occupations.Cobbler:
                if (NpcTrading.currentDay == 1)
                {
                    currentDialogue =
                        "Cobbler: The Apple Farmer just broke their shoes yesterday. Maybe you should have a chat with him? He is just at the north east of the island near the tree.";
                }
                else
                {
                    currentDialogue =
                        "Cobbler: The shoes are pretty durable, you can take your time to trade it.";
                }
                return currentDialogue;
            case Occupations.AppleFarmer:
                if (NpcTrading.currentDay == 1)
                {
                    currentDialogue = "Apple Farmer: Pleasure doing business with you.";
                }
                else
                {
                    currentDialogue =
                        "Apple Farmer: You must really like apples to go through all the trouble of trading to get them.";
                }
                return currentDialogue;
            default:
                return "";
        }
    }

    public void Startdialogue(bool isTrading = false, NpcOccupations Player = null)
    {
        if (DialoguePanel == null || DialogueText == null)
        {
            Debug.LogError("DialoguePanel or DialogueText is not assigned in the inspector!");
            return;
        }

        if (DialoguePanel.activeSelf)
            return;

        if (isTrading)
        {
            currentDialogue = TradingDialogue(Player);
        }
        else
        {
            currentDialogue = RandomizeNonTradingDialogue();
        }
        Index = 0;

        DialoguePanel.SetActive(true);

        DialogueText.text = "";

        StartCoroutine(TypeDialogue(currentDialogue));

        Invoke("EndDialogue", currentDialogue.Length * textSpeed + 3f);
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        foreach (char letter in dialogue.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void EndDialogue()
    {
        DialoguePanel.SetActive(false);
        DialogueText.text = "";
    }
}
