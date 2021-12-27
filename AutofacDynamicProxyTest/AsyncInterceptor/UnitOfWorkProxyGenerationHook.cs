using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;
using Autofac;

namespace AutofacDynamicProxyTest.AsyncInterceptor
{
    public class UnitOfWorkProxyGenerationHook : IProxyGenerationHook
    {
        public void MethodsInspected()
        {
            
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
            
        }

        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            var atts = methodInfo.CustomAttributes;
            // if methodInfo.CustomAttributes has a UnitOfWorkAttribute,it will be proxy by UnitOfWorkInterceptor
            return atts.Any(a => a.AttributeType == typeof(UnitOfWorkAttribute));
        }

        public static MethodInfo GetImplementedMethod(Type targetType, MethodInfo interfaceMethod)
        {
            if (targetType is null) throw new ArgumentNullException(nameof(targetType));
            if (interfaceMethod is null) throw new ArgumentNullException(nameof(interfaceMethod));

            var map = targetType.GetInterfaceMap(interfaceMethod.DeclaringType);
            var index = Array.IndexOf(map.InterfaceMethods, interfaceMethod);
            if (index < 0) return null;

            return map.TargetMethods[index];
    
        }
    }
}
