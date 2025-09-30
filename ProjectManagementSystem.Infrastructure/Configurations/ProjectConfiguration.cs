
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.ValueObjects.Project;

namespace ProjectManagementSystem.Infrastructure.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(p => p.Id);
        builder.OwnsOne(p => p.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(ProjectName.MAX_LENGTH)
                .HasColumnName("Name");
            nameBuilder.HasIndex(n => n.Value).IsUnique();
        });


        builder.OwnsOne(p => p.CompanyNames, companyNamesBuilder =>
        {
            companyNamesBuilder.Property(cn => cn.CompanyNameForCostumer)
                .IsRequired()
                .HasMaxLength(ProjectCompanyNames.MAX_LENGTH)
                .HasColumnName("CompanyNameForCostumer");

            companyNamesBuilder.Property(cn => cn.CompanyNameForExecutor)
                .IsRequired()
                .HasMaxLength(ProjectCompanyNames.MAX_LENGTH)
                .HasColumnName("CompanyNameForExecutor");
        });

        builder.OwnsOne(p => p.Priority, priorityBuilder =>
        {
            priorityBuilder.Property(pr => pr.Value)
                .IsRequired()
                .HasColumnName("Priority");
        });

        builder.OwnsOne(p => p.Periods, periodsBuilder =>
        {
            periodsBuilder.Property(pd => pd.StartDate)
                .IsRequired()
                .HasColumnName("StartDate");

            periodsBuilder.Property(pd => pd.EndDate)
                .IsRequired()
                .HasColumnName("EndDate");
        });
    }
}
