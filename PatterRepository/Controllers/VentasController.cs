﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Venta;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatterRepository.Controllers
{
    [Route("api/ventas")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        #region Propiedades
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public VentasController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        #endregion


        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] VentaForCreationDto venta)
        {
            if (venta == null)
            {
                _logger.LogError("OrderForCreationDto object sent from client is null.");
                return BadRequest("OrderForCreationDto object is null");
            }

            var ventaEntity = _mapper.Map<Venta>(venta);

            _repository.Venta.CreateVenta(ventaEntity);
            await _repository.SaveAsync();

            return Ok(new { venta });
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] VentaForUpdateDto ventaForUpdate)
        {
            if (ventaForUpdate == null)
            {
                _logger.LogError("ventaForUpdateDto object sent from client is null.");
                return BadRequest("ventaForUpdateDto object is null");
            }

            var ventaEntity = await _repository.Venta.GetByVentaIDAsync(id, trackChanges: true);
            if (ventaEntity == null)
            {
                _logger.LogInfo($"Venta with id: {id} doesn't exist in the database.");

                return NotFound();
            }

            _mapper.Map(ventaForUpdate, ventaEntity);
            await _repository.SaveAsync();
            return NoContent();
        }




        [HttpGet]
        public async Task<ActionResult> GetAllVentas()
        {
            var getventas = await _repository.Venta.GetAllVentaAsync(trackChanges: false);
            if (getventas == null)
            {
                _logger.LogInfo($"El objecto Ventas no contiene datos. {getventas.Count()}");
                return NotFound();
            }

            var ventas = _mapper.Map<IEnumerable<VentasGetDto>>(getventas);

            //_logger.LogInfo($"Returning {OrderDto.} Categoria.");
            _logger.LogInfo($"El objecto ventas no contiene datos. {ventas.Count()}");
            return Ok(new { ventas });
        }
    }
}
