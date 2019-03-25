using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The BattlesSystem handles the organisation of rounds, selecting the dancers to dance off from each side.
/// It then hands off to the fightManager to determine the outcome of 2 dancers dance off'ing.
/// 
/// TODO:
///     Needs to hand the request for a dance off battle round by selecting a dancer from each side and 
///         handing off to the fight manager, via GameEvents.RequestFight
///     Needs to handle GameEvents.OnFightComplete so that a new round can start
///     Needs to handle a team winning or loosing
///     This may be where characters are set as selected when they are in a dance off and when they leave the dance off
/// </summary>
public class BattleSystem : MonoBehaviour
{
    public DanceTeam TeamA,TeamB;

    public float battlePrepTime = 2;
    public float fightWinTime = 2;
    public Character TeamAFighters;
    public Character TeamBFighters;

    private void OnEnable()
    {
        GameEvents.OnRequestFighters += RoundRequested;
        GameEvents.OnFightComplete += FightOver;
    }

    private void OnDisable()
    {
        GameEvents.OnRequestFighters -= RoundRequested;
        GameEvents.OnFightComplete -= FightOver;
    }

    void RoundRequested()
    {
        //calling the coroutine so we can put waits in for anims to play
        StartCoroutine(DoRound());
    }
    //messing with doround
    IEnumerator DoRound()
    {
        yield return new WaitForSeconds(battlePrepTime);

        //checking for no dancers on either team

        if (TeamA.activeDancers.Count > 0 && TeamB.activeDancers.Count > 0)
        {
            Debug.LogWarning("DoRound called, it needs to select a dancer from each team to dance off and put in the FightEventData below");

            int GetTeamAFirstNameIndex = Random.Range(0, TeamA.activeDancers.Count);
            int GetTeamBFirstNameIndex = Random.Range(0, TeamB.activeDancers.Count);

            TeamAFighters = TeamA.activeDancers[GetTeamAFirstNameIndex];
            TeamBFighters = TeamB.activeDancers[GetTeamBFirstNameIndex];
            GameEvents.RequestFight(new FightEventData(TeamAFighters, TeamBFighters));
       



            //TODO pick a dancer from Team A and a dancer from Team B
            // Use the Line of code below to start the fight
            // a and b should be replaced with variables you create
            // to hold the dancer from each team
            //

        }
        else
        {
            //TODO: Work out who the winning team is
            //HINT: you have access to the number of dancers in each team
            // TeamA.activedancers >= 0;
            // TeamB.activedancers >= 0;
            //GameEvents.BattleFinished(winner);
            //winner.EnableWinEffects();
            if (TeamA.activeDancers.Count >= 0)
            {
                GameEvents.BattleFinished(TeamB);
                TeamB.EnableWinEffects();
                Debug.Log("Team B IS THE WINNER");
            }

            else if (TeamB.activeDancers.Count >= 0)
            {
                GameEvents.BattleFinished(TeamA);
                TeamA.EnableWinEffects();
                Debug.Log("Team A IS THE WINNER");
            }

            //log it battlelog also
            Debug.Log("DoRound called, but we have a winner so Game Over");
        }
    }


    //messing with fightover
    void FightOver(FightResultData data)
    {
        Debug.LogWarning("FightOver called, may need to check for winners and/or notify teams of zero mojo dancers");

        // TODO - CHeck if the Battle is over
        // ONLY do wthe win effects and remove from active
        // if the outcome is valid (ie. not for a draw)
        Debug.Log(data.outcome);
        if (data.outcome <= 0)
        {

            //play the win/lose efects
            data.winner.myTeam.EnableWinEffects();
            //remove the defated character
            data.defeated.myTeam.RemoveFromActive(data.defeated);
        }


        //defaulting to starting a new round to ease development
        //calling the coroutine so we can put waits in for anims to play
        StartCoroutine(HandleFightOver());
    }

    IEnumerator HandleFightOver()
    {
        yield return new WaitForSeconds(fightWinTime);
        TeamA.DisableWinEffects();
        TeamB.DisableWinEffects();
        Debug.LogWarning("HandleFightOver called, may need to prepare or clean dancers or teams and checks before doing GameEvents.RequestFighters()");
        GameEvents.RequestFighters();
    }
}
