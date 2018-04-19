using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject {
    public string unitName;
    public enum Faction { Alliance, Horde };
    public Faction faction;
    public enum Race { Human, NightElf, Orc, Undead};
    public Race race;
    public List<string> audioFiles;
}
