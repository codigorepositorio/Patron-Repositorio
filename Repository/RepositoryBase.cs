﻿using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RepositoryContext RepositoryContext { get; set; }
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

        //public IQueryable<T> FindAll()
        //{
        //    return RepositoryContext.Set<T>().AsNoTracking();
        //}

        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ?
        RepositoryContext.Set<T>().AsNoTracking() : RepositoryContext.Set<T>();

        //public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        //       bool trackChanges) =>!trackChanges ?RepositoryContext.Set<T>().Where(expression)
        //       .AsNoTracking() :RepositoryContext.Set<T>().Where(expression);


        public IQueryable<T> FindCondition(Expression<Func<T, bool>> expression,
                         bool trackChanges) =>
                         !trackChanges ?
                         RepositoryContext.Set<T>()
                         .Where(expression)
                         .AsNoTracking() :
                         RepositoryContext.Set<T>()
                         .Where(expression);

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }
    }
}
