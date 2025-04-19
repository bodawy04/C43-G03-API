﻿using Domain.Models;

namespace Domain.Contracts;

public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity?> GetAsync(TKey key);
    Task<TEntity?> GetAsync(ISpecifications<TEntity> specifications);
    Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges=false);
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity> specifications);
}
