using System.Net.Http.Json;

namespace vox_populi_dashboard.Services; // Vérifiez bien votre namespace

public class AuthService
{
    private readonly HttpClient _http;
    public string? Token { get; private set; }

    public AuthService(HttpClient http)
    {
        _http = http;
        _http.Timeout = TimeSpan.FromSeconds(5); // On n'attend pas plus de 5 secondes !
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            // Vérifiez ici si votre API attend "email" ou "username" !
            var requestBody = new { username = username, password = password };

            var response = await _http.PostAsJsonAsync("http://74.161.43.203:5000/login", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                if (result != null && result.TryGetValue("token", out var tokenValue))
                {
                    Token = tokenValue;
                    return true;
                }
            }
            else
            {
                // Pour voir l'erreur dans la console Visual Studio si l'API refuse
                Console.WriteLine($"Erreur API: {response.StatusCode}");
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Crash connexion API: {ex.Message}");
            return false;
        }
    }
}