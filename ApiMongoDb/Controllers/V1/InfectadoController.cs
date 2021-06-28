using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMongoDb.Data.Collections;
using ApiMongoDb.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ApiMongoDb.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : Controller
    {
        private readonly Data.MongoDB _mongoDB;
        private readonly IMongoCollection<Infectado> _infectadosCollection; 

        public InfectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        /// <summary>
        /// Lista todos os registros de infectados
        /// </summary>
        /// <response code="200">Retorna a lista de infectados</response>
        [HttpGet("api/v1/[controller]")]
        public IActionResult ListarInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();

            return Ok(infectados);
        }

        /// <summary>
        /// Insere um novo registro de infectado
        /// </summary>
        /// <param name="dto">Conjunto de dados do infectado</param>
        /// <response code="201">Caso o infectado seja inserido</response>
        [HttpPost("api/v1/[controller]")]
        public IActionResult RegistrarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadosCollection.InsertOne(infectado);

            return StatusCode(201, "Infectado registrado com sucesso");

        }
    }
}