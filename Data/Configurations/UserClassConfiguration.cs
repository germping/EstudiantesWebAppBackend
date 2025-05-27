using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class UserClassConfiguration: IEntityTypeConfiguration<UserClass>
    {
        public void Configure(EntityTypeBuilder<UserClass> builder) 
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x=> x.IdUser).IsRequired();
            builder.Property(x=> x.IdClassSubject).IsRequired();
        }
    }
}
