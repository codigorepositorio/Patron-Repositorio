﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using LoggerService;
using Microsoft.AspNetCore.Mvc;

namespace PatterRepository.Controllers
{
    [Route("api/Owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            var owner = await _repository.Owner.GetAllOwnersAsync();
            _logger.LogInfo($"Returned all owners from database.");
           
            var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owner);
            _logger.LogInfo($"Returning {ownersResult.Count()} Owners.");
            return Ok(ownersResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerById(int id)
        {
            _logger.LogInfo($"Returned owner with id: {id}");
            var owner = await _repository.Owner.GetOwnerByIdAsync(id, trackChanges: false);

            var ownerResult = _mapper.Map<OwnerDto>(owner);
            _logger.LogInfo($"Returning {ownerResult} owner.");
            return Ok(ownerResult);
        }

        [HttpGet("{id}/account")]
        public async Task<IActionResult> GetOwnerWithDetails(int id)
        {
            _logger.LogInfo($"Returned owner with id: {id}");
            var owner = await _repository.Owner.GetOwnerWithDetailsAsync(id, trackChanges: false);

            var ownerResult = _mapper.Map<OwnerDto>(owner);
            _logger.LogInfo($"Returning ,{ownerResult} owner/account.");
            return Ok(ownerResult);
            
        }

        [HttpGet("{id}/accounts")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _repository.Account.GetAllOwnersAsync(trackChanges:false);
            _logger.LogInfo($"Returned all owners from database.");
            var ownersResult = _mapper.Map<IEnumerable<AccountDto>>(account);
            _logger.LogInfo($"Returning {ownersResult.Count()} Owners.");
            return Ok(ownersResult);
        }
    }
}