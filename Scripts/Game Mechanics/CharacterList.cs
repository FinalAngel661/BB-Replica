using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    public List<CharacterString> characterList = new List<CharacterString>();

    public CharacterString returnCharID(string ID)
    {
        CharacterString retVal = null;

        for (int i = 0; i < characterList.Count; i++)
        {
            if (string.Equals(characterList[i].charId, ID))
            {
                retVal = characterList[i];
                break;
            }
        }

        return retVal;
    }

}

[System.Serializable]
public class CharacterString
{
    public string charId;
    public GameObject prefab;
}
