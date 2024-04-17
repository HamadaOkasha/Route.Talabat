﻿using Route.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications
{
    public interface ISpecification<T> where T :BaseEntity
    {  
        
        public Expression<Func<T,bool>>? Criteria { get; set; } //Where --> p=>p.id = 1
        public List<Expression<Func<T, object>>> Includes { get; set; }  //Include
          //object instead of BaseEntity because may return orderitems -> list of items not one
    }
}