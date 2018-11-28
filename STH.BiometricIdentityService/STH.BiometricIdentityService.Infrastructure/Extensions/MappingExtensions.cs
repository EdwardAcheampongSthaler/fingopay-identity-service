using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using PagedList;

namespace STH.BiometricIdentityService.Infrastructure.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IPagedList<TResult> MapToPagedList<TResult>(this IPagedList self)
        {
            if (self == null)
                throw new ArgumentNullException();

            var subset = (List<TResult>) Mapper.Map(self, self.GetType(), typeof (List<TResult>));
            return new StaticPagedList<TResult>(subset, self.PageNumber, self.PageSize, self.TotalItemCount);
        }

        public static IList<TResult> MapTo<TResult>(this IEnumerable self)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (List<TResult>)Mapper.Map(self, self.GetType(), typeof(List<TResult>));
        }


        public static IList<TResult> MapTo<TResult>(this IList self)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (List<TResult>)Mapper.Map(self, self.GetType(), typeof(List<TResult>));
        }

        public static TResult MapTo<TResult>(this object self)
        {
            if (self == null)
                return default(TResult);

            return (TResult)Mapper.Map(self, self.GetType(), typeof(TResult));
        }

        public static TResult MapPropertiesToInstance<TResult>(this object self, TResult value)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (TResult)Mapper.Map(self, value, self.GetType(), typeof(TResult));
        }
        
    }
}