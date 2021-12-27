using System;
using Autofac.Extras.DynamicProxy;

namespace AutofacDynamicProxyTest
{
    [Intercept(typeof(CatInterceptor11))]
    public class Cat:ICat
    {
        public void Eat()
        {
            Console.WriteLine("猫在吃东西");
        }
    }
}