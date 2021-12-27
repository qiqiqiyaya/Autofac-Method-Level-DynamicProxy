using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;

namespace AutofacDynamicProxyTest.AsyncInterceptor
{
    //[Intercept(typeof(AbpAsyncDeterminationInterceptor<UnitOfWorkInterceptor>))]
    public interface IAsyncTest
    {
        Task<List<int>> GetAll();

        Task Save();

        void Set();
    }
}
