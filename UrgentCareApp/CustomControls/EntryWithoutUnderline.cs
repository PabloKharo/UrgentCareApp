namespace UrgentCareApp.CustomControls
{
    internal class EntryWithoutUnderline :Entry
    {
        public static readonly BindableProperty NoUnderlineProperty =
            BindableProperty.Create("NoUnderline", typeof(bool), typeof(EntryWithoutUnderline), false);

        public bool NoUnderline
        {
            get => (bool)GetValue(NoUnderlineProperty);
            set => SetValue(NoUnderlineProperty, value);
        }
    }
}
