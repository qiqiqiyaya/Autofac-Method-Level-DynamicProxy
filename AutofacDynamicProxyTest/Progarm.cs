using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using AutofacDynamicProxyTest.AsyncInterceptor;
using Castle.DynamicProxy;

namespace AutofacDynamicProxyTest
{
    public class Progarm
    {
        static void Main(string[] args)
        {
            /*ICat icat=new Cat();

            var catProxy=new CatProxy(icat);

            catProxy.Eat();*/


            // 第一种
            //var builder = new ContainerBuilder();

            //builder.RegisterType<CatInterceptor11>();//注册拦截器
            //builder.RegisterType<Cat>().As<ICat>().InterceptedBy(typeof(CatInterceptor11))
            //    .EnableInterfaceInterceptors();//注册Cat并为其添加拦截器

            //var container = builder.Build();

            //var cat = container.Resolve<ICat>();

            //cat.Eat();


            // 第二种
            //var builder = new ContainerBuilder();

            //builder.RegisterType<CatInterceptor11>();//注册拦截器

            //// Cat 类上面要写 [Intercept(typeof(CatInterceptor11))] 注解
            //builder.RegisterType<Cat>().As<ICat>().EnableInterfaceInterceptors();//注册Cat并为其添加拦截器
            //var container = builder.Build();
            //var cat = container.Resolve<ICat>();
            //cat.Eat();


            //var builder = new ContainerBuilder();

            //builder.RegisterType<CatInterceptor>();//注册拦截器
            //builder.RegisterType<Cat>().As<ICat>();//注册Cat
            //builder.RegisterType<CatOwner>().InterceptedBy(typeof(CatInterceptor))
            //    .EnableClassInterceptors(ProxyGenerationOptions.Default, additionalInterfaces: typeof(ICat));//注册CatOwner并为其添加拦截器和接口
            //var container = builder.Build();

            //var cat = container.Resolve<CatOwner>();//获取CatOwner的代理类

            //cat.GetType().GetMethod("Eat").Invoke(cat, null);//因为我们的代理类添加了ICat接口，所以我们可以通过反射获取代理类的Eat方法来执行

            // 3.给类绑定拦截器
            //try
            //{

            //    // 异步方法拦截器
            //    var builder = new ContainerBuilder();
            //    builder.RegisterGeneric(typeof(AbpAsyncDeterminationInterceptor<>)).InstancePerDependency();
            //    builder.Register(c => new UnitOfWorkInterceptor()).InstancePerDependency();
            //    builder.Register(c => new AuditLogInterceptor()).InstancePerDependency();

            //    //将拦截器绑定到整个类上，该类上的相关方法执行之前都会执行拦截器
            //    builder.RegisterType<AsyncTest>().As<IAsyncTest>()
            //        .EnableInterfaceInterceptors()
            //        .InterceptedBy(typeof(AbpAsyncDeterminationInterceptor<UnitOfWorkInterceptor>))
            //        .InterceptedBy(typeof(AbpAsyncDeterminationInterceptor<AuditLogInterceptor>));

            //    var container = builder.Build();
            //    var test = container.Resolve<IAsyncTest>();

            //    var aa = test.GetAll().Result;
            //    test.Save().Wait();
            //    test.Set();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //}

            // 4.实现了特定特性的方法或类，才会绑定到拦截器
            try
            {
                // 异步方法拦截器
                var builder = new ContainerBuilder();
                builder.RegisterGeneric(typeof(AbpAsyncDeterminationInterceptor<>)).InstancePerDependency();
                builder.Register(c => new UnitOfWorkInterceptor()).InstancePerDependency();
                builder.Register(c => new AuditLogInterceptor()).InstancePerDependency();

                var proxyGenerationOptions = new ProxyGenerationOptions(new UnitOfWorkProxyGenerationHook());
                //将拦截器绑定到整个类上，该类上的相关方法执行之前都会执行拦截器
                builder.RegisterType<AsyncTest>().As<IAsyncTest>()
                    .EnableClassInterceptors(proxyGenerationOptions)
                    //在注册服务时，扫描AsyncTest中所有方法，
                    //只有符合 UnitOfWorkProxyGenerationHook.ShouldInterceptMethod 规则的方法，在执行时，才会执行代理类
                    //注意EnableClassInterceptors ，扫描的是 AsyncTest。
                    //如果使用EnableInterfaceInterceptors，只会在程序运行时，且在解析IAsyncTest服务时
                    //才会扫描“IAsyncTest”中的所有方法。
                    .InterceptedBy(typeof(AbpAsyncDeterminationInterceptor<UnitOfWorkInterceptor>));
                
                var container = builder.Build();
                AutofacContainerObject.Container = container;
                

                var test = container.Resolve<IAsyncTest>();

                var aa = test.GetAll().Result;
                test.Save().Wait();
                test.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.Read();
        }
    }
}
