
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

        builder.ComplexProperty(p => p.Name, b =>
        {
            b.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(ProjectCompanyNames.MAX_LENGTH)
                .HasColumnName("Name");
        });
        builder.ComplexProperty(p => p.CompanyNames, b =>
        {
            b.Property(p => p.CompanyNameForCostumer)
                .IsRequired()
                .HasMaxLength(ProjectCompanyNames.MAX_LENGTH)
                .HasColumnName("CompanyNameForCostumer");

            b.Property(p => p.CompanyNameForExecutor)
                .IsRequired()
                .HasMaxLength(ProjectCompanyNames.MAX_LENGTH)
                .HasColumnName("CompanyNameForExecutor");
        });
        builder.ComplexProperty(p => p.Priority, b =>
        {
            b.IsRequired();
            b.Property(p => p.Priority).HasColumnName("Priority");
        });
        builder.ComplexProperty(p => p.Priority, b =>
        {
            b.IsRequired();
            b.Property(p => p.Priority).HasColumnName("Priority");
        });
        builder.ComplexProperty(p => p.Periods, b =>
        {
            b.Property(p => p.StartDate)
                .IsRequired()
                .HasColumnName("StartDate");

            b.Property(p => p.EndDate)
                .IsRequired()
                .HasColumnName("EndDate");
        });
    }
}
