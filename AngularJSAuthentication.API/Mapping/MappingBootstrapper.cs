using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace AngularJSAuthentication.API.Mapping
{
    public class MappingBootstrapper
    {
        public static void Configure()
        {
            try
            {
                Type profileType = typeof(Profile);

                // Get an instance of each Profile in the executing assembly.
                List<Profile> profiles = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => profileType.IsAssignableFrom(t)
                                && t.GetConstructor(Type.EmptyTypes) != null)
                    .Select(Activator.CreateInstance)
                    .Cast<Profile>().ToList();

                // Initialize AutoMapper with each instance of the profiles found.
                Mapper.Initialize(a => profiles.ForEach(a.AddProfile));
            }
            catch (ReflectionTypeLoadException ex)
            {
                var types = string.Join("|", ex.LoaderExceptions.Select(e => e.Message));
                throw new NotSupportedException(types);
            }
        }
    }
}