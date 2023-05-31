using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionStateController : MonoBehaviour
{
    Canvas startCv;
    Canvas attackCv;
    Canvas magicCv;

    Button meleeBtn;
    Button rangeBtn;
    Button magicAtkBtn;
    Button magicHealBtn;

    UnitInfo PlayerUI;
    GeneralInfo PlayerGI;

    UnitInfo EnemyUI;
    GeneralInfo EnemyGI;

    StateController sc;

    void Start()
    {
        if (this.gameObject.tag == "PlayerUnit")
        {
            // If this script is attatched to a troop unit, get the troop unit canvas
            
            // Canvases
            startCv = GameObject.Find("TroopCombatCanvas-Start").GetComponent<Canvas>();
            attackCv = GameObject.Find("TroopCombatCanvas-Start-Attack").GetComponent<Canvas>();
            magicCv = GameObject.Find("TroopCombatCanvas-Start-Magic").GetComponent<Canvas>();

            // Buttons
            meleeBtn = attackCv.transform.GetChild(0).GetComponent<Button>();
            rangeBtn = attackCv.transform.GetChild(1).GetComponent<Button>();
            magicAtkBtn = magicCv.transform.GetChild(0).GetComponent<Button>();
            magicHealBtn = magicCv.transform.GetChild(1).GetComponent<Button>();
        }
        else if (this.gameObject.tag == "PlayerGeneral")
        {
            // If this script is attatched to a general, get the general canvas

            // Canvases
            startCv = GameObject.Find("GeneralCombatCanvas-Start").GetComponent<Canvas>();
            attackCv = GameObject.Find("GeneralCombatCanvas-Start-Attack").GetComponent<Canvas>();
            magicCv = GameObject.Find("GeneralCombatCanvas-Start-Magic").GetComponent<Canvas>();

            // Buttons
            meleeBtn = attackCv.transform.GetChild(0).GetComponent<Button>();
            rangeBtn = attackCv.transform.GetChild(1).GetComponent<Button>();
            magicAtkBtn = magicCv.transform.GetChild(0).GetComponent<Button>();
            magicHealBtn = magicCv.transform.GetChild(1).GetComponent<Button>();
        }

        sc = GetComponent<StateController>();
    }

    public void ActionTriggerUnit(GameObject unit)
    {
        startCv.enabled = true;
        PlayerUI = unit.GetComponent<UnitInfo>();
        
        // Button Events
        meleeBtn.onClick.AddListener(UnitMelee);
        rangeBtn.onClick.AddListener(UnitRanged);
        magicAtkBtn.onClick.AddListener(UnitMagicAttack);
        magicHealBtn.onClick.AddListener(UnitMagicHeal);
    }

    public void ActionTriggerGeneral(GameObject unit)
    {
        startCv.enabled = true;
        PlayerGI = unit.GetComponent<GeneralInfo>();

        // Buttons Events
        meleeBtn.onClick.AddListener(GeneralMelee);
        rangeBtn.onClick.AddListener(GeneralRanged);
        magicAtkBtn.onClick.AddListener(GeneralMagicAttack);
        magicHealBtn.onClick.AddListener(GeneralMagicHeal);
    }

    public void UnitMelee()
    {

        EndAction();
    }

    public void UnitRanged()
    {
        EndAction();
    }

    public void UnitMagicAttack()
    {
        EndAction();
    }

    public void UnitMagicHeal()
    {
        EndAction();
    }

    public void GeneralMelee()
    {
        EndAction();
    }

    public void GeneralRanged()
    {
        EndAction();
    }

    public void GeneralMagicAttack()
    {
        EndAction();
    }

    public void GeneralMagicHeal()
    {
        EndAction();
    }

    public void EndAction()
    {
        sc.state = UnitState.END;
    }
}
