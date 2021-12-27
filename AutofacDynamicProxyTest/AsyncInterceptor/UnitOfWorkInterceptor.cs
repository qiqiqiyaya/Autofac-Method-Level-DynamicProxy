using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace AutofacDynamicProxyTest.AsyncInterceptor
{
    /// <summary>
    /// This interceptor is used to manage database connection and transactions.
    /// </summary>
    public class UnitOfWorkInterceptor : AbpInterceptorBase
    {
        public UnitOfWorkInterceptor()
        {

        }

        public override void InterceptSynchronous(IInvocation invocation)
        {
            Console.WriteLine("InterceptSynchronous");
            invocation.Proceed();
        }

        protected override async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            Console.WriteLine("InternalInterceptAsynchronous");
            var proceedInfo = invocation.CaptureProceedInfo();
            var method = GetMethodInfo(invocation);

            proceedInfo.Invoke();
            var task = (Task)invocation.ReturnValue;
            await task;
        }


        protected override async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            Console.WriteLine("InternalInterceptAsynchronous<TResult>");
            var proceedInfo = invocation.CaptureProceedInfo();
            var method = GetMethodInfo(invocation);

            proceedInfo.Invoke();
            var taskResult = (Task<TResult>)invocation.ReturnValue;
            return await taskResult;
        }

        private static MethodInfo GetMethodInfo(IInvocation invocation)
        {
            MethodInfo method;
            try
            {
                method = invocation.MethodInvocationTarget;
            }
            catch
            {
                method = invocation.GetConcreteMethod();
            }

            return method;
        }
    }
}
