using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Reference to the UI text
    public string[] conversationLines; // Array of pre-scripted conversation lines
    public int currentLineIndex = 0; // Track the current conversation line

    private void Start()
    {
        dialogueText.gameObject.SetActive(false); // Hide the text initially
        UpdateDialogue(); // Ensure dialogue is initialized correctly
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.SetCurrentNPC(this); // Set this NPC as the current NPC for the player
            }
            dialogueText.text = conversationLines[currentLineIndex]; // Set the initial message
            dialogueText.gameObject.SetActive(true); // Show the text
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.SetCurrentNPC(null); // Clear the current NPC for the player
            }
            dialogueText.gameObject.SetActive(false); // Hide the text when the player leaves
        }
    }

    public void GoBackInTime()
    {
        if (currentLineIndex > 0)
        {
            currentLineIndex--;
            UpdateDialogue();
        }
    }

    public void AdvanceInTime()
    {
        if (currentLineIndex < conversationLines.Length - 1)
        {
            currentLineIndex++;
            UpdateDialogue();
        }
    }

    private void UpdateDialogue()
    {
        if (currentLineIndex >= 0 && currentLineIndex < conversationLines.Length)
        {
            dialogueText.text = conversationLines[currentLineIndex];
        }
    }
}