using ComparativeGraderAPI.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Persistence
{
    public class Seed
    {
        public static async Task SeedData(GradingDataContext context, UserManager<ProfessorUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<ProfessorUser>{
                    new ProfessorUser{
                       // DisplayName = "Bob",
                        UserName = "Kyle",
                        Email = "Kyle@test.com"
                    },
                      new ProfessorUser{
                       // DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                      new ProfessorUser{
                        //DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    }
                };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (!context.Semesters.Any())
            {
                var semesters = new List<Semester>
                {
                    new Semester
                    {
                        EndDate = DateTime.Today,
                        StartDate = DateTime.Today.AddDays(-30)
                    },

                    new Semester
                    {
                        EndDate = DateTime.Today.AddDays(-30),
                        StartDate = DateTime.Today.AddDays(-60)
                    },

                    new Semester
                    {
                        EndDate = DateTime.Today.AddDays(-60),
                        StartDate = DateTime.Today.AddDays(-90)
                    }
                };

                context.Semesters.AddRange(semesters);
                context.SaveChanges();

            }
        }
    }
}
