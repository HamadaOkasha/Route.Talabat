﻿using System;
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
			set { pageSize= value > MaxPageSize ? MaxPageSize:value; }
			//to prevent frontEnd from send 1000!
		}
		public int PageIndex { get; set; } = 1;  //intalized of not send
        public string? Sort { get; set; } 
		public int? BrandId { get; set; }
		public int? CategoryId { get; set; }

    }
}