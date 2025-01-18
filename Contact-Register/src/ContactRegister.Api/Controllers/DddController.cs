using ContactRegister.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactRegister.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DddController : ControllerBase
{
    private readonly IDddService _dddService;

    public DddController(IDddService dddService)
    {
        _dddService = dddService;
	}

	/// <summary>
	/// Busca a lista com todos os DDDs cadastrados na base de dados.
	/// </summary>
	/// <returns>A lista com todos os DDDs, ou uma lista de erros.</returns>
	/// <response code="200">
	///	Busca realizada com sucesso. Exemplo de retorno:
	///		
	///		GET /Ddd/GetDdd
	///		[
	///			{
	///				"code": 69,
	///				"state": "RO",
	///				"region": "ALTO ALEGRE DO PARECIS, VALE DO PARA�SO, VALE DO ANARI, URUP�, THEOBROMA, TEIXEIR�POLIS, SERINGUEIRAS, S�O FRANCISCO DO GUAPOR�, S�O FELIPE D'OESTE, PRIMAVERA DE ROND�NIA, PIMENTEIRAS DO OESTE, PARECIS, NOVA UNI�O, MONTE NEGRO, MIRANTE DA SERRA, MINISTRO ANDREAZZA, ITAPU� DO OESTE, GOVERNADOR JORGE TEIXEIRA, CUJUBIM, CHUPINGUAIA, CASTANHEIRAS, CANDEIAS DO JAMARI, CAMPO NOVO DE ROND�NIA, CACAUL�NDIA, NOVO HORIZONTE DO OESTE, BURITIS, ALTO PARA�SO, ALVORADA D'OESTE, NOVA MAMOR�, S�O MIGUEL DO GUAPOR�, VILHENA, SANTA LUZIA D'OESTE, ROLIM DE MOURA, RIO CRESPO, PRESIDENTE M�DICI, PORTO VELHO, PIMENTA BUENO, OURO PRETO DO OESTE, NOVA BRASIL�NDIA D'OESTE, MACHADINHO D'OESTE, JI-PARAN�, JARU, GUAJAR�-MIRIM, ESPIG�O D'OESTE, COSTA MARQUES, CORUMBIARA, COLORADO DO OESTE, CEREJEIRAS, CACOAL, CABIXI, ARIQUEMES, ALTA FLORESTA D'OESTE"
	///			},
	///			{
	///				"code": 68,
	///				"state": "AC",
	///				"region": "PORTO ACRE, XAPURI, TARAUAC�, SENA MADUREIRA, SENADOR GUIOMARD, SANTA ROSA DO PURUS, RODRIGUES ALVES, RIO BRANCO, PORTO WALTER, PL�CIDO DE CASTRO, MARECHAL THAUMATURGO, MANOEL URBANO, M�NCIO LIMA, JORD�O, FEIJ�, EPITACIOL�NDIA, CRUZEIRO DO SUL, CAPIXABA, BUJARI, BRASIL�IA, ASSIS BRASIL, ACREL�NDIA"
	///			}
	///		]
	/// </response>
	/// <response code="400">Erro ao efetuar a busca</response>
	[HttpGet("[action]")]
	public async Task<IActionResult> GetDdd()
	{
		var result = await _dddService.GetDdd();

		if (result.IsError)
			return BadRequest(result.Errors);

		return Ok(result.Value);
	}

	/// <summary>
	/// Busca as informa��es regionais (estado e lista de cidades) a partir de um DDD informado.
	/// </summary>
	/// <param name="code">C�digo DDD a ser pesquisado.</param>
	/// <returns>A informa��o sobre o DDD, ou uma lista de erros.</returns>
	/// <response code="200">
	///	Busca realizada com sucesso. Exemplo de retorno:
	///		
	///		GET /Ddd/GetDdd/{code}
	///		{
	///			"code": 68,
	///			"state": "AC",
	///			"region": "PORTO ACRE, XAPURI, TARAUAC�, SENA MADUREIRA, SENADOR GUIOMARD, SANTA ROSA DO PURUS, RODRIGUES ALVES, RIO BRANCO, PORTO WALTER, PL�CIDO DE CASTRO, MARECHAL THAUMATURGO, MANOEL URBANO, M�NCIO LIMA, JORD�O, FEIJ�, EPITACIOL�NDIA, CRUZEIRO DO SUL, CAPIXABA, BUJARI, BRASIL�IA, ASSIS BRASIL, ACREL�NDIA"
	///		}
	/// </response>
	/// <response code="400">
	/// Erro ao efetuar a busca. Exemplo de retorno:
	/// 
	///		GET /Ddd/GetDdd/{code}
	///		[
	///			{
	///				"code": "Ddd.ExternalApi",
	///				"description": "DDD n�o encontrado",
	///				"type": 0,
	///				"numericType": 0,
	///				"metadata": null
	///			}
	///		]
	/// </response>
	[HttpGet("[action]/{code:int}")]
	public async Task<IActionResult> GetDdd(int code)
    {
        var result = await _dddService.GetDddByCode(code);

		if (result.IsError)
			return BadRequest(result.Errors);

		return Ok(result.Value);
	}
}