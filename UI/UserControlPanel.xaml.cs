// neural v3.0
using System;
using System.Windows;
using System.Windows.Controls;
using JaysAi.Finale.Input;
using JaysAi.Finale.Input.Handlers;

namespace JaysAi.Finale.UI.Panels
{
    public partial class UserControlPanel : UserControl
    {
        public UserControlPanel()
        {
            InitializeComponent();
        }

        private void RefreshInput_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ControllerIdBox.Text, out int controllerId))
            {
                var state = ControllerBridge.Instance.GetState(controllerId);
                if (state != null)
                {
                    StateText.Text = $"LX: {state.LeftStickX:F2}, LY: {state.LeftStickY:F2}, RX: {state.RightStickX:F2}, RY: {state.RightStickY:F2}";
                }
                else
                {
                    StateText.Text = "No data for this controller ID.";
                }
            }
            else
            {
                StateText.Text = "Invalid ID.";
            }
        }
    }
}
