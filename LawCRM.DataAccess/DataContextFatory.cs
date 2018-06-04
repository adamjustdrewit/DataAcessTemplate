using Template.Domain.Data;
using System;
using System.Collections.Generic;

namespace Template.DataAccess
{
    public static class DataContextFatory
    {
        private static Dictionary<Type, Type> _dataContextTypes = new Dictionary<Type, Type>()
        {
            {
               typeof(IFirmDataContext), typeof(FirmDataContext)
            }
        };

        public static T GetFirmDataContext<T>() where T : class, IDataContext
        {
            var returnType = _dataContextTypes[typeof(T)];
            if(returnType != null)
            {
                Activator.CreateInstance(returnType);
            }

            return new FirmDataContext() as T;
        }
    }
}
