namespace OnmpApp.Controls;

public partial class TextEntryControl : ContentView
{
	public TextEntryControl()
	{
		InitializeComponent();
	}

    // �������� ��� �����
    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(propertyName: nameof(LabelText),
        returnType: typeof(string), declaringType: typeof(EmailControl), defaultValue: "", defaultBindingMode: BindingMode.TwoWay);

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    // ����� � ����
    public static readonly BindableProperty EntryTextProperty = BindableProperty.Create(propertyName: nameof(EntryText),
        returnType: typeof(string), declaringType: typeof(EmailControl), defaultValue: "", defaultBindingMode: BindingMode.TwoWay);

    public string EntryText
    {
        get => (string)GetValue(EntryTextProperty);
        set => SetValue(EntryTextProperty, value);
    }

    // ������� ��� �������� ��� ������
    public static readonly BindableProperty InvalidTrigerProperty = BindableProperty.Create(propertyName: nameof(InvalidTriger),
        returnType: typeof(bool), declaringType: typeof(EmailControl), defaultValue: false, defaultBindingMode: BindingMode.TwoWay);

    public bool InvalidTriger
    {
        get => (bool)GetValue(InvalidTrigerProperty);
        set => SetValue(InvalidTrigerProperty, value);
    }
}