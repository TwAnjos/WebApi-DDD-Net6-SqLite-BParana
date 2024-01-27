﻿using AutoMapper;
using Domain.InterfacesInternal.InterfacesServices;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WebAPIs.ViewModels;

namespace WebAPIs.Controllers
{
    [ApiController, Route("api/Files"), Produces("application/json")]
    public class FilesController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IServiceFile _IServiceFile;

        public FilesController(IMapper iMapper, IServiceFile serviceFile)
        {
            _IMapper = iMapper;
            _IServiceFile = serviceFile;
        }

        /// <summary>
        /// AddCSV FromForm IFormFileCollection
        /// </summary>
        /// <param name="files"></param>
        /// <returns>IActionResult</returns>
        [Produces("application/json"), HttpPost("/api/Files"), NonAction]
        public async Task<IActionResult> AddCSV([FromForm] IFormFileCollection files)
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return BadRequest("files = null");
                }

                List<UserShawandpartnersViewModel> userShawandpartnersViewModel = new();
                foreach (var file in files)
                {
                    userShawandpartnersViewModel.AddRange(_IServiceFile.ReadCSV<UserShawandpartnersViewModel>(file.OpenReadStream()));
                }

                //convert DTo to Model
                List<UserShawandpartners> userShawandpartnersList = _IMapper.Map<List<UserShawandpartners>>(userShawandpartnersViewModel);

                //insert in db
                await _IServiceFile.AddCSV(userShawandpartnersList);

                return Ok($"csv file was uploaded successfully. {userShawandpartnersViewModel.Count}");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while uploading the csv file. " + ex.Message);
            }
        }

        [Produces("application/json"), HttpGet("/api/Users"), NonAction]
        public async Task<IActionResult> FindUsers([FromQuery] string q)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(q))
                {
                    return NotFound();
                }

                var result = await _IServiceFile.FindUsers(q);
                //O filtro deve procurar correspondências parciais e também não diferenciar maiúsculas de minúsculas.

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}