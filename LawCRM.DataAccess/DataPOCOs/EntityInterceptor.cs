using Castle.DynamicProxy;
using Template.Domain.Base;
using System;

namespace Template.DataAccess.DataPOCOs
{
    internal class EntityInterceptor<T> : IInterceptor where T : Entity
    {
       public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            if (invocation.Method.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase))
            {
                var methodName = invocation.Method.Name;
                ((T)invocation.InvocationTarget).OnPropertyChanged(methodName);               
            }
        }        
    }

    internal class ChildEntityInterceptor<T> : IInterceptor where T : ChildEntity
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            if (invocation.Method.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase))
            {
                var methodName = invocation.Method.Name;
                ((T)invocation.InvocationTarget).OnPropertyChanged(methodName);
            }
        }
    }
}
