using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the outcome of a dance off between 2 dancers, determines the strength of the victory form -1 to 1
/// 
/// TODO:
///     Handle GameEvents.OnFightRequested, resolve based on stats and respond with GameEvents.FightCompleted
///         This will require a winner and defeated in the fight to be determined.
///         This may be where characters are set as selected when they are in a dance off and when they leave the dance off
///         This may also be where you use the BattleLog to output the status of fights
///     This may also be where characters suffer mojo (hp) loss when they are defeated
/// </summary>
/// 

    // BattleHandler stuff 





public class FightManager : MonoBehaviour
{
    public Color drawCol = Color.gray;

    public float fightAnimTime = 2;

    private void OnEnable()
    {
        GameEvents.OnFightRequested += Fight;
    }

    private void OnDisable()
    {
        GameEvents.OnFightRequested -= Fight;
    }

    public void Fight(FightEventData data)
    {
        StartCoroutine(Attack(data.lhs, data.rhs));
    }

    IEnumerator Attack(Character lhs, Character rhs)
    {
        lhs.isSelected = true;
        rhs.isSelected = true;
        lhs.GetComponent<AnimationController>().Dance();
        rhs.GetComponent<AnimationController>().Dance();

        yield return new WaitForSeconds(fightAnimTime);

        float outcome = 0;
        Character winner = lhs, defeated = rhs;
        //Do this one second!
        //can re-use code from battle n battlehandler

        // Debug log will show the player stats and NPC

        Debug.Log("The NPC Rhythm stats is " + lhs.rhythm);
        Debug.Log("The NPC Style stats is " + lhs.style);
        Debug.Log("The NPC Luck stats is " + lhs.luck);
        Debug.Log("The Player Style stats is " + rhs.style); //combine them add or times them
        Debug.Log("The Player Luck stats is " + rhs.luck); //random role
        Debug.Log("The Player Rhythm stats is " + rhs.rhythm); // combine them add or times them


        // if the player has a higher style then the NPC then the player will add 1 to the player_has_one score
        int lhs_has_won = 0;
        if (rhs.style >= lhs.style)
            lhs_has_won += 1;

        // if the player has a higher luck then the NPC then the player will add 1 to the player_has_one score
        if (rhs.luck >= lhs.luck)
            lhs_has_won += 1;

        // if the player has a higher rhythm then the NPC then the player will add 1 to the player_has_one score
        if (rhs.rhythm >= lhs.rhythm)
            lhs_has_won += 1;


        //the outcome of the battle
        if (lhs_has_won >= 2)
            outcome = 1;

        if (outcome == 1)
        {
            winner = lhs;
            defeated = rhs;
        }
        else
        {
            winner = rhs;
            defeated = lhs;

        }
        // debug the left hand side character stats
        Debug.Log(lhs.rhythm);
        Debug.Log(lhs.style);
        Debug.Log(lhs.luck);
        Debug.Log(rhs.rhythm);
        Debug.Log(rhs.style);
        Debug.Log(rhs.luck);



        //somewhere here you are setting outcome
        //todo: when you set outcome
        // also set winner & defeated


        //example
        //outcome = 1;
        //winner = lhs;
        //defeated = rhs;

        //defaulting to draw 
     
        Debug.LogWarning("Attack called, needs to use character stats to determine winner with win strength from 1 to -1. This can most likely be ported from previous brief work.");


        Debug.LogWarning("Attack called, may want to use the BattleLog to report the dancers and the outcome of their dance off.");

        var results = new FightResultData(winner, defeated, outcome);

        lhs.isSelected = false;
        rhs.isSelected = false;
        GameEvents.FightCompleted(results);

        yield return null;
    }
}
