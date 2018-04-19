using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MiniJSON;

public class LocalizationManager : MonoBehaviour {

    #region UnityCompliant Singleton
    public static LocalizationManager Instance
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

    // Localization path : Application.streamingAssetsPath /i18n/localization.json
    [SerializeField]
    string localizationFile;

    public string currentLang;

    public string defaultLang;

    LanguageStruct loadedLocalization;

    List<LocalizedText> translatedText;

    bool isLoaded = false;

    public bool IsLoaded
    {
        get { return isLoaded; }
    }

    void Start () {
        LoadLocalization();
    }

    public void LoadLocalization()
    {
        if (IsLoaded) return;

        Dictionary<string, object> genericLocalizationObject = null;

        if (File.Exists(Application.streamingAssetsPath + "/" + localizationFile))
        {
            try
            {
                genericLocalizationObject = Json.Deserialize(File.ReadAllText(Application.streamingAssetsPath + "/" + localizationFile)) as Dictionary<string, object>;
            }
            catch (IOException e)
            {
                Debug.LogError(e.Message);
                Debug.LogWarning("Can't open localization file at " + Application.streamingAssetsPath + "/" + localizationFile);
            }
        }

        if (genericLocalizationObject != null)
        {
            loadedLocalization = new LanguageStruct();
            loadedLocalization.i18n = new Dictionary<string, TermStruct>();

            var termsData = genericLocalizationObject["localization"] as Dictionary<string, object>;
            foreach (var item in termsData)
            {
                TermStruct term = new TermStruct();
                term.terms = new Dictionary<string, string>();

                var translationData = item.Value as Dictionary<string, object>;
                foreach (var t in translationData) {
                    term.terms.Add(t.Key, t.Value as string);
                }

                loadedLocalization.i18n.Add(item.Key, term);
            }

            // Default language
            defaultLang = genericLocalizationObject["default"] as string;
            currentLang = defaultLang;

            isLoaded = true;

            // Translate text in list of localized text at start
            SetLang(currentLang);
        }
    }

    public string Translate(string term, string lang)
    {
        if (term == "" || lang == "")
            return "";

        if (!IsLoaded) LoadLocalization();

        if (loadedLocalization != null && loadedLocalization.i18n != null
            && loadedLocalization.i18n.ContainsKey(term) && loadedLocalization.i18n[term].terms.ContainsKey(lang))
        {
            return loadedLocalization.i18n[term].terms[lang];
        }

        return "";
    }

    public string Translate(string term)
    {
        return Translate(term, currentLang);
    }

    // Called by LocalizedText at Start
    public void AddText(LocalizedText text)
    {
        if (translatedText == null) {
            translatedText = new List<LocalizedText>();
        }

        translatedText.Add(text);
    }

    public void SetLang(string lang)
    {
        // Set current language
        currentLang = lang;

        // Refresh all text after language change
        if (translatedText != null) {
            foreach (LocalizedText text in translatedText) {
                if (text == null) {
                    translatedText.Remove(text);
                    continue;
                }

                text.Translate();
            }
        }
    }

    public void Reset()
    {
        SetLang(defaultLang);
    }

}

public class LanguageStruct
{
    public Dictionary<string, TermStruct> i18n;
}

public class TermStruct
{
    public Dictionary<string, string> terms;
}