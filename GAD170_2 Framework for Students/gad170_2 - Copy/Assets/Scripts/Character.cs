using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a single character, their stats, name and current mojo (or hp) in the dance battle.
/// 
/// TODO:
///     Initialise stats for the character that will be used to determine their victory in dance offs.
///     You may wish or need to add additional stats here for use in the fight (FightManager)
///     May need to handle the loss of mojo when loosing a fight
/// </summary>
public class Character : MonoBehaviour
{
    public CharacterName charName;

    [Range(0.0f,1.0f)]
    public float mojoRemaining = 1;

    [Header("Stats")]
    public int availablePoints = 10;

    public int level;
    public int xp;
    public int style, luck, rhythm;


    [Header("Related objects")]
    public DanceTeam myTeam;

    public bool isSelected;

    [SerializeField]
    protected TMPro.TextMeshPro nickText;


    void Awake()
    {
        InitialStats();
    }

    public void InitialStats()
    {

        // all character start at level 1
        level = 1;


        //Start at 0 XP points
        xp = 0;

        // all characters will start with a number of 1 - 6 
        rhythm = Random.Range(1, 6);
        luck = Random.Range(1, 6);
        style = Random.Range(1, 6);


        Debug.Log(rhythm);
        Debug.Log(style);
        Debug.Log(luck);
        Debug.Log(xp);

        // TODO - First, you can
        Debug.LogWarning("InitialStats called, needs to distribute points into stats. This should be able to be ported from previous brief work");
    }

    public void AssignName(CharacterName characterName)
    {
        charName = characterName;
        if(nickText != null)
        {
            nickText.text = charName.nickname;
            nickText.transform.LookAt(Camera.main.transform.position);
            //text faces the wrong way so
            nickText.transform.Rotate(0, 180, 0);
        }
    }
}
