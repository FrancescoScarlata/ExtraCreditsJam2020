using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public Member[] familyMembers;

    [SerializeField] GameObject gameOver1;
    [SerializeField] GameObject gameOver2;
    [SerializeField] GameObject gameOver3;

    int membersDead = 0;

    private void Start()
    {
        for(int i = 0; i < familyMembers.Length; i++)
        {
            if(familyMembers[i].myState == MemberState.dead)
            {
                membersDead++;
            }
        }

        if(membersDead == 0)
        {
            gameOver1.SetActive(true);
        }
        else if(membersDead > 0 && membersDead < 4)
        {
            gameOver2.SetActive(true);
        }
        else
        {
            gameOver3.SetActive(true);
        }
    }
}
