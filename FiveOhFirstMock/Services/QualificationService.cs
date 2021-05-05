using FiveOhFirstMock.Data;
using FiveOhFirstMock.Data.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Services
{
    public class QualificationService
    {
        private readonly IServiceProvider _services;

        public QualificationService(IServiceProvider services)
        {
            _services = services;
        }

        public async Task SubmitQualificationsAsync(BatchQualificationSubmission submission)
        {
            var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            List<Trooper> users = new();
            foreach(var s in submission.Submissions)
            {
                s.Value.Instructors = submission.Instructors;
                s.Value.Qualifications = submission.Qualification;

                var user = await db.FindAsync<Trooper>(s.Key);

                if (user is null) continue;

                users.Add(user);
                user.QualificationSubmissions.Add(s.Value);
                
                switch(s.Value.Status)
                {
                    case Data.Enums.QualificationStatus.Fail:
                    case Data.Enums.QualificationStatus.Revoked:
                        user.Qualifications ^= submission.Qualification;
                        break;
                    case Data.Enums.QualificationStatus.Pass:
                        user.Qualifications |= submission.Qualification;
                        break;
                }
            }

            await db.SaveChangesAsync();
        }
    }
}
