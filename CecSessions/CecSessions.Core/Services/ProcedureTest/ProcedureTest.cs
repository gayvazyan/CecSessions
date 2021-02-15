using CecSessions.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CecSessions.Core.Services.ProcedureTest
{
    public class ProcedureTest : IProcedureTest
    {
        private readonly CecSessionsContext _context;
        public ProcedureTest(CecSessionsContext context)
        {
            _context = context;
        }
        public List<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = new List<IdentityRole>();

            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "dbo.GetRoles";
            command.Connection = connection;
          //  command.Parameters.Add(new SqlParameter("@newsId", newsId));
            connection.Open();
            command.ExecuteNonQuery();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var role = new IdentityRole()
                    {
                        Id = reader["Id"].ToString(),
                        Name = reader["Name"].ToString(),
                        NormalizedName = reader["NormalizedName"].ToString(),
                        ConcurrencyStamp = reader["ConcurrencyStamp"].ToString(),
                    };
                    roles.Add(role);
                }
            }
            command.Connection.Close();
            connection.Close();

            return roles;
        }

        public IdentityRole GetRole(string id)
        {
            throw new NotImplementedException();
        }
    }
}
