using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EdugradeEX
{
    public partial class MainWindow : Window
    {
        private TextBlock resultText;

        public MainWindow()
        {
            InitializeComponent();
            Title = "Beräkningsapp";
            Width = 800;
            Height = 600;

            // Skapa en DockPanel för att organisera layouten
            DockPanel dockPanel = new DockPanel();

            // Skapa en TextBlock för att visa resultat
            resultText = new TextBlock { Text = "Resultat visas här", FontSize = 20, TextAlignment = TextAlignment.Center, Margin = new Thickness(10) };

            // Skapa menyn
            MenuManager menuManager = new MenuManager(resultText);
            dockPanel.Children.Add(menuManager.CreateMainMenu());
            DockPanel.SetDock(menuManager.CreateMainMenu(), Dock.Top);

            // Lägg till TextBlock i DockPanel
            dockPanel.Children.Add(resultText);

            // Försök att ladda bakgrundsbild
            try
            {
                ImageBrush background = new ImageBrush();
                background.ImageSource = new BitmapImage(new Uri("background.jpeg", UriKind.Relative));
                Background = background;

                // Skapa en ContextMenu för bakgrundsbilden
                ContextMenu contextMenu = new ContextMenu();

                // Huvudmenyalternativ "Golvvärme"
                MenuItem golvvarmeParent = new MenuItem { Header = "Golvvärme" };

                // Undermenyalternativ
                MenuItem inputData = new MenuItem { Header = "Skriv in data" };
                MenuItem outputData = new MenuItem { Header = "Skriv ut data" };
                MenuItem loadData = new MenuItem { Header = "Läs in data från JSON" };

                // Lägg till undermenyalternativ under "Golvvärme"
                golvvarmeParent.Items.Add(inputData);
                golvvarmeParent.Items.Add(outputData);
                golvvarmeParent.Items.Add(loadData);

                // Koppla eventhanterare
                inputData.Click += (s, e) => menuManager.InputData_Click(s, e);
                outputData.Click += (s, e) => menuManager.OutputData_Click(s, e);
                loadData.Click += (s, e) => menuManager.LoadData_Click(s, e);

                // Lägg till huvudmenyalternativ i ContextMenu
                contextMenu.Items.Add(golvvarmeParent);

                // Koppla ContextMenu till bakgrundsbilden
                this.ContextMenu = contextMenu;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kunde inte ladda bakgrundsbild: {ex.Message}");
            }

            Content = dockPanel;
        }
    }
}