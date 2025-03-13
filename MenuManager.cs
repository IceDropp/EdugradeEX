using System.Windows;
using System.Windows.Controls;

namespace EdugradeEX
{
    public class MenuManager
    {
        private FloorHeatingManager floorHeatingManager;
        private JsonManager jsonManager;
        private TextBlock resultText;

        public MenuManager(TextBlock resultText)
        {
            this.resultText = resultText;
            floorHeatingManager = new FloorHeatingManager();
            jsonManager = new JsonManager();
        }

        // Skapa huvudmenyn
        public Menu CreateMainMenu()
        {
            Menu menu = new Menu();

            // Golvvärmemenyn
            MenuItem floorHeatingMenu = new MenuItem { Header = "Golvvärme" };

            // Undermeny för Golvvärme
            MenuItem inputData = new MenuItem { Header = "Skriv in data" };
            MenuItem outputData = new MenuItem { Header = "Skriv ut data" };
            MenuItem loadData = new MenuItem { Header = "Läs in data från JSON" };

            // Lägg till undermenyalternativ
            floorHeatingMenu.Items.Add(inputData);
            floorHeatingMenu.Items.Add(outputData);
            floorHeatingMenu.Items.Add(loadData);

            // Lägg till Golvvärmemenyn i huvudmenyn
            menu.Items.Add(floorHeatingMenu);

            // Koppla eventhanterare
            inputData.Click += InputData_Click;
            outputData.Click += OutputData_Click;
            loadData.Click += LoadData_Click;

            return menu;
        }

        // Eventhanterare för att skriva in data (publik)
        public void InputData_Click(object sender, RoutedEventArgs e)
        {
            floorHeatingManager.ShowConfigurationWindow(jsonManager.SaveToJson);
        }

        // Eventhanterare för att skriva ut data (nu publik)
        public void OutputData_Click(object sender, RoutedEventArgs e)
        {
            var data = jsonManager.LoadFromJson();
            if (data != null)
            {
                resultText.Text = $"Aktuell data:\nMassflöde: {data.MassFlow} kg/s\nUtomhustemp: {data.TemperatureOutside} °C\nInnetemp: {data.TemperatureInside} °C\nIsolering: {data.Insulation}\nKostnad: {data.Cost} kWh";
            }
            else
            {
                resultText.Text = "Ingen data tillgänglig. Vänligen läs in eller skriv in data.";
            }
        }

        // Eventhanterare för att läsa in data från JSON (nu publik)
        public void LoadData_Click(object sender, RoutedEventArgs e)
        {
            var data = jsonManager.LoadFromJson();
            if (data != null)
            {
                resultText.Text = $"Data från JSON:\nMassflöde: {data.MassFlow} kg/s\nUtomhustemp: {data.TemperatureOutside} °C\nInnetemp: {data.TemperatureInside} °C\nIsolering: {data.Insulation}\nKostnad: {data.Cost} kWh";
            }
        }
    }
}