using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consid.Logic
{
    public class DatabaseLogic
    {
        public static void CRUD(ConsidContext _dbContext, object entity, string crudMethod)
        {
            switch (crudMethod)
            {
                case "Create":
                    _dbContext.Add(entity);
                    break;
                case "Edit":
                    _dbContext.Entry(entity).State = EntityState.Modified;
                    break;
                case "Delete":
                    _dbContext.Remove(entity);
                    break;
                default:
                    break;
            }

            _dbContext.SaveChangesAsync();
        }
    }
}
