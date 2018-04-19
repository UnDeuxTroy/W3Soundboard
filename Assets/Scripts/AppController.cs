using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour {

    #region UnityCompliant Singleton
    public static AppController Instance
    {
        get;
        private set;
    }

    public virtual void Awake()
    {
        if (Instance == null)
        {   
            Instance = this;
            return;
        }

        Destroy(this.gameObject);
    }
    #endregion

    [Header("Faction Switch")]
    [SerializeField]
    Texture2D cursorOrc;
    [SerializeField]
    MusicController mainMusic;

    [Space(10)]
    [Header("Unit Selection")]
    public Unit selectedUnit;
    public Faction selectedFaction;


    public void SwitchToAlliance()
    {
        mainMusic.SwitchMusic(true);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void SwitchToHorde()
    {
        mainMusic.SwitchMusic(false);
        Cursor.SetCursor(cursorOrc, Vector2.zero, CursorMode.Auto);
    }

    public void SelectUnit(string unitToSelect)
    {
        selectedUnit = Resources.Load("Units/" + selectedFaction.name + "/" + unitToSelect, typeof(Unit)) as Unit;
    }

    public void ExitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
