using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private bool male;
    public string vikingName;

    [SerializeField] private GameObject nameObject, dialogueObject;
    [SerializeField] private TMP_Text nameText, dialogueText;

    //Typewriter effect
    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";

    private void Start()
    {
        nameText = nameObject.GetComponent<TMP_Text>();
        dialogueText = dialogueObject.GetComponent<TMP_Text>();
        DialogueScript.Instance.vikings.Add(this.gameObject);
        if (nameText != null)
        {
            if (male)
            {
                NameGeneratorScript.Instance.GetNameMale();

                vikingName = NameGeneratorScript.Instance.heldName;
                nameText.text = vikingName;
            }
            else
            {
                NameGeneratorScript.Instance.GetNameFemale();

                vikingName = NameGeneratorScript.Instance.heldName;
                nameText.text = vikingName;
            }
        }
        else
        {
            Debug.Log("Error");
        }
        if (NameGeneratorScript.Instance.fallenNames.Contains(vikingName))
        {
            NameGeneratorScript.Instance.fallenNames.Remove(vikingName);
        }
    }

    void OnMouseDown()
    {
        NameGeneratorScript.Instance.fallenNames.Add(vikingName);
        if (NameGeneratorScript.Instance.usedNames.Contains(vikingName))
        {
            NameGeneratorScript.Instance.usedNames.Remove(vikingName);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        DialogueScript.Instance.vikings.Remove(this.gameObject);
    }

    public IEnumerator ClearDialogue()
    {
        yield return new WaitForSeconds(2);
        dialogueText.text = string.Empty;
    }

    public IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        StartCoroutine(ClearDialogue());
    }
}
