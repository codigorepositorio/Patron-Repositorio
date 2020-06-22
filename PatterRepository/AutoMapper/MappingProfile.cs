﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System.Linq;
namespace PatterRepository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<AccountForCreationDto, Account>();
            CreateMap<Account, AccountDto>()
             .ForMember(c => c.Owner, opt => opt.MapFrom(x => x.owner.Name));

            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerForCreationDto, Owner>();
            CreateMap<OwnerForUpdateDto, Owner>();


            #region Mapeo Objeto Articulo

            CreateMap<Articulo, ArticuloDto>()
                .ForMember(c => c.Categoria, opt => opt.MapFrom(x => x.Categoria.Nombre));
            CreateMap<ArticuloForCreationDto, Articulo>();
            CreateMap<ArticuloForUpdateDto, Articulo>();

            #endregion

            #region Mapeo Objeto Categoria
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<CategoriaForCreationDto, Categoria>();

            CreateMap<CategoriaForUpdateDto, Categoria>()
                .ForMember(ca => ca.Articulos, opt => opt.Ignore())

                   .AfterMap((clienteCRUD, clienteDB) =>
                   {
                       var ids = clienteCRUD.Articulos.Select(dir => dir.ArticuloId).ToList();
                       // delete - ids = Array [1,3,5]
                       var articulosBorrar = clienteDB.Articulos.Where(dir => !ids.Contains(dir.Id)).ToList();
                       foreach (var artB in articulosBorrar)
                           clienteDB.Articulos.Remove(artB);

                       // update
                       var articulosActualizar = clienteDB.Articulos.Where(dir => ids.Contains(dir.Id) && dir.Id > 0).ToList();
                       foreach (var dirU in articulosActualizar)
                       {
                           dirU.CategoriaId = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().CategoriaId;
                           dirU.Codigo = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().Codigo;
                           dirU.Nombre = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().Nombre;
                           dirU.PrecioVenta = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().PrecioVenta;
                           dirU.Stock = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().Stock;
                           dirU.Descripcion = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().Descripcion;
                           dirU.Condicion = clienteCRUD.Articulos.Where(cat => cat.ArticuloId == dirU.Id).First().Condicion;
                       }

                  });
                       #endregion

                   #region Mapeo Objeto Persona
                       CreateMap<Persona, PersonaDto>();
            CreateMap<PersonaForCreationDto, Persona>();
            CreateMap<PersonaForUpdateDto, Persona>();
            #endregion
        }
    }
}
