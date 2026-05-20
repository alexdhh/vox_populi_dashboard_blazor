using System.Net.Http.Headers;
using System.Net.Http.Json;
using vox_populi_dashboard.Models;

namespace vox_populi_dashboard.Services;

public class SimulationService
{
    private readonly HttpClient _http;
    private readonly AuthService _authService;

    public SimulationService(HttpClient http, AuthService authService)
    {
        _http = http;
        _authService = authService;
    }

    public async Task<List<Simulation>> GetSimulationsAsync()
    {
        if (string.IsNullOrEmpty(_authService.Token))
            return new List<Simulation>();

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authService.Token);

        try
        {
            // Retour à la méthode simple qui fonctionnait parfaitement pour le reste des données !
            var simulations = await _http.GetFromJsonAsync<List<Simulation>>("http://74.161.43.203:5000/api/Simulations");
            return simulations ?? new List<Simulation>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération des simulations: {ex.Message}");
            return new List<Simulation>();
        }
    }

    public async Task<Simulation?> GetSimulationByIdAsync(int id)
    {
        if (string.IsNullOrEmpty(_authService.Token))
            return null;

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authService.Token);

        // On crée une option qui ignore la casse (nomAgent = NomAgent = nomagent)
        var options = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        try
        {
            // On passe l'option à la requête !
            return await _http.GetFromJsonAsync<Simulation>($"http://74.161.43.203:5000/api/Simulations/{id}", options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération du détail: {ex.Message}");
            return null;
        }
    }
}