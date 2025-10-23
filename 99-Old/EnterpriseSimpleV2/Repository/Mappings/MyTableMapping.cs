using EnterpriseSimpleV2.Repository.Abstraction.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EnterpriseSimpleV2.Repository.Mappings
{
    public static class MyTableMapping
    {
        public static void Map(this EntityTypeBuilder<MyTable> entity)
        {
            entity.ToTable("MyTable");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name).IsRequired().HasMaxLength(256);
        }
    }
}