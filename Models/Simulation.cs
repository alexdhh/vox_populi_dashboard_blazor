using System.Text.Json.Serialization;

namespace vox_populi_dashboard.Models
{
    public class Simulation
    {
        public int SimulationId { get; set; }
        public string? Titre { get; set; }
        public string? Discours { get; set; }
        public int NbAgent { get; set; }
        public DateTime DateSimulation { get; set; }
        public List<Agent> Agents { get; set; } = new();
    }

    public class Agent
    {
        public int AgentId { get; set; }
        [JsonPropertyName("nom")] // API renvoie "nom"
        public string? NomAgent { get; set; }

        [JsonPropertyName("prenom")] // API renvoie "prenom"
        public string? PrenomAgent { get; set; }
        public DateTime? DateCreation { get; set; }

        public string? OriantationPolitique { get; set; }
        public string? OrientationPolitique { get; set; }
        public int NiveauEmotion { get; set; }
        public List<Prediction> Predictions { get; set; } = new();
    }

    public class Prediction
    {
        public int PredictionId { get; set; }
        public string? Reaction { get; set; }
        public string? Contenu { get; set; }
    }
}