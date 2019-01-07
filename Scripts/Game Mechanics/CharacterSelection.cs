using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public int numberofUsers;
    public int numberofPlayers;

    public List<PlayerBase> players = new List<PlayerBase>();
    public List<PlayerInterface> p1Interface = new List<PlayerInterface>();
    public List<CharacterPrefab> characterList = new List<CharacterPrefab>();

    public IconInfo[] IconPrefabs;
    public int maxX;
    public int maxY;
    IconInfo[,] charGrid;
    public GameObject IconCanvas;

    bool loadLevel;
    public bool bothPlayersSelected;
    bool canRun;

    public Client SChar;

    private void Start()
    {
        numberofPlayers = numberofUsers;

        charGrid = new IconInfo[maxX, maxY];

        int x = 0;
        int y = 0;

        IconPrefabs = IconCanvas.GetComponentsInChildren<IconInfo>();

        for (int i = 0; i < IconPrefabs.Length; i++)
        {
            IconPrefabs[i].posX += x;
            IconPrefabs[i].posY += y;

            charGrid[x, y] = IconPrefabs[i];

            if (x < maxX - 1)
            {
                x++;
            }
            else
            {
                x = 0;
                y++;
            }
        }        
    }

    private void Update()
    {
        if (!loadLevel)
        {
            for (int i = 0; i < p1Interface.Count; i++)
            {
                if (i < numberofPlayers)
                {
                    if (!players[i].charSelected)
                    {
                        Debug.Log("Did we get here?");
                        p1Interface[i].playerBase = players[i];

                        CursorPos(p1Interface[i]);
                        SelScreenInput(p1Interface[i], players[i].inputId);
                        IconPreview(p1Interface[i]);
                    }
                }
                else
                {
                    players[i].charSelected = true;
                }
            }
        }

        if (bothPlayersSelected == true && canRun == true)
        {
            Debug.Log("YEET");
            canRun = false;
            loadLevel = true;
            LoadBattle();

        }
        else
        {
            if (players[0].charSelected && players[1].charSelected)
            {
                bothPlayersSelected = true;
            }
        }

    }

    private void SelScreenInput(PlayerInterface p1, string playerId)
    {
        #region Grid Navigation

        Debug.Log("You can Select" + playerId);

        float vertical = Input.GetAxis("Vertical" + playerId);

        if (vertical != 0)
        {
            if (!p1.hitEnter)
            {
                if (vertical > 0)
                {
                    p1.activeY = (p1.activeY > 0) ? p1.activeY - 1 : maxY - 1;
                }
                else
                {
                    p1.activeY = (p1.activeY < maxY - 1) ? p1.activeY + 1 : 0;
                }

                p1.hitEnter = true;
            }
        }

        float horizontal = Input.GetAxis("Horizontal" + playerId);

        if (horizontal != 0)
        {
            if (!p1.hitEnter)
            {
                if (horizontal > 0)
                {
                    p1.activeX = (p1.activeX > 0) ? p1.activeX - 1 : maxX - 1;
                }
                else
                {
                    p1.activeX = (p1.activeX < maxX - 1) ? p1.activeX + 1 : 0;
                }

               
                p1.hitEnter = true;
            }
        }

        if (vertical == 0 && horizontal == 0)
        {
            p1.hitEnter = false;
        }

        if (p1.hitEnter)
        {
        }

        #endregion

        if (Input.GetButtonUp("Submit" + playerId))
        {
            //p1.createdCharacter.GetComponentInChildren<Animator>().Play("Victory");

            p1.playerBase.playChar = returnCharID(p1.activeIcon.characterId).prefab;

            p1.playerBase.charSelected = true;

            Debug.Log("Player selected:" + p1.activeIcon.characterId);

        }
    }

    private void CursorPos(PlayerInterface p1)
    {
        p1.Cursor.SetActive(true);

        p1.activeIcon = charGrid[p1.activeX, p1.activeY];

        Vector2 Curpos = p1.activeIcon.transform.localPosition;
        Curpos = Curpos + new Vector2(IconCanvas.transform.localPosition.x, 
            IconCanvas.transform.localPosition.y);

    }

    private void IconPreview(PlayerInterface p1)
    {
        if (p1.previewIcon != p1.activeIcon)
        {
            if (p1.createdCharacter != null)
            {
                Destroy(p1.createdCharacter);
            }

            GameObject go = Instantiate(returnCharID(p1.activeIcon.characterId).prefab,
                p1.charVisPos.position, Quaternion.identity) as GameObject;

            p1.createdCharacter = go;
            p1.previewIcon = p1.activeIcon;

            if (!string.Equals(p1.playerBase.playerId, players[0].playerId))
            {
                p1.createdCharacter.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    public CharacterPrefab returnCharID(string id)
    {
        CharacterPrefab retVal = null;

        for (int i = 0; i < characterList.Count; i++)
        {
            if (string.Equals(characterList[i].charId, id))
            {
                retVal = characterList[i];
                break;
            }
        }

        return retVal;
    }

    public void LoadBattle()
    {
        SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Single);
    }

    public static CharacterSelection instance;

    public static CharacterSelection GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}

[System.Serializable]
public class PlayerBase
{
    public string playerId;
    public string inputId;
    public GameObject playChar;
    public bool charSelected;
    
}

[System.Serializable]
public class CharacterPrefab
{
    public string charId;
    public GameObject prefab;
}

[System.Serializable]
public class PlayerInterface
{
    public IconInfo activeIcon;
    public IconInfo previewIcon;
    public GameObject Cursor;
    public Transform charVisPos;
    public GameObject createdCharacter;

    public int activeX;
    public int activeY;

    public bool hitEnter;
    public PlayerBase playerBase;

}


