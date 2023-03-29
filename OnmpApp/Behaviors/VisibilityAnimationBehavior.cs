using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmpApp.Behaviors
{
    public class VisibilityAnimationBehavior : Behavior<View>
    {
        public static readonly BindableProperty TranslateAnimationProperty =
            BindableProperty.Create(nameof(TranslateAnimation), typeof(bool), typeof(VisibilityAnimationBehavior), default(bool));

        private View _associatedObject;

        public bool TranslateAnimation
        {
            get => (bool)GetValue(TranslateAnimationProperty);
            set => SetValue(TranslateAnimationProperty, value);
        }

        protected override void OnAttachedTo(View bindable)
        {
            _associatedObject = bindable;
            base.OnAttachedTo(bindable);
            bindable.PropertyChanged += OnViewPropertyChanged;
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.PropertyChanged -= OnViewPropertyChanged;
        }

        private async void OnViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(View.IsVisible))
            {
                if (_associatedObject.IsVisible)
                {
                    await ShowViewAsync();
                }
                else
                {
                    await HideViewAsync();
                }
            }
        }

        private async Task ShowViewAsync()
        {
            _associatedObject.HeightRequest = -1;
            _associatedObject.Opacity = 0;
            await Task.WhenAll(
                _associatedObject.FadeTo(1, 250),
                TranslateAnimation ? _associatedObject.TranslateTo(0, 0, 250) : Task.CompletedTask
            );
        }

        private async Task HideViewAsync()
        {
            await Task.WhenAll(
                _associatedObject.FadeTo(0, 250),
                TranslateAnimation ? _associatedObject.TranslateTo(0, -20, 250) : Task.CompletedTask
            );
            _associatedObject.HeightRequest = 0;
        }
    }
}
