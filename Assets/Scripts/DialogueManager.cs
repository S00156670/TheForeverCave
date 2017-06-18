using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public static DialogueManager Instance;
    public List<string> dialogueLines = new List<string>();
    public string npcName;
    public GameObject dialoguePannel;

    Button continueButton;
    Text dialogueText;
    Text nameText;
    int dialogueIndex;

 private void Awake()
    {
        continueButton = dialoguePannel.transform.Find("Continue").GetComponent<Button>();
        dialogueText = dialoguePannel.transform.Find("Text").GetComponent<Text>();
        nameText = dialoguePannel.transform.Find("Name").GetChild(0).GetComponent<Text>();
        // add listener to inspector for click
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });

        dialoguePannel.SetActive(false);

        // set an instance of dialogue manager to be referenced by game
           if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        //dialogueLines = new List<string>();
        dialogueLines.Clear();
        this.npcName = npcName;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        Debug.Log(dialogueLines.Count + " dialogue lines added");

        CreateDialogue();

    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[0];
        nameText.text = npcName;
        dialoguePannel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePannel.SetActive(false);
        }
    }

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
