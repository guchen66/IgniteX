using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace IgniteX.UI.Behaviors
{
    public static class CommandBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(CommandBehavior),
                new PropertyMetadata(null, CommandPropertyChanged));
        public static readonly DependencyProperty ParameterProperty =
             DependencyProperty.RegisterAttached(
        "Parameter",
        typeof(object),
        typeof(CommandBehavior),
        new PropertyMetadata(null));

        public static void SetParameter(UIElement element, object value)
        {
            element.SetValue(ParameterProperty, value);
        }

        public static object GetParameter(UIElement element)
        {
            return element.GetValue(ParameterProperty);
        }
        public static void SetCommand(UIElement element, ICommand value)
        {
            element.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        private static void CommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                if (e.NewValue is ICommand command)
                {
                    element.MouseLeftButtonDown += Element_MouseLeftButtonDown;
                }
                else
                {
                    element.MouseLeftButtonDown -= Element_MouseLeftButtonDown;
                }
            }
        }

        private static void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is UIElement element && GetCommand(element) is ICommand command)
            {
                if (command.CanExecute(null))
                {
                    command.Execute(null);
                }
            }
        }
    }
}
