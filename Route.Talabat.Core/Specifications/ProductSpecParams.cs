using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications
{
    public class ProductSpecParams
    {
		private const int MaxPageSize=10;
	
		private int pageSize=5;  //intalized of not send
        public int PageSize
        {
			get { return pageSize; }
			set { pageSize= (value > MaxPageSize || value<=0)  ? MaxPageSize : value; }
			//to prevent frontEnd from send 1000!
		}
        
		private int pageIndex = 1;
        public int PageIndex { 
			get { return pageIndex; }
			set { pageIndex = value < pageIndex ? pageIndex : value; } 
		}
        public string? Sort { get; set; } 
		public int? BrandId { get; set; }
		public int? CategoryId { get; set; }

    }
}
