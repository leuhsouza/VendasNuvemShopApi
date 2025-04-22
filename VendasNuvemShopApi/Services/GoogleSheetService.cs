using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Configuration;

namespace VendasNuvemShopApi.Services
{
    public class GoogleSheetsService
    {
        private readonly SheetsService _sheetsService;
        private readonly string _spreadsheetId;
        private readonly string _sheetName;

        public GoogleSheetsService(IConfiguration configuration)
        {
            _spreadsheetId = configuration["GoogleSheets:SpreadsheetId"]!;
            _sheetName = configuration["GoogleSheets:SheetName"] ?? "Vendas";
            var credentialsPath = configuration["GoogleSheets:CredentialsFilePath"];

            GoogleCredential credential;
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(SheetsService.Scope.Spreadsheets);
            }

            _sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Vendas Nuvem Shop"
            });
        }

        public async Task AdicionarVendaAsync(string cliente, string produto, double valor)
        {
            var range = $"{_sheetName}!A:C";
            var valores = new List<IList<object>> {
                new List<object> { cliente, produto, valor }
            };

            var body = new ValueRange { Values = valores };

            var request = _sheetsService.Spreadsheets.Values.Append(body, _spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            await request.ExecuteAsync();
        }
    }
}
