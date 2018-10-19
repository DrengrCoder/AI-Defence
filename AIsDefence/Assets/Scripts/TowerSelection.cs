using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelection : MonoBehaviour {
    
    public Button[] BUTTONS;

    private bool blackPressed = false;
    private bool redPressed = false;

	// Use this for initialization
	void Start ()
    {
        this.BUTTONS[0].GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(0); });
        this.BUTTONS[1].GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(1); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ButtonClick(int i)
    {
        this.BUTTONS[0].GetComponent<Button>().GetComponent<Image>().color = new Color(255, 255, 255);
        this.BUTTONS[1].GetComponent<Button>().GetComponent<Image>().color = new Color(255, 255, 255);
        this.blackPressed = false;
        this.redPressed = false;

        switch (i)
        {
            case 0:
                this.BUTTONS[0].GetComponent<Button>().GetComponent<Image>().color = new Color(0, 255, 0);
                this.blackPressed = true;
                break;
            case 1:
                this.BUTTONS[1].GetComponent<Button>().GetComponent<Image>().color = new Color(0, 255, 0);
                this.redPressed = true;
                break;
            default:
                break;
        }
    }

    public bool RedTowerSelected()
    {
        return this.redPressed;
    }

    public bool BlackTowerSelected()
    {
        return this.blackPressed;
    }
}
