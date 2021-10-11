﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IShopRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Commit();
    }
}