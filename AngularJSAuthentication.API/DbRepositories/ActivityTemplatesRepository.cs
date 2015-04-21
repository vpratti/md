using System;
using System.Collections.Generic;
using System.Linq;
using AngularJSAuthentication.API.DbContexts;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.DbRepositories
{
    public class ActivityTemplatesRepository : IActivityTemplatesRepository, IDisposable
    {
        private readonly LookupContext _context;

        public ActivityTemplatesRepository()
        {
            _context = new LookupContext();
        }

        public List<ActivityTemplate> GetTemplates()
        {
            return _context.ActivityTemplates.ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}