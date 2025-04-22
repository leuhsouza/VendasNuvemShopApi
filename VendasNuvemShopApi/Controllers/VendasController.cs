using Microsoft.AspNetCore.Mvc;
using VendasNuvemShopApi.DTOs;
using VendasNuvemShopApi.Services;

namespace VendasNuvemShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly NuvemShopService _nuvemShopService;
        private readonly GoogleSheetsService _googleSheetsService;

        public VendasController(NuvemShopService nuvemShopService, GoogleSheetsService googleSheetsService)
        {
            _nuvemShopService = nuvemShopService;
            _googleSheetsService = googleSheetsService;
        }

        [HttpPost("registrar-venda")]
        public async Task<IActionResult> RegistrarVenda([FromBody] VendaDto venda)
        {
            await _googleSheetsService.AdicionarVendaAsync(venda.Cliente, venda.Produto, venda.Valor);
            return Ok("Venda registrada com sucesso no Google Sheets!");
        }

        [HttpGet("buscar-vendas-nuvemshop")]
        public async Task<IActionResult> BuscarVendas()
        {
            var json = await _nuvemShopService.BuscarVendasAsync();
            return Ok(json); 
        }
    }
}
