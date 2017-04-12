﻿using MenuForms.Animations.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MenuForms.Behaviors
{
    public class BeginAnimationBehavior : Behavior<VisualElement>
    {
        private static VisualElement associatedObject;

        protected override async void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            associatedObject = bindable;

            if (Animation != null)
            {
                if (Animation.Target == null)
                {
                    Animation.Target = associatedObject;
                }

                var delay = Task.Delay(250);
                await Task.WhenAll(delay);
                await Animation.Begin();
            }
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            associatedObject = null;
            base.OnDetachingFrom(bindable);
        }

        public static readonly BindableProperty AnimationProperty =
          BindableProperty.Create("Animation", typeof(AnimationBase), typeof(BeginAnimationBehavior), null,
              BindingMode.OneWay, null);

        public AnimationBase Animation
        {
            get { return (AnimationBase)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }
    }
}
