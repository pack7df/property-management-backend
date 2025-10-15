using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Domain
{
    public class OrderParameterNotRecognizedException : Exception
    {
        private Type entity;
        private string column;
        public OrderParameterNotRecognizedException(Type entity, string column)
        {
            this.entity = entity;
            this.column = column;
        }
    }
}
