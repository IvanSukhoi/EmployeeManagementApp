using EmployeeManagement.Data.EF.Tests;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;

public class DbContextTest
{
    public static T CreateDbContext<T>() where T : DbContext
    {
        var dbContext = A.Fake<T>();
        A.CallTo(() => dbContext.SaveChanges()).Returns(0);

        var properties = typeof(T).GetProperties(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var property in properties)
        {
            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition().IsAssignableFrom(typeof(IDbSet<>)))
            {
                var innerType = property.PropertyType.GetGenericArguments()[0];
                property.SetValue(dbContext, CreateGeneric(typeof(TestDbSet<>), innerType));
            }
        }

        return dbContext;
    }

    private static object CreateGeneric(Type generic, Type innerType, params object[] args)
    {
        return Activator.CreateInstance(generic.MakeGenericType(innerType), args);
    }
}

public static class DbSetExtentions
{
    public static void AddRange<T>(this IDbSet<T> dbset, List<T> range) where T : class
    {
        foreach (var item in range)
        {
            dbset.Add(item);
        }
    }
}