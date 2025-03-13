using System;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace EdugradeEX
{
    public class JsonManager
    {
        private string jsonPath = "data.json"; // Sökväg till JSON-filen

        // Spara data till JSON-fil
        public void SaveToJson(FloorHeatingData data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data);
                File.WriteAllText(jsonPath, json);
                MessageBox.Show("Data sparad till JSON-fil!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fel vid sparande: {ex.Message}");
            }
        }

        // Läs data från JSON-fil
        public FloorHeatingData LoadFromJson()
        {
            try
            {
                if (File.Exists(jsonPath))
                {
                    string json = File.ReadAllText(jsonPath);
                    return JsonSerializer.Deserialize<FloorHeatingData>(json);
                }
                else
                {
                    MessageBox.Show("Ingen JSON-fil hittades.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fel vid läsning: {ex.Message}");
                return null;
            }
        }
    }
}