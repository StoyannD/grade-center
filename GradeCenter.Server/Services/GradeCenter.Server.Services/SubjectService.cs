namespace GradeCenter.Server.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class SubjectService: ISubjectService
    {
        private readonly GradeCenterDbContext dbContext;

        public SubjectService(GradeCenterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.dbContext.Subjects
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetByNameAsync<T>(string name)
        {
            return await this.dbContext.Subjects
                .Where(s => s.Name == name)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.dbContext.Subjects
                .To<T>()
                .ToListAsync();
        }

        public async Task<int> CreateAsync(string name)
        {
            var subject = new Subject
            {
                Name = name,
            };

            await this.dbContext.Subjects.AddAsync(subject);
            await this.dbContext.SaveChangesAsync();

            return subject.Id;
        }

        public async Task<bool> UpdateAsync(int id, string name)
        {
            var subject = await this.dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null)
            {
                return false;
            }

            subject.Name = name;

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subject = await this.dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null)
            {
                return false;
            }

            this.dbContext.Subjects.Remove(subject);
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}