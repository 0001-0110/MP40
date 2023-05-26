﻿namespace MP40.DAL.Repositories
{
    internal interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        
        T? GetById(int id);

        void Create(T entity);

        void Edit(T entity);

        void Delete(T entity);
    }
}
