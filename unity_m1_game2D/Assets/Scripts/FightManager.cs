using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    private GameObject canvas;

    private int movenumber;
    private int moveselect;
    private int turn;

    [Header("De Player en Boss gameobjects")]
    public GameObject player;
    public GameObject boss;
    private Animator playeranimator;
    private Animator bossanimator;
    [Space(20)]


    [Header("Animatie Trigger woorden")]
    [Space(5)]
    public string[] Karakter_Animatie_Attack_TriggerWoord;
    public string[] Karakter_Animatie_Damage_TriggerWoord;
    public float karakteraanvalreactietijd = 0.5f;
    [Space(10)]
    public string[] Boss_Animatie_Attack_Triggerwoord;
    public string[] Boss_Animatie_Damage_TriggerWoord;
    public float bossaanvalreactietijd = 2f;

    private IEnumerator routine; 

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("UI");
        playeranimator = player.GetComponent<Animator>();
        bossanimator = boss.GetComponent<Animator>();
        CheckAssets();
    }

    void CheckAssets()
    {
        if (canvas == null)
        {
            Debug.LogError("er is geen canvas aanwezig. maak deze opnieuw aan ");
        }
        if (player == null)
        {
            Debug.LogError("je hebt geen karakter object in de 'player' slot gezet. zet de game uit en voeg deze toe voordat je verder gaat");
        }
        if (playeranimator == null)
        {
            Debug.LogError("je 'player' karakter heeft geen animator object. zet de game uit en voeg deze toe voordat je verder gaat");
        }
        if (boss == null)
        {
            Debug.LogError("je hebt geen eindbaas object in het 'Boss' slot gezet. zet de game uit en voeg deze toe voordat je verder gaat");
        }
        if (bossanimator == null)
        {
            Debug.LogError("je 'boss' karakter heeft geen animator object. zet de game uit en voeg deze toe voordat je verder gaat");
        }
    }

    public void moveselected(int attacknumber)
    {
        if (Karakter_Animatie_Attack_TriggerWoord.Length > attacknumber)
        {
            if(Karakter_Animatie_Attack_TriggerWoord[attacknumber] != "")
            {
                moveselect = attacknumber;
                attackorganizer();
            }
            else
            {
                Debug.LogError("deze knop heeft geen animatie triggerwoord");
            }
        }
        else
        {
            Debug.LogError("deze knop heeft geen animatie triggerwoord");
        }
    }

    void attackorganizer()
    {
        movenumber++;
        if(movenumber > 3)
        {
            movenumber = 1;
        }

        switch (movenumber)
        {
            case 1:
                Debug.Log("ronde 1");
                canvas.SetActive(false);
                turn = Random.Range(1, 3);
                if(turn == 1)
                {
                    routine = PlayerAttack();
                }
                else
                {
                    routine = BossAttack();
                }
                StartCoroutine(routine);
                break;
            case 2:
                Debug.Log("ronde 2");
                if (turn == 1)
                {
                    routine = BossAttack();
                }
                else
                {
                    routine = PlayerAttack();
                }
                StartCoroutine(routine);
                break;
            case 3:
                Debug.Log("einde gevecht");
                canvas.SetActive(true);
                break;
        }
    }

    int CheckBossAttacks()
    {
        if (Boss_Animatie_Attack_Triggerwoord.Length >= 1)
        {
            int randomnumber = Random.Range(0, Boss_Animatie_Attack_Triggerwoord.Length);
            if(Boss_Animatie_Attack_Triggerwoord[randomnumber] == "")
            {
                Debug.LogError("de aanval animatie trigger van de boss is leeg");
                return randomnumber;
            }
            else
            {
                return randomnumber;
            }
        }
        else
        {
            Debug.LogError("je hebt geen animatie trigger woorden genoteerd voor de Boss aanvallen");
            return 0;
        }
    }

    int CheckBossDefence()
    {
        if (Boss_Animatie_Damage_TriggerWoord.Length >= 1)
        {
            int randomnumber = Random.Range(0, Boss_Animatie_Damage_TriggerWoord.Length);
            if (Boss_Animatie_Damage_TriggerWoord[randomnumber] == "")
            {
                Debug.LogError("de damage animatie trigger van de boss is leeg");
                return randomnumber;
            }
            else
            {
                return randomnumber;
            }
        }
        else
        {
            Debug.LogError("je hebt geen animatie trigger woorden genoteerd voor de Boss damage");
            return 0;
        }
    }

    int CheckCharacterDefence()
    {
        if (Karakter_Animatie_Damage_TriggerWoord.Length >= 1)
        {
            int randomnumber = Random.Range(0, Karakter_Animatie_Damage_TriggerWoord.Length);
            if (Karakter_Animatie_Damage_TriggerWoord[randomnumber] == "")
            {
                Debug.LogError("de damage animatie trigger van de boss is leeg");
                return randomnumber;
            }
            else
            {
                return randomnumber;
            }
        }
        else
        {
            Debug.LogError("je hebt geen animatie trigger woorden genoteerd voor de Boss damage");
            return 0;
        }
    }

    private IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);

        playeranimator.SetTrigger(Karakter_Animatie_Attack_TriggerWoord[moveselect]);
        yield return new WaitForSeconds(karakteraanvalreactietijd);

        bossanimator.SetTrigger(Boss_Animatie_Damage_TriggerWoord[CheckBossDefence()]);
        yield return new WaitForSeconds(bossaanvalreactietijd);
        yield return new WaitForSeconds(1f);
        attackorganizer();
    }

    private IEnumerator BossAttack()
    {
        yield return new WaitForSeconds(1f);

        bossanimator.SetTrigger(Boss_Animatie_Attack_Triggerwoord[CheckBossAttacks()]);
        yield return new WaitForSeconds(bossaanvalreactietijd);


        playeranimator.SetTrigger(Karakter_Animatie_Damage_TriggerWoord[CheckCharacterDefence()]);
        yield return new WaitForSeconds(karakteraanvalreactietijd);
        yield return new WaitForSeconds(1f);
        attackorganizer();
    }
}
