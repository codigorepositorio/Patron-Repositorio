﻿using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Categoria>> GetAllCategoriaAsync(bool trackChanges) => await
                 FindAll(trackChanges)
                 .OrderBy(c => c.Nombre)
                .ToListAsync();
        public async Task<IEnumerable<Categoria>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges) => await
            FindByCondition(c => ids.Contains(c.Id), trackChanges)         
            .ToListAsync();

        public async Task<Categoria> GetCategoriaAsync(int categoriaId, bool trackChanges) => await
            FindByCondition(c => c.Id.Equals(categoriaId), trackChanges).Include(a => a.articulos)
            .SingleOrDefaultAsync();
        public void CreateCategoria(Categoria categoria) => Create(categoria);
        public void DeleteCategoria(Categoria categoria) => Delete(categoria);

    } 
}
