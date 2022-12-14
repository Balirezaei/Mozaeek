// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MozaeekCore.Persistense.EF;

namespace MozaeekCore.Persistense.EF.Migrations
{
    [DbContext(typeof(CoreDomainContext))]
    [Migration("20210225205713_tech")]
    partial class tech
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MozaeekCore.Domain.Announcement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("RequestTargetId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RequestTargetId");

                    b.ToTable("Announcement");
                });

            modelBuilder.Entity("MozaeekCore.Domain.AnnouncementPoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AnnouncementId")
                        .HasColumnType("bigint");

                    b.Property<long>("PointId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementId");

                    b.HasIndex("PointId");

                    b.ToTable("AnnouncementPoint");
                });

            modelBuilder.Entity("MozaeekCore.Domain.EducationField", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("EducationField");
                });

            modelBuilder.Entity("MozaeekCore.Domain.EducationGrade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EducationGrade");
                });

            modelBuilder.Entity("MozaeekCore.Domain.Label", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("MozaeekCore.Domain.Point", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Point");
                });

            modelBuilder.Entity("MozaeekCore.Domain.RSS", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IntervalDataReceiveHours")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("RSS");
                });

            modelBuilder.Entity("MozaeekCore.Domain.Request", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("RequestActId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("RequestExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RequestStartDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("RequestTargetId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RequestActId");

                    b.HasIndex("RequestTargetId");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("MozaeekCore.Domain.RequestAct", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("RequestAct");
                });

            modelBuilder.Entity("MozaeekCore.Domain.RequestOrg", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("RequestOrg");
                });

            modelBuilder.Entity("MozaeekCore.Domain.RequestTarget", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("RequestTarget");
                });

            modelBuilder.Entity("MozaeekCore.Domain.RequestTargetLabel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("LabelId")
                        .HasColumnType("bigint");

                    b.Property<long>("RequestTargetId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LabelId");

                    b.HasIndex("RequestTargetId");

                    b.ToTable("RequestTargetLabel");
                });

            modelBuilder.Entity("MozaeekCore.Domain.RequestTargetRequestOrg", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("RequestOrgId")
                        .HasColumnType("bigint");

                    b.Property<long>("RequestTargetId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RequestOrgId");

                    b.HasIndex("RequestTargetId");

                    b.ToTable("RequestTargetRequestOrg");
                });

            modelBuilder.Entity("MozaeekCore.Domain.RequestTargetSubject", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("RequestTargetId")
                        .HasColumnType("bigint");

                    b.Property<long>("SubjectId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RequestTargetId");

                    b.HasIndex("SubjectId");

                    b.ToTable("RequestTargetSubject");
                });

            modelBuilder.Entity("MozaeekCore.Domain.Subject", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("MozaeekCore.Domain.Technician", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("TechnicianContactInfoId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TechnicianEducationalInformationId")
                        .HasColumnType("bigint");

                    b.Property<long>("TechnicianPersonalInfoId")
                        .HasColumnType("bigint");

                    b.Property<byte>("TechnicianType")
                        .HasColumnType("TINYINT");

                    b.HasKey("Id");

                    b.HasIndex("TechnicianContactInfoId");

                    b.HasIndex("TechnicianEducationalInformationId");

                    b.HasIndex("TechnicianPersonalInfoId");

                    b.ToTable("Technician");
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianAttachment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttachmentType")
                        .HasColumnType("int");

                    b.Property<string>("FileExtention")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<byte[]>("Source")
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("TechnicianId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TechnicianId");

                    b.ToTable("TechnicianAttachment");
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianContactInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("OfficeNumber")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TechnicianContactInfo");
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianEducationalInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("EducationFieldId")
                        .HasColumnType("bigint");

                    b.Property<long>("EducationGradeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EducationFieldId");

                    b.HasIndex("EducationGradeId");

                    b.ToTable("TechnicianEducationalInfo");
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianPersonalInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("IdentityNumber")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NationalCode")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("TechnicianPersonalInfo");
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianPoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("PointId")
                        .HasColumnType("bigint");

                    b.Property<long>("TechnicianId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PointId");

                    b.HasIndex("TechnicianId");

                    b.ToTable("TechnicianPoint");
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("RequestId")
                        .HasColumnType("bigint");

                    b.Property<long>("TechnicianId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.HasIndex("TechnicianId");

                    b.ToTable("TechnicianRequest");
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianSubject", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("SubjectId")
                        .HasColumnType("bigint");

                    b.Property<long>("TechnicianId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TechnicianId");

                    b.ToTable("TechnicianSubject");
                });

            modelBuilder.Entity("MozaeekCore.Domain.UnProcessedRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<string>("Summery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UnProcessedRequests");
                });

            modelBuilder.Entity("MozaeekCore.ViewModel.RSSNews", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("RSSId")
                        .HasColumnType("bigint");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RssNewses");
                });

            modelBuilder.Entity("MozaeekCore.Domain.Announcement", b =>
                {
                    b.HasOne("MozaeekCore.Domain.RequestTarget", "RequestTarget")
                        .WithMany("Announcements")
                        .HasForeignKey("RequestTargetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekCore.Domain.AnnouncementPoint", b =>
                {
                    b.HasOne("MozaeekCore.Domain.Announcement", "Announcement")
                        .WithMany("AnnouncementPoints")
                        .HasForeignKey("AnnouncementId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MozaeekCore.Domain.Point", "Point")
                        .WithMany("AnnouncementPoints")
                        .HasForeignKey("PointId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekCore.Domain.EducationField", b =>
                {
                    b.HasOne("MozaeekCore.Domain.EducationField", "Parent")
                        .WithMany("SubEducationFields")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("MozaeekCore.Domain.Label", b =>
                {
                    b.HasOne("MozaeekCore.Domain.Label", "Parent")
                        .WithMany("SubLabels")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("MozaeekCore.Domain.Point", b =>
                {
                    b.HasOne("MozaeekCore.Domain.Point", "Parent")
                        .WithMany("SubPoints")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("MozaeekCore.Domain.Request", b =>
                {
                    b.HasOne("MozaeekCore.Domain.RequestAct", "RequestAct")
                        .WithMany("Requests")
                        .HasForeignKey("RequestActId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MozaeekCore.Domain.RequestTarget", "RequestTarget")
                        .WithMany("Requests")
                        .HasForeignKey("RequestTargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("MozaeekCore.Domain.RequestAction", "RequestActions", b1 =>
                        {
                            b1.Property<long>("RequestId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Priority")
                                .HasColumnType("int");

                            b1.HasKey("RequestId", "Id");

                            b1.ToTable("RequestAction");

                            b1.WithOwner()
                                .HasForeignKey("RequestId");
                        });

                    b.OwnsMany("MozaeekCore.Domain.RequestDocument", "RequestDocuments", b1 =>
                        {
                            b1.Property<long>("RequestId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Priority")
                                .HasColumnType("int");

                            b1.HasKey("RequestId", "Id");

                            b1.ToTable("RequestDocument");

                            b1.WithOwner()
                                .HasForeignKey("RequestId");
                        });

                    b.OwnsMany("MozaeekCore.Domain.RequestNessesity", "RequestNessesities", b1 =>
                        {
                            b1.Property<long>("RequestId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Priority")
                                .HasColumnType("int");

                            b1.HasKey("RequestId", "Id");

                            b1.ToTable("RequestNessesity");

                            b1.WithOwner()
                                .HasForeignKey("RequestId");
                        });

                    b.OwnsMany("MozaeekCore.Domain.RequestQualification", "RequestQualifications", b1 =>
                        {
                            b1.Property<long>("RequestId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Priority")
                                .HasColumnType("int");

                            b1.HasKey("RequestId", "Id");

                            b1.ToTable("RequestQualification");

                            b1.WithOwner()
                                .HasForeignKey("RequestId");
                        });
                });

            modelBuilder.Entity("MozaeekCore.Domain.RequestOrg", b =>
                {
                    b.HasOne("MozaeekCore.Domain.RequestOrg", "Parent")
                        .WithMany("SubRequestOrgs")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("MozaeekCore.Domain.RequestTargetLabel", b =>
                {
                    b.HasOne("MozaeekCore.Domain.Label", "Label")
                        .WithMany("RequestTargetLabels")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MozaeekCore.Domain.RequestTarget", "RequestTarget")
                        .WithMany("RequestTargetLabels")
                        .HasForeignKey("RequestTargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekCore.Domain.RequestTargetRequestOrg", b =>
                {
                    b.HasOne("MozaeekCore.Domain.RequestOrg", "RequestOrg")
                        .WithMany("RequestTargetRequestOrgs")
                        .HasForeignKey("RequestOrgId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MozaeekCore.Domain.RequestTarget", "RequestTarget")
                        .WithMany("RequestTargetRequestOrgs")
                        .HasForeignKey("RequestTargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekCore.Domain.RequestTargetSubject", b =>
                {
                    b.HasOne("MozaeekCore.Domain.RequestTarget", "RequestTarget")
                        .WithMany("RequestTargetSubjects")
                        .HasForeignKey("RequestTargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MozaeekCore.Domain.Subject", "Subject")
                        .WithMany("RequestTargetSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekCore.Domain.Subject", b =>
                {
                    b.HasOne("MozaeekCore.Domain.Subject", "Parent")
                        .WithMany("SubSubjects")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("MozaeekCore.Domain.Technician", b =>
                {
                    b.HasOne("MozaeekCore.Domain.TechnicianContactInfo", "TechnicianContactInfo")
                        .WithMany("Technicians")
                        .HasForeignKey("TechnicianContactInfoId");

                    b.HasOne("MozaeekCore.Domain.TechnicianEducationalInfo", "TechnicianEducationalInfo")
                        .WithMany("Technicians")
                        .HasForeignKey("TechnicianEducationalInformationId");

                    b.HasOne("MozaeekCore.Domain.TechnicianPersonalInfo", "TechnicianPersonalInfo")
                        .WithMany("Technicians")
                        .HasForeignKey("TechnicianPersonalInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianAttachment", b =>
                {
                    b.HasOne("MozaeekCore.Domain.Technician", "Technician")
                        .WithMany("TechnicianAttachments")
                        .HasForeignKey("TechnicianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianEducationalInfo", b =>
                {
                    b.HasOne("MozaeekCore.Domain.EducationField", "EducationField")
                        .WithMany("TechnicianEducationalInfos")
                        .HasForeignKey("EducationFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MozaeekCore.Domain.EducationGrade", "EducationGrade")
                        .WithMany("TechnicianEducationalInfos")
                        .HasForeignKey("EducationGradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianPoint", b =>
                {
                    b.HasOne("MozaeekCore.Domain.Point", "Point")
                        .WithMany()
                        .HasForeignKey("PointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MozaeekCore.Domain.Technician", "Technician")
                        .WithMany("TechnicianPoints")
                        .HasForeignKey("TechnicianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianRequest", b =>
                {
                    b.HasOne("MozaeekCore.Domain.Request", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MozaeekCore.Domain.Technician", "Technician")
                        .WithMany("TechnicianRequests")
                        .HasForeignKey("TechnicianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekCore.Domain.TechnicianSubject", b =>
                {
                    b.HasOne("MozaeekCore.Domain.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MozaeekCore.Domain.Technician", "Technician")
                        .WithMany("TechnicianSubjects")
                        .HasForeignKey("TechnicianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
