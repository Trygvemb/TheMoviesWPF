﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMoviesWPF.Model.Interfaces
{
    internal interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T entity);

    }
}
