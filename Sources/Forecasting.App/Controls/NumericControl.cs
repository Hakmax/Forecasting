using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Forecasting.App.Controls
{
    public class NumericControl : Control
    {
        private Button _upButton;
        private Button _downButton;
        private TextBox _inputTextbox;
        private readonly static DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericControl), new UIPropertyMetadata(10));
        private readonly static DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(int), typeof(NumericControl), new UIPropertyMetadata(1));
        private readonly static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(NumericControl), new UIPropertyMetadata(1, ValueChanged));
        private readonly static DependencyProperty IncrementButtonsVisibilityProperty = DependencyProperty.Register("IncrementButtonsVisibility", typeof(Visibility), typeof(NumericControl), new PropertyMetadata(Visibility.Visible));

        static NumericControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericControl), new FrameworkPropertyMetadata(typeof(NumericControl)));
        }

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                SetCurrentValue(ValueProperty, value);
                string strVal = value.ToString();
                if (strVal != _inputTextbox.Text)
                    _inputTextbox.Text = strVal;
            }
        }

        public Visibility IncrementButtonsVisibility
        {
            get { return (Visibility)GetValue(IncrementButtonsVisibilityProperty); }
            set { SetValue(IncrementButtonsVisibilityProperty, value); }
        }


        public static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _upButton = Template.FindName("UpButton", this) as Button;
            _downButton = Template.FindName("DownButton", this) as Button;
            _inputTextbox = Template.FindName("InputTextBox", this) as TextBox;
            _inputTextbox.KeyDown += _inputTextbox_KeyDown;
            _inputTextbox.TextChanged += _inputTextbox_TextChanged;
            _inputTextbox.Text = ((int)GetValue(MinValueProperty)).ToString();
            _upButton.Click += _upButton_Click;
            _downButton.Click += _downButton_Click;
        }

        private void _downButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value > MinValue)
                Value -= 1;
        }

        private void _upButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value < MaxValue)
                Value += 1;
        }

        private void _inputTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int inputVal = 0;
            if (int.TryParse(_inputTextbox.Text, out inputVal))
            {
                if (inputVal > MinValue && inputVal < MaxValue && inputVal != Value)
                    Value = inputVal;
                else
                    _inputTextbox.Text = Value.ToString();
            }
            else
            {
                Value = MinValue;
            }
        }

        private void _inputTextbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }
    }
}
