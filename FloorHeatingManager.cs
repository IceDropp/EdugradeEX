using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EdugradeEX
{
    public class FloorHeatingManager
    {
        // Visa ett fönster för att konfigurera golvvärme
        public void ShowConfigurationWindow(Action<FloorHeatingData> onSave)
        {
            Window configWindow = new Window
            {
                Title = "Konfigurera golvvärme",
                Width = 300,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            // Skapa input-fält för golvvärmeinställningar
            StackPanel configPanel = new StackPanel { Margin = new Thickness(10) };

            // Massflöde
            Grid massFlowGrid = new Grid();
            TextBox massFlowInput = new TextBox { Margin = new Thickness(5) };
            TextBlock massFlowPlaceholder = new TextBlock
            {
                Text = "Massflöde (kg/s)",
                Foreground = Brushes.Gray,
                IsHitTestVisible = false
            };
            massFlowGrid.Children.Add(massFlowInput);
            massFlowGrid.Children.Add(massFlowPlaceholder);

            // Utomhustemperatur
            Grid tempOutsideGrid = new Grid();
            TextBox tempOutsideInput = new TextBox { Margin = new Thickness(5) };
            TextBlock tempOutsidePlaceholder = new TextBlock
            {
                Text = "Utomhustemperatur (°C)",
                Foreground = Brushes.Gray,
                IsHitTestVisible = false
            };
            tempOutsideGrid.Children.Add(tempOutsideInput);
            tempOutsideGrid.Children.Add(tempOutsidePlaceholder);

            // Innetemperatur
            Grid tempInsideGrid = new Grid();
            TextBox tempInsideInput = new TextBox { Margin = new Thickness(5) };
            TextBlock tempInsidePlaceholder = new TextBlock
            {
                Text = "Innetemperatur (°C)",
                Foreground = Brushes.Gray,
                IsHitTestVisible = false
            };
            tempInsideGrid.Children.Add(tempInsideInput);
            tempInsideGrid.Children.Add(tempInsidePlaceholder);

            // Isolering
            Grid insulationGrid = new Grid();
            TextBox insulationInput = new TextBox { Margin = new Thickness(5) };
            TextBlock insulationPlaceholder = new TextBlock
            {
                Text = "Isolering (0-1)",
                Foreground = Brushes.Gray,
                IsHitTestVisible = false
            };
            insulationGrid.Children.Add(insulationInput);
            insulationGrid.Children.Add(insulationPlaceholder);

            // Kostnad
            Grid costGrid = new Grid();
            TextBox costInput = new TextBox { Margin = new Thickness(5) };
            TextBlock costPlaceholder = new TextBlock
            {
                Text = "Kostnad (kWh)",
                Foreground = Brushes.Gray,
                IsHitTestVisible = false
            };
            costGrid.Children.Add(costInput);
            costGrid.Children.Add(costPlaceholder);

            // Knapp för att spara
            Button saveButton = new Button { Content = "Spara till JSON", Margin = new Thickness(5) };

            // Lägg till fält i panelen
            configPanel.Children.Add(massFlowGrid);
            configPanel.Children.Add(tempOutsideGrid);
            configPanel.Children.Add(tempInsideGrid);
            configPanel.Children.Add(insulationGrid);
            configPanel.Children.Add(costGrid);
            configPanel.Children.Add(saveButton);

            // Hantera textändringar för att visa/dölja platshållartext
            massFlowInput.TextChanged += (s, ev) => massFlowPlaceholder.Visibility = string.IsNullOrEmpty(massFlowInput.Text) ? Visibility.Visible : Visibility.Collapsed;
            tempOutsideInput.TextChanged += (s, ev) => tempOutsidePlaceholder.Visibility = string.IsNullOrEmpty(tempOutsideInput.Text) ? Visibility.Visible : Visibility.Collapsed;
            tempInsideInput.TextChanged += (s, ev) => tempInsidePlaceholder.Visibility = string.IsNullOrEmpty(tempInsideInput.Text) ? Visibility.Visible : Visibility.Collapsed;
            insulationInput.TextChanged += (s, ev) => insulationPlaceholder.Visibility = string.IsNullOrEmpty(insulationInput.Text) ? Visibility.Visible : Visibility.Collapsed;
            costInput.TextChanged += (s, ev) => costPlaceholder.Visibility = string.IsNullOrEmpty(costInput.Text) ? Visibility.Visible : Visibility.Collapsed;

            // Hantera klick på spara-knappen
            saveButton.Click += (s, ev) =>
            {
                try
                {
                    // Skapa ett nytt FloorHeatingData-objekt med användarens input
                    var data = new FloorHeatingData
                    {
                        MassFlow = double.Parse(massFlowInput.Text),
                        TemperatureOutside = double.Parse(tempOutsideInput.Text),
                        TemperatureInside = double.Parse(tempInsideInput.Text),
                        Insulation = double.Parse(insulationInput.Text),
                        Cost = double.Parse(costInput.Text)
                    };

                    // Anropa callback-funktionen för att spara data
                    onSave(data);
                    configWindow.Close(); // Stäng konfigurationsfönstret
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fel vid sparande: {ex.Message}");
                }
            };

            configWindow.Content = configPanel;
            configWindow.ShowDialog(); // Visa konfigurationsfönstret
        }

        // Beräkna effekten för golvvärme
        public double CalculatePower(FloorHeatingData data)
        {
            // Värmekapacitet för vatten (J/kg·K)
            double heatCapacity = 4186;

            // Beräkna effekten med formeln P = m * Cp * (T_inside - T_outside)
            return data.MassFlow * heatCapacity * (data.TemperatureInside - data.TemperatureOutside);
        }
    }
}