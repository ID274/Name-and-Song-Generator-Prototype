using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class NameGeneratorScript : MonoBehaviour
{
    public static NameGeneratorScript Instance { get; private set; }

    public List<string> usedNames = new List<string>();
    public List<string> fallenNames = new List<string>();

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

    public string heldName = "";


    //Create text file if doesn't exist - write the text into the files. There might be a better way of doing it as the file is
    //invisible to the player after building the game. We want it to be modable.
    void CreateText()
    {
        string path1 = Application.dataPath + "/MaleNames.txt";
        string path2 = Application.dataPath + "/FemaleNames.txt";

        if (!File.Exists(path1))
        {
            File.WriteAllText(path1, "Arne\nBirger\nBjorn\nBo\nErik\nFrode\nGorm\nHalfdan\nHarald\nKnud\nKare\nLeif\nNjal\nRoar\nRune\nSten\nSkarde\nSune\nSvend\nTroels\nToke\nTorsten\nTrygve\nUlf\nOdger\nAge");
        }
        if (!File.Exists(path2))
        {
            File.WriteAllText(path2, "Astrid\nBodil\nFrida\nGertrud\nGro\nEstrid\nHilda\nGudrun\nGunhild\nHelga\nInga\nLiv\nRandi\nSigne\nSigrid\nRevna\nSif\nTora\nTove\nThyra\nThurid\nYrsa\nUlfhild\nAse");
        }
    }

    public void GetNameMale()
    {
        string readFromFilePath = Application.dataPath + "/MaleNames" + ".txt";
        List<string> maleNames = File.ReadAllLines(readFromFilePath).ToList();

        int randomName = Random.Range(0, maleNames.Count);

        heldName = maleNames[randomName];

        if (usedNames.Contains(heldName))
        {
            heldName += "-son";
        }
        else
        {
            usedNames.Add(heldName);
        }
    }

    public void GetNameFemale()
    {
        string readFromFilePath = Application.dataPath + "/FemaleNames" + ".txt";
        List<string> femaleNames = File.ReadAllLines(readFromFilePath).ToList();

        int randomName = Random.Range(0, femaleNames.Count);

        heldName = femaleNames[randomName];

        if (usedNames.Contains(heldName))
        {
            heldName += "-dottir";
        }
        else
        {
            usedNames.Add(heldName);
        }
    }

    void Start()
    {
        CreateText();
    }
}
