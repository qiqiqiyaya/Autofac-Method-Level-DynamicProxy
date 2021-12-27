using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacDynamicProxyTest
{
    public class CatInterceptor11 : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("猫吃东西之前");
            invocation.Proceed();
            Console.WriteLine("猫吃东西之后");
        }
    }
}
