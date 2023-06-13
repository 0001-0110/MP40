﻿using MP40.DAL.Models;

namespace MP40.DAL.Repositories
{
    public interface IRepository
    {

    }

    public interface IRepository<T> : IRepository where T : class, IDalModel
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetWhere(Predicate<T> predicate);

        T? GetById(int id);

        void Create(T entity);

        void Edit(int id, T entity);

        void Delete(T entity);
    }
}
