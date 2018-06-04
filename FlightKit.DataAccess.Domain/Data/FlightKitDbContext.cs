using System;
using FlightKit.DataAccess.Domain.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace FlightKit.DataAccess.Domain
{
    public partial class FlightKitDbContext : DbContext
    {
        public FlightKitDbContext()
        {
        }

        public FlightKitDbContext(DbContextOptions<FlightKitDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Risk_AdditionDate> AdditionDates { get; set; }
        public virtual DbSet<Risk_Comment> Comments { get; set; }
        public virtual DbSet<Risk_CommentSegment> CommentSegments { get; set; }
        public virtual DbSet<Risk_ConstructionTypeCode> ConstructionTypeCodes { get; set; }
        public virtual DbSet<Risk_Exposure> Exposures { get; set; }
        public virtual DbSet<Risk_FireDivisionRisk> FireDivisionRisks { get; set; }
        public virtual DbSet<Risk_FloorsAndRoof> FloorsAndRoofs { get; set; }
        public virtual DbSet<Risk_InternalProtection> InternalProtections { get; set; }
        public virtual DbSet<Risk_Occupant> Occupants { get; set; }
        public virtual DbSet<Risk_OccupantHazard> OccupantHazards { get; set; }
        public virtual DbSet<Risk_OccupantLevel> OccupantLevels { get; set; }
        public virtual DbSet<Risk_ProtectionSafeguard> ProtectionSafeguards { get; set; }
        public virtual DbSet<Risk_ProtectionSafeguardCode> ProtectionSafeguardCodes { get; set; }
        public virtual DbSet<Risk_Report> Reports { get; set; }
        public virtual DbSet<Risk_ReportAddress> ReportAddresses { get; set; }
        public virtual DbSet<Risk_ReportAttachment> ReportAttachments { get; set; }
        public virtual DbSet<Risk_ReportBuildingInformation> ReportBuildingInformations { get; set; }
        public virtual DbSet<Risk_ReportDateTypeCode> ReportDateTypeCodes { get; set; }
        public virtual DbSet<Risk_ReportHazard> ReportHazards { get; set; }
        public virtual DbSet<Risk_ReportPhoto> ReportPhotos { get; set; }
        public virtual DbSet<Risk_ReportRelatedDate> ReportRelatedDates { get; set; }
        public virtual DbSet<Risk_RetiredOccupantNumber> RetiredOccupantNumbers { get; set; }
        public virtual DbSet<Risk_SecondaryConstruction> SecondaryConstructions { get; set; }
        public virtual DbSet<Risk_Wall> Walls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=VAE1VWCPRSQLT1.VERISKT.LOCAL,3341;Initial Catalog=CommercialProperty;Persist Security Info=True;User ID=PIDFLTKITT;Password=Thus8sap");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Risk_AdditionDate>(entity =>
            {
                entity.HasKey(e => e.AdditionDateIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("AdditionDates", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.AdditionDate })
                    .HasName("AdditionDates_ix00")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.AdditionDateIdentifier).ValueGeneratedNever();

                entity.Property(e => e.AdditionDate)
                    .HasColumnName("AdditionDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.AdditionDates)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_ReportAdditionDates_FK");
            });

            modelBuilder.Entity<Risk_Comment>(entity =>
            {
                entity.HasKey(e => e.CommentIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("Comments", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.CommentDateTime })
                    .HasName("Comments_ix01")
                    .ForSqlServerIsClustered();

                entity.Property(e => e.CommentIdentifier).ValueGeneratedNever();

                entity.Property(e => e.CommentBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CommentDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.CommentType)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.Comments)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_Comments_FK");
            });

            modelBuilder.Entity<Risk_CommentSegment>(entity =>
            {
                entity.HasKey(e => e.CommentSegmentIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CommentSegments", "Risks");

                entity.HasIndex(e => new { e.CommentIdentifier, e.CommentSegmentSequenceNumber })
                    .HasName("CommentSegments_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.CommentSegmentIdentifier).ValueGeneratedNever();

                entity.Property(e => e.CommentSegmentText)
                    .IsRequired()
                    .HasMaxLength(7950)
                    .IsUnicode(false);

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentSegments)
                    .HasForeignKey(d => d.CommentIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comments_CommentSegments_FK");
            });

            modelBuilder.Entity<Risk_ConstructionTypeCode>(entity =>
            {
                entity.HasKey(e => e.ConstructionTypeCodeValue);

                entity.ToTable("ConstructionTypeCodes", "Risks");

                entity.Property(e => e.ConstructionTypeCodeValue)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ConstructionTypeCodeDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Risk_Exposure>(entity =>
            {
                entity.HasKey(e => e.ExposureIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("Exposures", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.ExposureIdentifier })
                    .HasName("Exposures_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.ExposureIdentifier).ValueGeneratedNever();

                entity.Property(e => e.ExposureConstructionType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ExposureDirection)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ExposureProtectionType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.OccupancyType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PartyProtectionType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PassageCombustibleType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PassageProtectionType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.RiskConstructionType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.RiskProtectionType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.Exposures)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_Exposures_FK");
            });

            modelBuilder.Entity<Risk_FireDivisionRisk>(entity =>
            {
                entity.HasKey(e => e.FireDivisionRiskIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("FireDivisionRisks", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.FireDivisionRiskIdentifier })
                    .HasName("FireDivisionRisks_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.FireDivisionRiskIdentifier).ValueGeneratedNever();

                entity.Property(e => e.RiskId)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.FireDivisionRisks)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_FireDivisionRisks_FK");
            });

            modelBuilder.Entity<Risk_FloorsAndRoof>(entity =>
            {
                entity.HasKey(e => e.FloorAndRoofIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("FloorsAndRoofs", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.FloorAndRoofIdentifier })
                    .HasName("FloorsAndRoofs_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.FloorAndRoofIdentifier).ValueGeneratedNever();

                entity.Property(e => e.Bgiitype)
                    .HasColumnName("BGIIType")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CombustibleType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ConstructionType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.DeckConstruction)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.FireResistanceListing)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.LevelType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.SupportType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.FloorsAndRoofs)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_FloorsAndRoofs_FK");
            });

            modelBuilder.Entity<Risk_InternalProtection>(entity =>
            {
                entity.HasKey(e => e.InternalProtectionIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("InternalProtections", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.InternalProtectionIdentifier })
                    .HasName("InternalProtections_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.InternalProtectionIdentifier).ValueGeneratedNever();

                entity.Property(e => e.AlarmSignalDestinationType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.AutoFireAlarmInspectionAndTestFrequencyType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.AutoFireAlarmSystemCoverageType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LimitedSupplyAlarmType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PartialSupplyAlarmType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.SprinkleredStationAlarmType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.SprinkleredStationType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.TroubleSignalDestinationType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.WatchmanType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.InternalProtections)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_InternalProtections_FK");
            });

            modelBuilder.Entity<Risk_Occupant>(entity =>
            {
                entity.HasKey(e => e.OccupantIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("Occupants", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.OccupantIdentifier })
                    .HasName("Occupants_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.OccupantIdentifier).ValueGeneratedNever();

                entity.Property(e => e.BasicGroup2Symbol)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CommercialStatisticalPlan)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.IsBaseGroupIioverride).HasColumnName("IsBaseGroupIIOverride");

                entity.Property(e => e.OccupantComment)
                    .HasMaxLength(1200)
                    .IsUnicode(false);

                entity.Property(e => e.OccupantName)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SuiteNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.Occupants)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_Occupants_FK");
            });

            modelBuilder.Entity<Risk_OccupantHazard>(entity =>
            {
                entity.HasKey(e => e.OccupantHazardIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("OccupantHazards", "Risks");

                entity.HasIndex(e => new { e.OccupantIdentifier, e.OccupantHazardIdentifier })
                    .HasName("OccupantHazards_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.OccupantHazardIdentifier).ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .HasMaxLength(650)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Occupant)
                    .WithMany(p => p.OccupantHazards)
                    .HasForeignKey(d => d.OccupantIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Occupants_OccupantHazards_FK");
            });

            modelBuilder.Entity<Risk_OccupantLevel>(entity =>
            {
                entity.HasKey(e => e.OccupantLevelIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("OccupantLevels", "Risks");

                entity.HasIndex(e => new { e.OccupantIdentifier, e.OccupantLevelIdentifier })
                    .HasName("OccupantLevels_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.OccupantLevelIdentifier).ValueGeneratedNever();

                entity.Property(e => e.AutomaticSprinklerType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.LimitedSupplyFireProtectionSystemComment)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.OccupantLevelType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.Occupant)
                    .WithMany(p => p.OccupantLevels)
                    .HasForeignKey(d => d.OccupantIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Occupants_OccupantLevels_FK");
            });

            modelBuilder.Entity<Risk_ProtectionSafeguard>(entity =>
            {
                entity.HasKey(e => e.ProtectionSafeguardIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ProtectionSafeguards", "Risks");

                entity.HasIndex(e => new { e.ProtectionSafeguardCodeValue, e.ReportIdentifier })
                    .HasName("ProtectionSafeguards_ix01")
                    .IsUnique();

                entity.HasIndex(e => new { e.ReportIdentifier, e.ProtectionSafeguardCodeValue })
                    .HasName("ProtectionSafeguards_ix00")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.ProtectionSafeguardIdentifier).ValueGeneratedNever();

                entity.Property(e => e.ProtectionSafeguardCodeValue)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ProtectionSafeguards)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_ProtectionSafeguardCodeValue_FK");
            });

            modelBuilder.Entity<Risk_ProtectionSafeguardCode>(entity =>
            {
                entity.HasKey(e => e.ProtectionSafeguardCodeValue);

                entity.ToTable("ProtectionSafeguardCodes", "Risks");

                entity.Property(e => e.ProtectionSafeguardCodeValue)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ProtectionSafeguardCodeDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Risk_Report>(entity =>
            {
                entity.HasKey(e => new { e.RiskId, e.ReportIdentifier });

                entity.ToTable("Reports", "Risks");

                entity.HasIndex(e => e.ReportIdentifier)
                    .HasName("Reports_IX01")
                    .IsUnique();

                entity.HasIndex(e => new { e.OrderId, e.ReportIdentifier })
                    .HasName("Reports_IX02")
                    .IsUnique();

                entity.Property(e => e.RiskId)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.BasicGroup2Symbol)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.BillingType)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingDescription)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingFireConstructionCodeValue)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingFireRatedCodeValue)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingWindCommercialStatisticalPlanCodeValue)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingWindConstructionCodeValue)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ClmClassCsp)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ColumnType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CombustibilityClass)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CommercialStatisticalPlanCode)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CommercialStatisticalPlanTerritoryCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DeleteComment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EscortedByName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EscortedByPhone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FieldRep)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FinalScheduleResultType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FireDepartmentCompaniesAdequacy).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.HydrantSpacingAdequacy).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.InaccessibleAreaComment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IsCc3).HasColumnName("IsCC3");

                entity.Property(e => e.IsCc4).HasColumnName("IsCC4");

                entity.Property(e => e.OverallAdequacy).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.PanelConstructionType)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.PercentFloorCombustible).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.PercentFloorFireResistive).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.PercentFloorHourlyRatingEqualsOne).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.PercentFloorHourlyRatingGreaterThanOne).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.PercentWallCombustible).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.PercentWallFireResistive).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.PercentWallModeratelyFireResistive).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.PercentwallNoncombustible).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.PublicProtectionClass)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.RiskTypeCodeValue)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SkylightsRecognition)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.SprinklerTypeCodeValue)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SurveyStatusCodeValue)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TerritoryCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WaterSupplyWorksAdequacy).HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<Risk_ReportAddress>(entity =>
            {
                entity.HasKey(e => e.ReportAddressIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ReportAddresses", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.AddressSequence })
                    .HasName("ReportAddresses_ix00")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.HasIndex(e => new { e.StateCode, e.City, e.Zip })
                    .HasName("ReportAddresses_ix01");

                entity.HasIndex(e => new { e.Zip, e.StateCode, e.City })
                    .HasName("ReportAddresses_ix02");

                entity.Property(e => e.ReportAddressIdentifier).ValueGeneratedNever();

                entity.Property(e => e.AddressVerificationTypeCodeValue)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CommunityName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.County)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FireProtectionArea)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.HighNumber)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.LowNumber)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.PostDirection)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCity)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PreDirection)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Prefix)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StreetName)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.StreetType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Zip4)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportAddresses)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_ReportAddresses_FK");
            });

            modelBuilder.Entity<Risk_ReportAttachment>(entity =>
            {
                entity.HasKey(e => e.ReportAttachmentIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ReportAttachments", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.AttachmentSequence })
                    .HasName("ReportAttachments_ix00")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.ReportAttachmentIdentifier).ValueGeneratedNever();

                entity.Property(e => e.Attachment).IsRequired();

                entity.Property(e => e.AttachmentDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AttachmentTypeCodeValue)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportAttachments)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_ReportAttachments_FK");
            });

            modelBuilder.Entity<Risk_ReportBuildingInformation>(entity =>
            {
                entity.HasKey(e => e.ReportBuildingInformationIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ReportBuildingInformation", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.DisplaySequence })
                    .HasName("ReportBuildingInformation_ix00")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.ReportBuildingInformationIdentifier).ValueGeneratedNever();

                entity.Property(e => e.BuildingInformation)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportBuildingInformations)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_ReportBuildingInformation_FK");
            });

            modelBuilder.Entity<Risk_ReportDateTypeCode>(entity =>
            {
                entity.HasKey(e => e.ReportDateTypeCodeValue);

                entity.ToTable("ReportDateTypeCodes", "Risks");

                entity.Property(e => e.ReportDateTypeCodeValue)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ReportDateTypeCodeDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Risk_ReportHazard>(entity =>
            {
                entity.HasKey(e => e.ReportHazardIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ReportHazards", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.ReportHazardIdentifier })
                    .HasName("ReportHazards_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.ReportHazardIdentifier).ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .HasMaxLength(650)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportHazards)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_ReportHazards_FK");
            });

            modelBuilder.Entity<Risk_ReportPhoto>(entity =>
            {
                entity.HasKey(e => e.ReportPhotoIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ReportPhotos", "Risks");

                entity.HasIndex(e => new { e.PhotoIdentifier, e.ReportPhotoIdentifier })
                    .HasName("ReportPhotos_ix02")
                    .IsUnique();

                entity.HasIndex(e => new { e.ReportIdentifier, e.ReportPhotoIdentifier })
                    .HasName("ReportPhotos_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.ReportPhotoIdentifier).ValueGeneratedNever();

                entity.Property(e => e.ReportPhotoType)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportPhotoes)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_ReportPhotos_FK");
            });

            modelBuilder.Entity<Risk_ReportRelatedDate>(entity =>
            {
                entity.HasKey(e => e.ReportRelatedDateIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ReportRelatedDates", "Risks");

                entity.HasIndex(e => new { e.ReportDateTime, e.ReportDateTypeCodeValue, e.ReportIdentifier })
                    .HasName("ReportRelatedDates_ix01")
                    .IsUnique();

                entity.HasIndex(e => new { e.ReportIdentifier, e.ReportDateTypeCodeValue, e.ReportDateTime })
                    .HasName("ReportRelatedDates_ix00")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.ReportRelatedDateIdentifier).ValueGeneratedNever();

                entity.Property(e => e.ReportDateTypeCodeValue)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.ReportDateTypeCode)
                    .WithMany(p => p.ReportRelatedDates)
                    .HasForeignKey(d => d.ReportDateTypeCodeValue)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ReportRelatedDateTypeCodes_ReportRelatedDates_FK");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportRelatedDates)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_ReportRelatedDates_FK");
            });

            modelBuilder.Entity<Risk_RetiredOccupantNumber>(entity =>
            {
                entity.HasKey(e => new { e.RetiredOccupantNumberIdentifier, e.ReportIdentifier });

                entity.ToTable("RetiredOccupantNumbers", "Risks");

                entity.HasIndex(e => new { e.RetiredOccupantNumberIdentifier, e.ReportIdentifier })
                    .HasName("RetiredOccupantNumbers_IX01");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.RetiredOccupantNumbers)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RetiredOccupantNumbers_Reports_FK");
            });

            modelBuilder.Entity<Risk_SecondaryConstruction>(entity =>
            {
                entity.HasKey(e => e.SecondaryConstructionIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("SecondaryConstructions", "Risks");

                entity.HasIndex(e => new { e.ReportIdentifier, e.SecondaryConstructionIdentifier })
                    .HasName("SecondaryConstructions_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.SecondaryConstructionIdentifier).ValueGeneratedNever();

                entity.Property(e => e.BuildingConditionComment)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingConditionType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CombustibleExteriorAttachmentType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.FinishExteriorCombustibleType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.FinishInteriorCombustibleType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.GreenRoofCoverage)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PercentOfExteriorWallArea).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.RoofSurfaceConstructionType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.SolarPanelsCoverage)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.SecondaryConstructions)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_SecondaryConstructions_FK");
            });

            modelBuilder.Entity<Risk_Wall>(entity =>
            {
                entity.HasKey(e => e.WallIdentifier)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("Walls", "Risks");

                entity.HasIndex(e => new { e.ConstructionTypeCodeValue, e.WallIdentifier })
                    .HasName("Walls_ix02")
                    .IsUnique();

                entity.HasIndex(e => new { e.ReportIdentifier, e.WallClassification, e.WallIdentifier })
                    .HasName("Walls_ix01")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.WallIdentifier).ValueGeneratedNever();

                entity.Property(e => e.CombustibilityClass)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CombustibilityRating)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ConstructionTypeCodeValue)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FireResistiveListType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.ConstructionTypeCode)
                    .WithMany(p => p.Walls)
                    .HasForeignKey(d => d.ConstructionTypeCodeValue)
                    .HasConstraintName("ConstructionTypeCodes_Walls_FK");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.Walls)
                    .HasPrincipalKey(p => p.ReportIdentifier)
                    .HasForeignKey(d => d.ReportIdentifier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reports_Walls_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
