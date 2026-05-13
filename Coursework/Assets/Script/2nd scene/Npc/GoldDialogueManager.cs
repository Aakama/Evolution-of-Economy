using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GoldDialogueManager : MonoBehaviour
{
    public static GoldDialogueManager Instance;

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

    String GoldRandomizeNonTradingDialogue()
    {
        if (NonTradingDialogueLines.Length == 0)
            return "";

        int randomIndex = UnityEngine.Random.Range(0, NonTradingDialogueLines.Length);
        currentDialogue = NonTradingDialogueLines[randomIndex];

        return currentDialogue;
    }

    public void GoldStartdialogue(bool isTrading = false, GoldNpcOccupations Player = null)
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
            currentDialogue = Player.Product.Dialogue;
        }
        else
        {
            currentDialogue = GoldRandomizeNonTradingDialogue();
        }
        Index = 0;

        DialoguePanel.SetActive(true);

        DialogueText.text = "";

        StartCoroutine(GoldTypeDialogue(currentDialogue));

        Invoke("GoldEndDialogue", currentDialogue.Length * textSpeed + 3f);
    }

    IEnumerator GoldTypeDialogue(string dialogue)
    {
        foreach (char letter in dialogue.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void GoldEndDialogue()
    {
        DialoguePanel.SetActive(false);
        DialogueText.text = "";
    }
}
