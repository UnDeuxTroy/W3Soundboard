using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContentController : MonoBehaviour
{

    [Header("GUI Components")]
    [SerializeField]
    Image unitIcon;
    [SerializeField]
    Image unitFrame;
    [SerializeField]
    Image[] buttons;
    [SerializeField]
    TMP_Dropdown raceSelection;
    [SerializeField]
    TMP_Dropdown unitSelection;

    [Space(10)]
    [Header("Sprites")]
    [SerializeField]
    Sprite allianceFrame;
    [SerializeField]
    Sprite hordeFrame;
    [SerializeField]
    Sprite allianceButton;
    [SerializeField]
    Sprite hordeButton;

    [Space(10)]
    [Header("Audio contents setup")]
    [SerializeField]
    GameObject container;
    [SerializeField]
    GameObject buttonPrefab;
    [SerializeField]
    AudioSource source;

    Race selectedRace;
    Unit selectedUnit;
    AppController game;

    private void Start()
    {
        game = AppController.Instance;
    }

    void SetupFactionDisplay()
    {
        if (game.selectedFaction.name == "Alliance")
        {
            unitFrame.sprite = allianceFrame;
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].sprite = allianceButton;
        }
        else
        {
            unitFrame.sprite = hordeFrame;
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].sprite = hordeButton;
        }
    }

    public void SetFaction(Faction factionToSet)
    {
        game.selectedFaction = factionToSet;
        SetupFactionDisplay();
        raceSelection.ClearOptions();
        List<TMP_Dropdown.OptionData> dataList = new List<TMP_Dropdown.OptionData>();
        foreach (Race race in factionToSet.raceList)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData()
            {
                text = race.name
            };
            dataList.Add(data);
        }
        raceSelection.AddOptions(dataList);
        raceSelection.value = 0;
        SetRace();
    }

    public void SetRace()
    {
        foreach (Race race in game.selectedFaction.raceList)
        {
            if (race.name == raceSelection.options[raceSelection.value].text)
                selectedRace = race;
        }
        unitSelection.ClearOptions();
        List<TMP_Dropdown.OptionData> dataList = new List<TMP_Dropdown.OptionData>();
        foreach (Unit unit in selectedRace.unitList)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData()
            {
                text = unit.name
            };
            dataList.Add(data);
        }
        unitSelection.AddOptions(dataList);
        unitSelection.value = 0;
        SetUnit();
    }

    public void SetUnit()
    {
        TMP_Dropdown.OptionData data = unitSelection.options[unitSelection.value];
        if (data == null)
            return;
        foreach (Unit unit in selectedRace.unitList)
        {
            if (unit.name == unitSelection.options[unitSelection.value].text)
                selectedUnit = unit;
        }
        game.SelectUnit(selectedRace.name + selectedUnit.name);
        unitIcon.sprite = Resources.Load("Sprites/UnitIcons/" + game.selectedFaction.name + "/" + selectedRace.name
            + "/" + selectedUnit.name, typeof(Sprite)) as Sprite;
        SetupAudioContent();
    }
    
    void SetupAudioContent()
    {
        foreach (Transform child in container.transform)
            Destroy(child.gameObject);

        foreach (string audioPath in selectedUnit.audioFiles)
        {
            GameObject audioButton = Instantiate(buttonPrefab, container.transform);
            AudioContent audioContent = audioButton.GetComponent<AudioContent>();
            audioContent.label.text = audioPath;
            audioContent.clip = Resources.Load("Sounds/Units/" + game.selectedFaction.name + "/" + selectedRace.name
                + "/" + selectedUnit.name + "/" + selectedUnit.name + audioPath) as AudioClip;
            audioContent.audioSource = source;
            audioContent.Setup();
        }
    }
}
