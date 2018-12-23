using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEditor;
using System;

[AddComponentMenu("Radial Menu Framework/RMF Element")]
public class RMF_RadialMenuElement : MonoBehaviour {

    [HideInInspector]
    public RectTransform rt;
    [HideInInspector]
    public RMF_RadialMenu parentRM;

    [Tooltip("Each radial element needs a button. This is generally a child one level below this primary radial element game object.")]
    public Button button;

    [Tooltip("This is the name of affected property when pushing this button")]
    public string label;

    [HideInInspector]
    public float angleMin, angleMax;

    [HideInInspector]
    public float angleOffset;

    [HideInInspector]
    public bool active = false;

    [HideInInspector]
    public int assignedIndex = 0;
    // Use this for initialization

    private CanvasGroup cg;
    
    //================================================
    //custom entered code - Dylan McAdam
    //================================================

    public bool _usingToolTips;
    public Text _toolTipText;
    public GameObject _toolTipBox;
    
    public RadialMenuController _rmc;

    //================================================
    //end of custom entered code
    //================================================

    private void OnEnable()
    {

        if (_usingToolTips == true)
        {
            UpdateButton();
        }

    }

    private void Awake() {

        rt = gameObject.GetComponent<RectTransform>();

        if (gameObject.GetComponent<CanvasGroup>() == null)
            cg = gameObject.AddComponent<CanvasGroup>();
        else
            cg = gameObject.GetComponent<CanvasGroup>();


        if (rt == null)
            Debug.LogError("Radial Menu: Rect Transform for radial element " + gameObject.name + " could not be found. Please ensure this is an object parented to a canvas.");

        if (button == null)
            Debug.LogError("Radial Menu: No button attached to " + gameObject.name + "!");
        
    }

    private void Start () {

        rt.rotation = Quaternion.Euler(0, 0, -angleOffset); //Apply rotation determined by the parent radial menu.

        //If we're using lazy selection, we don't want our normal mouse-over effects interfering, so we turn raycasts off.
        if (parentRM.useLazySelection)
            cg.blocksRaycasts = false;
        else {

            //Otherwise, we have to do some magic with events to get the label stuff working on mouse-over.

            EventTrigger t;

            if (button.GetComponent<EventTrigger>() == null) {
                t = button.gameObject.AddComponent<EventTrigger>();
                t.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();
            } else
                t = button.GetComponent<EventTrigger>();



            EventTrigger.Entry enter = new EventTrigger.Entry();
            enter.eventID = EventTriggerType.PointerEnter;
            enter.callback.AddListener((eventData) => { setParentMenuLable(label); });


            EventTrigger.Entry exit = new EventTrigger.Entry();
            exit.eventID = EventTriggerType.PointerExit;
            exit.callback.AddListener((eventData) => { setParentMenuLable(""); });

            t.triggers.Add(enter);
            t.triggers.Add(exit);



        }

    }
	
    //Used by the parent radial menu to set up all the approprate angles. Affects master Z rotation and the active angles for lazy selection.
    public void setAllAngles(float offset, float baseOffset) {

        angleOffset = offset;
        angleMin = offset - (baseOffset / 2f);
        angleMax = offset + (baseOffset / 2f);

    }

    //Highlights this button. Unity's default button wasn't really meant to be controlled through code so event handlers are necessary here.
    //I would highly recommend not messing with this stuff unless you know what you're doing, if one event handler is wrong then the whole thing can break.
    public void highlightThisElement(PointerEventData p) {

        ExecuteEvents.Execute(button.gameObject, p, ExecuteEvents.selectHandler);
        active = true;
        setParentMenuLable(label);

        if (_usingToolTips == true)
        {
            LoadTooltipText();
            Transform button = this.gameObject.transform.GetChild(0);
            _toolTipBox.transform.position = button.position + button.TransformDirection(new Vector3(0, 30, 0));
            _toolTipBox.SetActive(true);
        }

    }

    //Sets the label of the parent menu. Is set to public so you can call this elsewhere if you need to show a special label for something.
    public void setParentMenuLable(string l) {

        if (parentRM.textLabel != null)
            parentRM.textLabel.text = l;


    }


    //Unhighlights the button, and if lazy selection is off, will reset the menu's label.
    public void unHighlightThisElement(PointerEventData p) {

        ExecuteEvents.Execute(button.gameObject, p, ExecuteEvents.deselectHandler);
        active = false;

        if (!parentRM.useLazySelection)
            setParentMenuLable(" ");

        if (_usingToolTips == true)
        {
            _toolTipBox.SetActive(false);
        }
    }

    //Just a quick little test you can run to ensure things are working properly.
    public void clickMeTest() {

        Debug.Log(assignedIndex);


    }

    

    private void LoadTooltipText()
    {
        switch (this.label)
        {
            //upgrades
            case "Health":
                _toolTipText.text = this.label + ": " + _rmc._hitTower.TowerHealth + "/" + _rmc._hitTower._maxHealth + " => " + 
                                    (_rmc._hitTower._maxHealth + _rmc._hitTower.GetHealthUpgrade()) + "\n" +
                                    "Level: " + _rmc._hitTower._healthLevel + " => " + (_rmc._hitTower._healthLevel + 1);
                break;
            case "Damage":
                _toolTipText.text = this.label + ": " + _rmc._hitTower.GetTowerDamage() + " => " +
                                    (_rmc._hitTower.GetTowerDamage() + _rmc._hitTower.GetDamageUpgrade()) + "\n" +
                                    "Level: " + _rmc._hitTower._damageLevel + " => " + (_rmc._hitTower._damageLevel + 1);
                break;
            case "Firerate":
                _toolTipText.text = this.label + ": " + _rmc._hitTower.GetTowerFireRate() + " => " +
                                    (_rmc._hitTower.GetTowerFireRate() - _rmc._hitTower.GetFirerateUpgrade()) + "\n" +
                                    "Level: " + _rmc._hitTower._fireRateLevel + " => " + (_rmc._hitTower._fireRateLevel + 1);
                break;
            case "Range":
                _toolTipText.text = this.label + ": " + _rmc._hitTower.GetComponent<SphereCollider>().radius + " => " +
                                    (_rmc._hitTower.GetComponent<SphereCollider>().radius + _rmc._hitTower.GetRadiusUpgrade()) + "\n" +
                                    "Level: " + _rmc._hitTower._rangeLevel + " => " + (_rmc._hitTower._rangeLevel + 1);
                break;
            //attack priorities
            case "First Enemy":
                _toolTipText.text = AttackChoiceUtils.GetDescription(AttackChoice.First);
                break;
            case "Last Enemy":
                _toolTipText.text = AttackChoiceUtils.GetDescription(AttackChoice.Last);
                break;
            case "Most Damage":
                _toolTipText.text = AttackChoiceUtils.GetDescription(AttackChoice.MostDamage);
                break;
            case "Least Damage":
                _toolTipText.text = AttackChoiceUtils.GetDescription(AttackChoice.LeastDamage);
                break;
            case "Most Health":
                _toolTipText.text = AttackChoiceUtils.GetDescription(AttackChoice.MostHealth);
                break;
            case "Least Health":
                _toolTipText.text = AttackChoiceUtils.GetDescription(AttackChoice.LeastHealth);
                break;
            default:
                break;
        }
    }


    public void UpdateButton()
    {
        string buttonText = "MAXED";
        Tower tower = _rmc._hitTower;

        switch (this.label)
        {
            //upgrades
            case "Health":
                
                if (tower._healthLevel < tower._maxUpgrades)
                    buttonText = tower._healthLevelCosts[tower._healthLevel].ToString() + " QE";
                else
                    this.button.interactable = false;

                break;
            case "Damage":
                
                if (tower._damageLevel < tower._maxUpgrades)
                    buttonText = tower._damageLevelCosts[tower._damageLevel].ToString() + " QE";
                else
                    this.button.interactable = false;

                break;
            case "Firerate":
                
                if (tower._fireRateLevel < tower._maxUpgrades)
                    buttonText = tower._fireRateLevelCosts[tower._fireRateLevel].ToString() + " QE";
                else
                    this.button.interactable = false;

                break;
            case "Range":
                
                if (tower._rangeLevel < tower._maxUpgrades)
                    buttonText = tower._rangeLevelCosts[tower._rangeLevel].ToString() + " QE";
                else
                    this.button.interactable = false;

                break;
            //attack priorities
            case "First Enemy":
                buttonText = AttackChoiceUtils.GetBaseText(AttackChoice.First);
                break;
            case "Last Enemy":
                buttonText = AttackChoiceUtils.GetBaseText(AttackChoice.Last);
                break;
            case "Most Damage":
                buttonText = AttackChoiceUtils.GetBaseText(AttackChoice.MostDamage);
                break;
            case "Least Damage":
                buttonText = AttackChoiceUtils.GetBaseText(AttackChoice.LeastDamage);
                break;
            case "Most Health":
                buttonText = AttackChoiceUtils.GetBaseText(AttackChoice.MostHealth);
                break;
            case "Least Health":
                buttonText = AttackChoiceUtils.GetBaseText(AttackChoice.LeastHealth);
                break;
            default:
                break;
        }

        this.button.transform.GetChild(0).GetComponent<Text>().text = buttonText;
        
    }


    public void ExecuteFunction()
    {
        Tower tower = _rmc._hitTower;

        switch (this.label)
        {
            //upgrades
            case "Health":

                //take away money
                _rmc._bank.MinusCredits(tower._healthLevelCosts[tower._healthLevel]);

                //set new stats
                tower.TowerHealth = tower.TowerHealth + tower.GetHealthUpgrade();

                tower._healthLevel++;

                break;
            case "Damage":

                //take away money
                _rmc._bank.MinusCredits(tower._damageLevelCosts[tower._damageLevel]);

                //set new stats
                tower.SetTowerDamage(tower.GetTowerDamage() + tower.GetDamageUpgrade());

                tower._damageLevel++;

                break;
            case "Firerate":

                //take away money
                _rmc._bank.MinusCredits(tower._fireRateLevelCosts[tower._fireRateLevel]);

                //set new stats
                if (tower.GetTowerFireRate() > 0.25f)
                {
                    tower.SetTowerFireRate(tower.GetTowerFireRate() - tower.GetFirerateUpgrade());
                }

                tower._fireRateLevel++;

                break;
            case "Range":

                //take away money
                _rmc._bank.MinusCredits(tower._rangeLevelCosts[tower._rangeLevel]);

                //set new stats
                tower.gameObject.GetComponent<SphereCollider>().radius = 
                    tower.gameObject.GetComponent<SphereCollider>().radius + tower.GetRadiusUpgrade();

                tower._rangeLevel++;

                break;
            //attack priorities
            case "First Enemy":
                tower.TargetParameters = AttackChoice.First;
                break;
            case "Last Enemy":
                tower.TargetParameters = AttackChoice.Last;
                break;
            case "Most Damage":
                tower.TargetParameters = AttackChoice.MostDamage;
                break;
            case "Least Damage":
                tower.TargetParameters = AttackChoice.LeastDamage;
                break;
            case "Most Health":
                tower.TargetParameters = AttackChoice.MostHealth;
                break;
            case "Least Health":
                tower.TargetParameters = AttackChoice.LeastHealth;
                break;
            default:
                break;
        }

        parentRM.CheckPrices();
        UpdateButton();
        LoadTooltipText();
    }

}

//================================================
//custom entered code = Dylan McAdam
//this was an attempt to get the fields to appear and disappear in the inspector,
//it worked, but you had to pass prefab objects in to the fields in the inspector 
//and for some reason it didnt work that way
//================================================

//[CustomEditor(typeof(RMF_RadialMenuElement))]
//public class RMF_RadialMenuElementEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        var myScript = target as RMF_RadialMenuElement;

//        myScript.button = (Button)EditorGUILayout.ObjectField("Button", myScript.button, typeof(Button), false, null);
//        myScript.label = EditorGUILayout.TextField("Label", myScript.label);

//        myScript._usingToolTips = EditorGUILayout.Toggle("Using Tool Tips", myScript._usingToolTips);

//        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(myScript._usingToolTips)))
//        {
//            if (group.visible == true)
//            {
//                EditorGUI.indentLevel++;
//                //EditorGUILayout.PrefixLabel("Tool Tip Object");
//                myScript._toolTipBox = (GameObject)EditorGUILayout.ObjectField("Tool Tip Object", myScript._toolTipBox, typeof(GameObject), false);
//                //EditorGUILayout.PrefixLabel("Tool Tip Text");
//                myScript._toolTipText = (GameObject)EditorGUILayout.ObjectField("Tool Tip Text", myScript._toolTipText, typeof(GameObject), false);
//                EditorGUI.indentLevel--;
//            }
//        }
//    }
//}

//================================================
//end of custom entered code
//================================================
