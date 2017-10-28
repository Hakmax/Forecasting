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
        public readonly static DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericControl), new UIPropertyMetadata(100));
        public readonly static DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(int), typeof(NumericControl), new UIPropertyMetadata(1));
        public readonly static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int?), typeof(NumericControl), new UIPropertyMetadata(1, ValueChanged));
        public readonly static DependencyProperty IncrementButtonsVisibilityProperty = DependencyProperty.Register("IncrementButtonsVisibility", typeof(Visibility), typeof(NumericControl), 
            new PropertyMetadata(Visibility.Visible));
        public readonly static DependencyProperty AllowNullProperty = DependencyProperty.Register("AllowNull", typeof(bool), typeof(NumericControl), new UIPropertyMetadata(false));

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

        public int? Value
        {
            get { return (int?)GetValue(ValueProperty); }
            set
            {
                SetCurrentValue(ValueProperty, value);
                string strVal = value.ToString();
                if (strVal != _inputTextbox.Text)
                    _inputTextbox.Text = strVal;
            }
        }


        public bool AllowNull
        {
            get
            {
                return (bool)GetValue(AllowNullProperty);
            }
            set
            {
                SetValue(AllowNullProperty, value);
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
            GotFocus += NumericControl_GotFocus;
            _upButton = Template.FindName("UpButton", this) as Button;
            _downButton = Template.FindName("DownButton", this) as Button;
            _inputTextbox = Template.FindName("InputTextBox", this) as TextBox;
            _inputTextbox.Text = Value.ToString();
            _inputTextbox.TextChanged += inputTextbox_TextChanged;
            _upButton.Click += upButton_Click;
            _downButton.Click += downButton_Click;
        }

        private void NumericControl_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_inputTextbox.Focusable)
            {
                _inputTextbox.SelectAll();
                _inputTextbox.Focus();
            }
        }

        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value > MinValue)
                Value -= 1;
        }

        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value < MaxValue)
                Value += 1;
        }

        private void inputTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int inputVal = 0;
            if (int.TryParse(_inputTextbox.Text, out inputVal))
            {
                if (inputVal >= MinValue && inputVal <= MaxValue && inputVal != Value)
                    Value = inputVal;
                else
                    _inputTextbox.Text = Value.ToString();
            }
            else
            {
                if (!AllowNull)
                    Value = MinValue;
                else
                    Value = null;
            }
        }
    }
}
