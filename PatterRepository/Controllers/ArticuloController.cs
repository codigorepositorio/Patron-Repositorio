﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatterRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repoWrapper;

        public ArticuloController(ILoggerManager logger, IRepositoryWrapper repoWrapper)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
        }
        
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInfo("Fetching all Account from the StoraSge");
            //var AccountList = _repoWrapper.Owner.FindCondition(x => x.Address.Contains("ICAS"));
            var AccountList = await _repoWrapper.Articulo.GetAllArticuloAsync();       
            _logger.LogInfo($"Returning {AccountList.Count()} Account.");

            var Categoria = await _repoWrapper.Categoria.GetAllCategoriaAsync();
            return Ok(AccountList);
        }

    }
}