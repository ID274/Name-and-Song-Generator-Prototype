using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    public static DialogueScript Instance { get; private set; }

    public List<string> unusedSongs1 = new List<string>();
    public List<string> unusedSongs2 = new List<string>();
    public List<string> usedSongs = new List<string>();

    public List<GameObject> vikings = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ButtonTest()
    {
        CreateSong();
    }


    void CreateSong()
    {
        if (NameGeneratorScript.Instance.fallenNames.Count != 0)
        {
            int fallenName = Random.Range(0, NameGeneratorScript.Instance.fallenNames.Count);
            int randomSong = Random.Range(0, unusedSongs1.Count);
            string Song = $"{unusedSongs1[randomSong]}, {NameGeneratorScript.Instance.fallenNames[fallenName]}, {unusedSongs2[randomSong]}";
            usedSongs.Add(Song);
        }
    }

   public void SingSong()
    {
        if (usedSongs.Count == 0)
        {
            CreateSong();
        }
        else if (usedSongs.Count < 0)
        {
            Debug.Log("Error: usedSongs.Count is lower than 0");
        }

        if (usedSongs.Count > 0)
        {
            //Choose song
            int randomSong = Random.Range(0, usedSongs.Count);
            string chosenSong = usedSongs[randomSong];

            for (int i = 0; i < vikings.Count; i++)
            {
                CharacterScript vikingScript = vikings[i].GetComponent<CharacterScript>();
                vikingScript.fullText = chosenSong;
                StartCoroutine(vikingScript.ShowText());
            }
        }
    }
}
