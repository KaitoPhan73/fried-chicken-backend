using AutoMapper;
using FriedChickenStore.Repository;
using System;
using System.Collections.Generic;

namespace FriedChickenStore.Service
{
    public interface IGenericService<T, U> where T : class where U : class
    {
        IEnumerable<U> ListAll();
        U ListById(int id);
        bool Add(U dto);
        bool Update(int id, U newDto);
        bool Delete(int id);
    }
}
