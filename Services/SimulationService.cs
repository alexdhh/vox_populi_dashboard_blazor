using System.Net.Http.Headers;
using System.Net.Http.Json;
using vox_populi_dashboard.Models; // Vérifiez que le namespace correspond à votre dossier Models

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
        // On vérifie qu'on a bien notre token
        if (string.IsNullOrEmpty(_authService.Token))
            return new List<Simulation>();

        // On injecte le token dans l'en-tête de la requête HTTP
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authService.Token);

        try
        {
            // Appel à la route GET /api/Simulations
            var simulations = await _http.GetFromJsonAsync<List<Simulation>>("http://74.161.43.203:5000/api/Simulations");
            return simulations ?? new List<Simulation>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération des simulations: {ex.Message}");
            return new List<Simulation>();
        }
    }
}