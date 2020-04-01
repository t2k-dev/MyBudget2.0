using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBudget.Domain;

namespace MyBudget.Data
{
    public class ApplicationContext : IdentityDbContext
    {
        #region DbSets

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        #endregion

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserCategory>()
                .HasKey(u => new { u.UserId, u.CategoryID });

            #region Seeding database.

            builder.Entity<Category>().HasData(
                // System categories.
                new Category() { ID = 1, Name = "Без категории", IsSpendingCategory = false, IsSystem = true },
                new Category() { ID = 2, Name = "Без категории", IsSpendingCategory = true, IsSystem = true },
                new Category() { ID = 3, Name = "Взять в долг", IsSpendingCategory = false, IsSystem = true },
                new Category() { ID = 4, Name = "Дать в долг", IsSpendingCategory = true, IsSystem = true },
                new Category() { ID = 5, Name = "Остаток", IsSpendingCategory = false, IsSystem = true },
                new Category() { ID = 6, Name = "Отдать долг", IsSpendingCategory = true, IsSystem = true },
                new Category() { ID = 7, Name = "Получить долг", IsSpendingCategory = false, IsSystem = true },
                new Category() { ID = 8, Name = "Пополнить цель", IsSpendingCategory = true, IsSystem = true },
                
                //Default categories.
                new Category() { ID = 20, Name = "Зарплата", IsSpendingCategory = false, IsSystem = false },
                new Category() { ID = 21, Name = "Бизнес", IsSpendingCategory = false, IsSystem = false },
                new Category() { ID = 22, Name = "Премия", IsSpendingCategory = false, IsSystem = false },
                
                new Category() { ID = 23, Name = "Здоровье", IsSpendingCategory = true, IsSystem = false },
                new Category() { ID = 24, Name = "Жилье", IsSpendingCategory = true, IsSystem = false },
                new Category() { ID = 25, Name = "Авто", IsSpendingCategory = true, IsSystem = false },
                new Category() { ID = 26, Name = "Транспорт", IsSpendingCategory = true, IsSystem = false },
                new Category() { ID = 27, Name = "Спорт", IsSpendingCategory = true, IsSystem = false },
                new Category() { ID = 28, Name = "Еда и продукты", IsSpendingCategory = true, IsSystem = false },
                new Category() { ID = 29, Name = "Клубы и бары", IsSpendingCategory = true, IsSystem = false },
                new Category() { ID = 30, Name = "Развлечения", IsSpendingCategory = true, IsSystem = false },
                new Category() { ID = 31, Name = "Одежда", IsSpendingCategory = true, IsSystem = false },
                new Category() { ID = 32, Name = "Связь", IsSpendingCategory = true, IsSystem = false },
                new Category() { ID = 40, Name = "Подарки", IsSpendingCategory = true, IsSystem = false }
                );
            builder.Entity<Category>().Property(c => c.ID).HasIdentityOptions(startValue: 50);


            builder.Entity<Currency>().HasData(
                new Currency() { ID = 1, Name = "Тенге", Symbol = "₸" },
                new Currency() { ID = 2, Name = "Доллар США", Symbol = "$" },
                new Currency() { ID = 3, Name = "Евро", Symbol = "€" },
                new Currency() { ID = 4, Name = "Российский рубль", Symbol = "₽" },
                new Currency() { ID = 5, Name = "Фунт стерлингов", Symbol = "£" }
                );
            #endregion

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=azsxdc!2");
        }
    }
}
