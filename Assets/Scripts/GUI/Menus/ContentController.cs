using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContentController : MonoBehaviour {

    [Header("GUI Components")]
    [SerializeField]
    Image unitIcon;
    [SerializeField]
    TMP_Dropdown raceSelection;
    [SerializeField]
    TMP_Dropdown unitSelection;

    string race;
    string unit;
    AppController game;

    private void Start()
    {
        game = AppController.Instance;
    }

    public void SetFaction(Faction factionToSet)
    {
        game.selectedFaction = factionToSet;
        Debug.Log("Faction set to: " + game.selectedFaction.name);
    }

    public void SetRace()
    {
        race = raceSelection.options[raceSelection.value].text;
        Debug.Log("Race set to: " + race);
    }

    public void SetUnit()
    {
        TMP_Dropdown.OptionData data = unitSelection.options[unitSelection.value];
        if (data == null)
            return;
        unit = data.text;
        Debug.Log("Unit set to: " + unit);
        game.SelectUnit(race + unit);
        unitIcon.sprite = Resources.Load("Sprites/UnitIcons/" + game.selectedFaction.name + "/" + race + "/" + unit, typeof(Sprite)) as Sprite;
    }
}
