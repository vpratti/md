using System.Collections.Generic;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.DbRepositories
{
    public interface IActivityTemplatesRepository
    {
        List<ActivityTemplate> GetTemplates();
    }
}