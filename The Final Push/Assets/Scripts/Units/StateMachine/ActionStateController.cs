using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ActionStateController : MonoBehaviour
{
    // Three button canvases.
    Canvas startCv;
    Canvas attackCv;
    Canvas magicCv;

    // Canvas that tells the player what to do after they selected an action.
    Canvas promptCv;

    // Canvas that tells the player information about the Unit
    Canvas infoCv;

    // All the buttons that we will need.
    Button meleeBtn;
    Button rangeBtn;
    Button magicAtkBtn;
    Button magicHealBtn;
    Button endTurnBtn;
    
    Button confirmActionBtn;

    // Scripts for the unit that this script is attatched to.
    // One of these will not be used, depending on whether the GO script is attatched to is a General or not.
    UnitInfo PlayerUI;
    GeneralInfo PlayerGI;

    // Scripts for enemies that are attacked. Will be assigned on an as-needed basis.
    UnitInfo EnemyUI;
    GeneralInfo EnemyGI;
    BuildingInfo EnemyBI;

    // This script is for both sides, which means that we will need the EnemyAI script for the enemies turn.
    EnemyAI enemyAI;

    // Range Indicators, my lazy solution to not using the GeneratePathTo function in TileMap.
    public GameObject meleeRI;
    public GameObject rangeRI;
    public GameObject magicAtkRI;
    public GameObject magicHealRI;
    GameObject currentRI;

    // Referance to the state controller, so we can ship out the END state when we're done.
    StateController sc;

    // Need a reference to this to ship out usedMagic if we used magic this turn.
    TroopCombatCanvasStartController tccsc;

    // Reference to the map to access the GeneratePathTo function.
    TileMap map;

    // General 4's special variable. Will not be touched except by General 4, and used in the Magic functions.
    public bool generalFourMagic;

    // Int that tells the Update void what action we are doing.
    public int whichAction;

    // Need this to determine critical hits
    int critChance;
    bool crit;

    void Start()
    {
        if (this.gameObject.tag == "PlayerUnit" || this.gameObject.tag == "EnemyUnit")
        {
            // If this script is attatched to a troop unit, get the troop unit info
            PlayerUI = GetComponent<UnitInfo>();
        }
        else if (this.gameObject.tag == "PlayerGeneral" || this.gameObject.tag == "EnemyGeneral")
        {
            // If this script is attatched to a general, get the general info
            PlayerGI = GetComponent<GeneralInfo>();

            // If the General is General 4, then add a special magic variable
            if (this.PlayerGI.generalType == 4)
            {
                generalFourMagic = true;
            }
            else
            {
                generalFourMagic = false;
            }
        }
        else
        {
            Debug.LogWarning("An ActionStateController is attatched to an object with a null tag");
        }

        // If the tag says that it's an enemy, we'll need the EnemyAI script variables to work
        if (this.gameObject.tag == "EnemyUnit" || this.gameObject.tag == "EnemyGeneral")
        {
            enemyAI = GetComponent<EnemyAI>();
        }

        // Declaring Variables
        // Canvases
        startCv = GameObject.Find("TroopCombatCanvas-Start").GetComponent<Canvas>();
        attackCv = GameObject.Find("TroopCombatCanvas-Start-Attack").GetComponent<Canvas>();
        magicCv = GameObject.Find("TroopCombatCanvas-Start-Magic").GetComponent<Canvas>();

        promptCv = GameObject.Find("TroopCombatCanvas-ActionPrompt").GetComponent<Canvas>();

        infoCv = GameObject.Find("InfoDisplayCanvas").GetComponent<Canvas>();

        // Buttons
        endTurnBtn = startCv.transform.GetChild(2).GetComponent<Button>();
        meleeBtn = attackCv.transform.GetChild(0).GetComponent<Button>();
        rangeBtn = attackCv.transform.GetChild(1).GetComponent<Button>();
        magicAtkBtn = magicCv.transform.GetChild(0).GetComponent<Button>();
        magicHealBtn = magicCv.transform.GetChild(1).GetComponent<Button>();

        confirmActionBtn = promptCv.transform.GetChild(2).GetComponent<Button>();

        sc = GetComponent<StateController>();

        tccsc = startCv.GetComponent<TroopCombatCanvasStartController>();

        map = GameObject.FindWithTag("Map").GetComponent<TileMap>();

        whichAction = 0;
    }

    void Update()
    {
        // Testing implementing this code in ClickableTile. Saving it here in case it doesn't work, but it's probably easier there.
        // Checks to make sure that we want to recieve a world click point, which is when an action is active.
        if (whichAction == 0)
        {
            return;
        }

        // Click function, checks if the mouse is down and if it is over a UI element, which we don't want.
        /*if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Action is in progress and click detected");
            
        }*/

        // Back function. Basically rewinds everything.
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(currentRI);

            promptCv.enabled = false;

            if (whichAction == 1 || whichAction == 2)
            {
                attackCv.enabled = true;
            }
            else if (whichAction == 3 || whichAction == 4)
            {
                magicCv.enabled = true;
            }
            else
            {
                Debug.LogWarning("ActionStateController go back error, whichAction was not assigned properly");
            }

            whichAction = 0;
        }

        // If the player is targeting the wrong object, the confirm button will not be enabled
        if (promptCv.enabled)
        {
            if (map.selectedUnit)
            {
                if (whichAction == 4)
                {
                    if (map.selectedUnit.tag == "PlayerUnit" || map.selectedUnit.tag == "PlayerGeneral")
                    {
                        confirmActionBtn.interactable = true;
                    }
                    else
                    {
                        confirmActionBtn.interactable = false;
                    } 
                }
                else
                {
                    if (map.selectedUnit.tag == "EnemyUnit" || map.selectedUnit.tag == "EnemyGeneral")
                    {
                        confirmActionBtn.interactable = true;
                    }
                    else
                    {
                        confirmActionBtn.interactable = false;
                    }
                }
            }
        }
    }

    public void ActionTriggerUnit()
    {
        startCv.enabled = true;
        
        // Button Events
        meleeBtn.onClick.AddListener(StartUnitMelee);
        rangeBtn.onClick.AddListener(StartUnitRanged);
        magicAtkBtn.onClick.AddListener(StartUnitMagicAttack);
        magicHealBtn.onClick.AddListener(StartUnitMagicHeal);
        endTurnBtn.onClick.AddListener(EndAction);
    }

    public void StartUnitMelee()
    {
        attackCv.enabled = false;
        currentRI = Instantiate(meleeRI, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        whichAction = 1;
        SetConfirmListener();
    }

    public void StartUnitRanged()
    {
        attackCv.enabled = false;
        currentRI = Instantiate(rangeRI, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        whichAction = 2;
        SetConfirmListener();
    }

    public void StartUnitMagicAttack()
    {
        magicCv.enabled = false;
        currentRI = Instantiate(magicAtkRI, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        whichAction = 3;
        SetConfirmListener();
    }

    public void StartUnitMagicHeal()
    {
        magicCv.enabled = false;
        currentRI = Instantiate(magicHealRI, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        whichAction = 4;
        SetConfirmListener();
    }

    public void SetConfirmListener()
    {
        promptCv.enabled = true;

        if (whichAction == 1)
        {
            confirmActionBtn.onClick.AddListener(PlayerExecuteMelee);
        }
        else if (whichAction == 2)
        {
            confirmActionBtn.onClick.AddListener(PlayerExecuteRange);
        }
        else if (whichAction == 3)
        {
            confirmActionBtn.onClick.AddListener(ExecuteMagicAtk);
        }
        else if (whichAction == 4)
        {
            confirmActionBtn.onClick.AddListener(ExecuteMagicHeal);
        }
        else
        {
            Debug.LogError("SetConfirmListener error: whichAction not transfered properly");
        }
    }

    public void PlayerExecuteMelee()
    {
        critChance = Random.Range(1, 5);

        if (critChance == 1)
        {
            crit = true;
        }
        else
        {
            crit = false;
        }

        if (map.selectedUnit.tag == "EnemyUnit")
        {
            EnemyUI = map.selectedUnit.GetComponent<UnitInfo>();
            if (this.tag == "PlayerUnit")
            {
                EnemyUI.curHealth -= PlayerUI.atkPower;
                if (crit)
                {
                    EnemyUI.curHealth -= PlayerUI.atkPower;
                }
            }
            else
            {
                EnemyUI.curHealth -= PlayerGI.atkPower;
                if (crit)
                {
                    EnemyUI.curHealth -= PlayerGI.atkPower;
                }
            }
        }
        else if (map.selectedUnit.tag == "EnemyGeneral")
        {
            EnemyGI = map.selectedUnit.GetComponent<GeneralInfo>();
            if (this.tag == "PlayerUnit")
            {
                EnemyGI.curHealth -= PlayerUI.atkPower;
                if (crit)
                {
                    EnemyGI.curHealth -= PlayerUI.atkPower;
                }
            }
            else
            {
                EnemyGI.curHealth -= PlayerGI.atkPower;
                if (crit)
                {
                    EnemyGI.curHealth -= PlayerGI.atkPower;
                }
            }
        }
        else
        {
            EnemyBI = map.selectedUnit.GetComponent<BuildingInfo>();
            if (this.tag == "PlayerUnit")
            {
                EnemyBI.curHealth -= PlayerUI.atkPower;
                if (crit)
                {
                    EnemyBI.curHealth -= PlayerUI.atkPower;
                }
            }
            else
            {
                EnemyBI.curHealth -= PlayerGI.atkPower;
                if (crit)
                {
                    EnemyBI.curHealth -= PlayerGI.atkPower;
                }
            }
        }

        promptCv.enabled = false;
        EndAction();
    }

    public void PlayerExecuteRange()
    {
        critChance = Random.Range(1, 8);

        if (critChance == 1)
        {
            crit = true;
        }
        else
        {
            crit = false;
        }

        if (map.selectedUnit.tag == "EnemyUnit")
        {
            EnemyUI = map.selectedUnit.GetComponent<UnitInfo>();
            if (this.tag == "PlayerUnit")
            {
                EnemyUI.curHealth -= PlayerUI.atkPower;
                if (crit)
                {
                    EnemyUI.curHealth -= PlayerUI.atkPower;
                }
            }
            else
            {
                EnemyUI.curHealth -= PlayerGI.atkPower;
                if (crit)
                {
                    EnemyUI.curHealth -= PlayerGI.atkPower;
                }
            }
        }
        else if (map.selectedUnit.tag == "EnemyGeneral")
        {
            EnemyGI = map.selectedUnit.GetComponent<GeneralInfo>();
            if (this.tag == "PlayerUnit")
            {
                EnemyGI.curHealth -= PlayerUI.atkPower;
                if (crit)
                {
                    EnemyGI.curHealth -= PlayerUI.atkPower;
                }
            }
            else
            {
                EnemyGI.curHealth -= PlayerGI.atkPower;
                if (crit)
                {
                    EnemyGI.curHealth -= PlayerGI.atkPower;
                }
            }
        }
        else
        {
            EnemyBI = map.selectedUnit.GetComponent<BuildingInfo>();
            if (this.tag == "PlayerUnit")
            {
                EnemyBI.curHealth -= PlayerUI.atkPower;
                if (crit)
                {
                    EnemyBI.curHealth -= PlayerUI.atkPower;
                }
            }
            else
            {
                EnemyBI.curHealth -= PlayerGI.atkPower;
                if (crit)
                {
                    EnemyBI.curHealth -= PlayerGI.atkPower;
                }
            }
        }

        promptCv.enabled = false;
        EndAction();
    }

    public void ExecuteMagicAtk()
    {
        if (map.selectedUnit.tag == "EnemyUnit")
        {
            EnemyUI = map.selectedUnit.GetComponent<UnitInfo>();
            if (this.tag == "PlayerUnit")
            {
                EnemyUI.curHealth -= PlayerUI.atkPower;
            }
            else
            {
                EnemyUI.curHealth -= PlayerGI.atkPower;
            }
        }
        else if (map.selectedUnit.tag == "EnemyGeneral")
        {
            EnemyGI = map.selectedUnit.GetComponent<GeneralInfo>();
            if (this.tag == "PlayerUnit")
            {
                EnemyGI.curHealth -= PlayerUI.atkPower;
            }
            else
            {
                EnemyGI.curHealth -= PlayerGI.atkPower;
            }
        }
        else
        {
            EnemyBI = map.selectedUnit.GetComponent<BuildingInfo>();
            if (this.tag == "PlayerUnit")
            {
                EnemyBI.curHealth -= PlayerUI.atkPower;
            }
            else
            {
                EnemyBI.curHealth -= PlayerGI.atkPower;
            }
        }

        promptCv.enabled = false;

        if (generalFourMagic)
        {
            // One way bool lock to ensure that this boolean only triggers once
            generalFourMagic = false;
        }
        else
        {
            tccsc.usedMagic = true;
        }

        EndAction();
    }

    // Healing fudges the rules to call the selected (friendly) unit an enemy, but I don't want to make another variable
    public void ExecuteMagicHeal()
    {
        if (map.selectedUnit.tag == "PlayerUnit")
        {
            EnemyUI = map.selectedUnit.GetComponent<UnitInfo>();
            if (this.tag == "PlayerUnit")
            {
                EnemyUI.curHealth += PlayerUI.atkPower;
            }
            else
            {
                EnemyUI.curHealth += PlayerGI.atkPower;
            }
        }
        else
        {
            EnemyGI = map.selectedUnit.GetComponent<GeneralInfo>();
            if (this.tag == "PlayerUnit")
            {
                EnemyGI.curHealth += PlayerUI.atkPower;
            }
            else
            {
                EnemyGI.curHealth += PlayerGI.atkPower;
            }
        }

        promptCv.enabled = false;

        if (generalFourMagic)
        {
            // One way bool lock to ensure that this boolean only triggers once
            generalFourMagic = false;
        }
        else
        {
            tccsc.usedMagic = true;
        }

        EndAction();
    }

    // These next two public voids are for the enemies: the player should not be able to activate them.
    public void EnemyMeleeAttack(Unit target)
    {
        critChance = Random.Range(1, 10);

        if (critChance == 1)
        {
            crit = true;
        }
        else
        {
            crit = false;
        }

        if (target.gameObject.tag == "PlayerUnit")
        {
            EnemyUI = target.gameObject.GetComponent<UnitInfo>();
            if (this.tag == "EnemyUnit")
            {
                EnemyUI.curHealth -= PlayerUI.atkPower;
                if (crit)
                {
                    EnemyUI.curHealth -= PlayerUI.atkPower;
                }
            }
            else
            {
                EnemyUI.curHealth -= PlayerGI.atkPower;
                if (crit)
                {
                    EnemyUI.curHealth -= PlayerGI.atkPower;
                }
            }
        }
        else if (target.gameObject.tag == "PlayerGeneral")
        {
            EnemyGI = target.gameObject.GetComponent<GeneralInfo>();
            if (this.tag == "EnemyUnit")
            {
                EnemyGI.curHealth -= PlayerUI.atkPower;
                if (crit)
                {
                    EnemyGI.curHealth -= PlayerUI.atkPower;
                }
            }
            else
            {
                EnemyGI.curHealth -= PlayerGI.atkPower;
                if (crit)
                {
                    EnemyGI.curHealth -= PlayerGI.atkPower;
                }
            }
        }
        else
        {
            Debug.Log("ASC: EnemyMeleeAttack targeting error, tag not found");
        }
    }

    public void EnemyRangedAttack(Unit target)
    {
        if (target.gameObject.tag == "PlayerUnit")
        {
            EnemyUI = target.gameObject.GetComponent<UnitInfo>();
            if (this.tag == "EnemyUnit")
            {
                EnemyUI.curHealth -= PlayerUI.atkPower;
            }
            else
            {
                EnemyUI.curHealth -= PlayerGI.atkPower;
            }
        }
        else if (target.gameObject.tag == "PlayerGeneral")
        {
            EnemyGI = target.gameObject.GetComponent<GeneralInfo>();
            if (this.tag == "EnemyUnit")
            {
                EnemyGI.curHealth -= PlayerUI.atkPower;
            }
            else
            {
                EnemyGI.curHealth -= PlayerGI.atkPower;
            }
        }
        else
        {
            Debug.Log("ASC: EnemyRangeAttack targeting error, tag not found");
        }
    }

    public void EndAction()
    {
        // Clean up the button listeners so they don't activate the unit again
        meleeBtn.onClick.RemoveListener(StartUnitMelee);
        rangeBtn.onClick.RemoveListener(StartUnitRanged);
        magicAtkBtn.onClick.RemoveListener(StartUnitMagicAttack);
        magicHealBtn.onClick.RemoveListener(StartUnitMagicHeal);
        endTurnBtn.onClick.RemoveListener(EndAction);

        // Reseting variables and setting the unit's state to END
        infoCv.enabled = false;
        whichAction = 0;
        promptCv.enabled = false;
        sc.state = UnitState.END;
    }
}
