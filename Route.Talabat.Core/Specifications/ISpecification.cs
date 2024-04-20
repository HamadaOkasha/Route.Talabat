using Route.Talabat.Core.Entities;
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
        //Where
        public Expression<Func<T,bool>>? Criteria { get; set; } //Where --> p=>p.id = 1
        //includes
        public List<Expression<Func<T, object>>> Includes { get; set; }  //Include
        //object instead of BaseEntity because may return orderitems -> list of items not one

        //Sort
        public Expression<Func<T, object>> OrderBy { get; set; } //OrderBy(p=>p.Name)
        public Expression<Func<T, object>> OrderByDesc { get; set; } //OrderByDesc(p=>p.Name)

        //
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; } // if true will paginate

    }
}
