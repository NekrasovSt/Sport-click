﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Данные.Модели
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class СпортКлик : DbContext
    {
        public СпортКлик()
            : base("name=СпортКлик")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Категории> Категории { get; set; }
        public virtual DbSet<Товар> Товар { get; set; }
        public virtual DbSet<Производители> Производители { get; set; }
        public virtual DbSet<СтатусЗаказа> СтатусЗаказа { get; set; }
        public virtual DbSet<СвойствоЗаказа> СвойствоЗаказа { get; set; }
        public virtual DbSet<Заказ> Заказ { get; set; }
        public virtual DbSet<ИсторияЗаказа> ИсторияЗаказа { get; set; }
        public virtual DbSet<Отзыв> Отзыв { get; set; }
    }
}
