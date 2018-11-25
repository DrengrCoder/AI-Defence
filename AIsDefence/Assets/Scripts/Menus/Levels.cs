using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour {

    [SerializeField]
    private SaveData _saveData;

    [SerializeField]
    private Button[] _buttons;

    public void ActivateButtons()
    {
        int unlocks = _saveData.LevelsUnlocked;

        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i < unlocks)
            {
                _buttons[i].interactable = true;
            }
            else
            {
                _buttons[i].interactable = false;
            }
        }
    }

}
