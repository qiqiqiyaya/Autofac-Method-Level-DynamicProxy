using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDynamicProxyTest.AsyncInterceptor
{
    public class AsyncTest: IAsyncTest
    {
        public AsyncTest()
        {

        }

        /// <summary>
        /// olny the virturl method will be proxy by UnitOfWorkInterceptor
        /// </summary>
        /// <returns></returns>
        [UnitOfWork]
        public virtual Task<List<int>> GetAll()
        {
            Console.WriteLine("异步方法GetAll，设置某某。。。");
            return Task.FromResult(new List<int>() {1, 2, 3, 4});
        }

        public virtual async Task Save()
        {
            Console.WriteLine("异步方法Save，设置某某。。。");
            await Task.Delay(3000);
        }

        public virtual void Set()
        {
            Console.WriteLine("同步方法Set，设置某某。。。");
        }
    }
}
