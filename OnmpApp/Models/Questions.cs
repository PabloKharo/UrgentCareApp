using System.Text;
using OnmpApp.Properties;

namespace OnmpApp.Models;

public abstract class TestQuestion
{
    public string QuestionText { get; set; }
    public int QuestionNumber { get; set; }
    public List<string> Options { get; set; }
    public string AdditionalLabelText { get; set; } = "";
    public Type ResultType { get; set; } = typeof(string);


    public abstract string GetValue();
    public abstract void SetValue(string val);
}

public class RadioButtonQuestion : TestQuestion
{
    public int SelectedOptionIndex { get; set; } = -1;

    public override string GetValue()
    {
        if (SelectedOptionIndex == -1)
            return null;

        return Options[SelectedOptionIndex];
    }

    public override void SetValue(string val)
    {
        SelectedOptionIndex = Options.IndexOf(val);
    }
}

public class RadioButtonWithTextQuestion : RadioButtonQuestion
{
    public string AdditionalText { get; set; }

    public override string GetValue()
    {
        StringBuilder str = new();
        if (!string.IsNullOrEmpty(AdditionalLabelText))
        {
            if (SelectedOptionIndex >= 0)
                str.Append(Options[SelectedOptionIndex]);

            str.Append(Settings.FieldDelimeter + AdditionalLabelText + Settings.FieldDelimeter);

            AdditionalText = AdditionalText?.Trim();
            if (!string.IsNullOrEmpty(AdditionalText))
                str.Append(AdditionalText);

            if (str.ToString() == Settings.FieldDelimeter + AdditionalLabelText + Settings.FieldDelimeter)
                return null;
        }
        else
        {
            if (SelectedOptionIndex >= 0)
                str.Append(Options[SelectedOptionIndex]);
            else
                str.Append(AdditionalText);

            if (str.ToString().Trim() == "")
                return null;
        }

        return str.ToString();
    }

    public override void SetValue(string val)
    {
        if (AdditionalLabelText != null && AdditionalLabelText != "")
        {
            var splitStrings = val.Split(new[] { Settings.FieldDelimeter }, StringSplitOptions.None);
            SelectedOptionIndex = Options.IndexOf(splitStrings[0]);
            AdditionalLabelText = splitStrings[2];
        }
        else
        {
            SelectedOptionIndex = Options.IndexOf(val);
            if (SelectedOptionIndex == -1)
                AdditionalText = val;
        }
    }
}

public class CheckBoxQuestion : TestQuestion
{
    public List<bool> SelectedOptions { get; set; }

    public override string GetValue()
    {
        StringBuilder str = new();
        for (var i = 0; i < Options.Count; i++)
            if (SelectedOptions[i])
            {
                str.Append(Options[i]);
                str.Append(Settings.PropertyDelimeter);
            }

        if (str.Length > 0)
            str.Length -= 1;

        return str.ToString();
    }

    public override void SetValue(string val)
    {
        var splitStrings = val.Split(new[] { Settings.PropertyDelimeter }, StringSplitOptions.None);

        for (var i = 0; i < splitStrings.Length; i++)
        {
            var ind = Options.IndexOf(splitStrings[i]);
            if (ind != -1)
                SelectedOptions[ind] = true;
        }
    }
}

public class CheckBoxWithTextQuestion : CheckBoxQuestion
{
    public string AdditionalText { get; set; }

    public override string GetValue()
    {
        StringBuilder str = new();
        if (AdditionalLabelText != null && AdditionalLabelText != "")
        {
            for (var i = 0; i < SelectedOptions.Count; i++)
                if (SelectedOptions[i])
                    str.Append(Options[i] + Settings.PropertyDelimeter);
            if (str.Length > 0)
                str.Length -= 1;

            str.Append(Settings.FieldDelimeter + AdditionalLabelText + Settings.FieldDelimeter);

            AdditionalText = AdditionalText?.Trim();
            if (AdditionalText != null && AdditionalText != "")
                str.Append(AdditionalText);

            if (str.ToString() == Settings.FieldDelimeter + AdditionalLabelText + Settings.FieldDelimeter)
                return null;
        }
        else
        {
            AdditionalText = AdditionalText?.Trim();
            if (AdditionalText != null && AdditionalText != "")
                str.Append(AdditionalText);
            else
                for (var i = 0; i < SelectedOptions.Count; i++)
                    if (SelectedOptions[i])
                        str.Append(Options[i] + Settings.PropertyDelimeter);

            if (str.ToString().Trim() == "")
                return null;
        }

        return str.ToString();
    }

    public override void SetValue(string val)
    {
        if (AdditionalLabelText != null && AdditionalLabelText != "")
        {
            var splitStrings = val.Split(new[] { Settings.FieldDelimeter }, StringSplitOptions.None);

            var splitCheckBoxes = splitStrings[0].Split(new[] { Settings.PropertyDelimeter }, StringSplitOptions.None);
            for (var i = 0; i < splitCheckBoxes.Length; i++)
            {
                var id = Options.IndexOf(splitCheckBoxes[i]);
                if (id != -1)
                    SelectedOptions[id] = true;
            }

            if (splitStrings.Length > 1)
                AdditionalText = splitStrings[2];
        }
        else
        {
            if (val.Contains(Settings.PropertyDelimeter))
            {
                var splitCheckBoxes = val.Split(new[] { Settings.PropertyDelimeter }, StringSplitOptions.None);
                for (var i = 0; i < splitCheckBoxes.Length; i++)
                {
                    var id = Options.IndexOf(splitCheckBoxes[i]);
                    if (id != -1)
                        SelectedOptions[id] = true;
                }
            }
            else
            {
                AdditionalText = val;
            }
        }
    }
}

public class TextQuestion : TestQuestion
{
    public string AnswerText { get; set; }

    public override string GetValue()
    {
        AnswerText = AnswerText?.Trim();
        if (AnswerText == null || AnswerText == "")
            return null;
        return AnswerText;
    }

    public override void SetValue(string val)
    {
        AnswerText = val;
    }
}

public class TestQuestionTemplateSelector : DataTemplateSelector
{
    public DataTemplate RadioButtonQuestionTemplate { get; set; }
    public DataTemplate RadioButtonWithTextQuestionTemplate { get; set; }
    public DataTemplate CheckBoxQuestionTemplate { get; set; }
    public DataTemplate CheckBoxWithTextQuestionTemplate { get; set; }
    public DataTemplate TextQuestionTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return item switch
        {
            RadioButtonWithTextQuestion => RadioButtonWithTextQuestionTemplate,
            RadioButtonQuestion => RadioButtonQuestionTemplate,
            CheckBoxWithTextQuestion => CheckBoxWithTextQuestionTemplate,
            CheckBoxQuestion => CheckBoxQuestionTemplate,
            TextQuestion => TextQuestionTemplate,
            _ => throw new NotSupportedException("Unknown question type")
        };
    }
}