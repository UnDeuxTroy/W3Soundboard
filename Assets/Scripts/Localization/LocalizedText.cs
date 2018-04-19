using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour {

    [SerializeField]
    [TextArea]
    string baseText;

    [Space(14)]
    [SerializeField]
    bool translateOnStart = true;

    [SerializeField]
    bool replaceText;

    [Space(14)]
    [SerializeField]
    Text textComponent;

    bool translated = false;

	void Start () {
        translated = false;

		if (textComponent == null) {
            textComponent = GetComponent<Text>();
        }

        if (replaceText) {
            textComponent.text = baseText;
        } else {
            baseText = textComponent.text;
        }

        LocalizationManager.Instance.AddText(this);

        if (translateOnStart && translated == false && LocalizationManager.Instance.IsLoaded)
        {
            Translate();
        }
	}

    public void Translate()
    {
        textComponent.text = baseText;

        var pattern = @"\${.+?}";

        RegexOptions options = RegexOptions.Multiline;
        var result = Regex.Matches(textComponent.text, pattern, options);

        var term_pattern = @"^\${(.+)}$";
        foreach (var m in result)
        {
            var r = Regex.Match(m.ToString(), term_pattern);
            string replace_by = LocalizationManager.Instance.Translate(r.Groups[1].ToString());

            if (replace_by != "")
            {
                textComponent.text = textComponent.text.Replace(m.ToString(), replace_by);
            }
        }
    }

}
