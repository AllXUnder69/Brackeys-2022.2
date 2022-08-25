using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    #region Instance
    public static DialogueSystem Instance;
    void OnValidate()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private GameObject holder;

    [Space]
    [SerializeField] private string dialogueScriptPath;

    [Space]
    [SerializeField] private TMP_Text T_Speaker;
    [SerializeField] private TMP_Text T_SpeakerText;
    [Space]
    [SerializeField] private Button nextButton;

    [Space]
    [SerializeField] private List<DialogueColor> dialogueColors = new List<DialogueColor>();

    List<Dialogue> dialogues = new List<Dialogue>();
    string[] allLines;

    void Start()
    {
        T_Speaker.text = string.Empty;
        T_SpeakerText.text = string.Empty;
    }

    public void UpdateDialogue(int id)
    {
        if (holder.activeInHierarchy)
            return;

        holder.SetActive(true);

        using (StreamReader sr = new StreamReader(dialogueScriptPath))
        {
            allLines = File.ReadAllLines(dialogueScriptPath);

            List<string> idDialogueLines = new List<string>();

            int startLineIndex = Array.FindIndex(allLines, line => line == $"---{id}---");
            int nextLineIndex = Array.FindIndex(allLines, line => line == $"---{id + 1}---");

            if (nextLineIndex >= allLines.Length || nextLineIndex == -1)
                nextLineIndex = allLines.Length;

            for (int lineIndex = startLineIndex + 1; lineIndex < nextLineIndex; lineIndex++)
            {
                idDialogueLines.Add(allLines[lineIndex]);
            }

            foreach (string currentLine in idDialogueLines)
            {
                //-[Speaker 1] Talk talk talk
                print(currentLine);

                //-[Speaker 1] Talk talk talk
                string currSpeaker = currentLine.TrimStart(new char[] { ' ', '-', '[' });

                //Speaker 1
                currSpeaker = currSpeaker[..currSpeaker.IndexOf(']')];


                //-[Speaker 1] Talk talk talk
                string currSpeakerText = currentLine.TrimStart(new char[] { ' ', '-', '[' });

                currSpeakerText = currSpeakerText[currSpeakerText.IndexOf(']')..].TrimStart(']', ' ');

                dialogues.Add(new Dialogue(currSpeaker, currSpeakerText));
            }

            DisplayNextDialogue();
        }
    }

    bool stop = false;
    int currDialIndex = 0;
    public void DisplayNextDialogue()
    {
        if (currDialIndex >= dialogues.Count)
        {
            holder.SetActive(false);
            return;
        }

        Dialogue currDialogue = dialogues[currDialIndex];

        Color dialColor = dialogueColors.Find(dc => dc.speakerID == currDialogue.speaker) != null 
                ? dialogueColors.Find(dc => dc.speakerID == currDialogue.speaker).dialogueColor 
                : Color.white;

        if (dialColor == null)
        {
            T_Speaker.color = Color.white;
            T_SpeakerText.color = Color.white;
        }
        else
        {
            T_Speaker.color = dialColor;
            T_SpeakerText.color = dialColor;
        }

        T_Speaker.text = currDialogue.speaker;

        if (T_SpeakerText.text == string.Empty)
        {
            StartCoroutine(AnimateText(currDialogue.speakerText));
        }
        else
        {
            stop = true;

            T_SpeakerText.text = currDialogue.speakerText;

            currDialIndex++;

            stop = false;
        }
    }

    IEnumerator AnimateText(string speakerText)
    {
        T_SpeakerText.text = string.Empty;

        //nextButton.interactable = false;

        foreach (char letter in speakerText.ToCharArray())
        {
            if (!stop)
            {
                T_SpeakerText.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }

        //nextButton.interactable = true;

        StopCoroutine(AnimateText(string.Empty));
    }
}

public class Dialogue
{
    public string speaker;

    public string speakerText;

    public Dialogue(string _speaker, string _speakerText)
    {
        speaker = _speaker;
        speakerText = _speakerText;
    }
}

[System.Serializable]
public class DialogueColor
{
    public string speakerID;

    public Color dialogueColor;
}