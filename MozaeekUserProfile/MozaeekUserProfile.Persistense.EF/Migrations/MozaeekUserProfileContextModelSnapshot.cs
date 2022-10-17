﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MozaeekUserProfile.Persistense.EF;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    [DbContext(typeof(MozaeekUserProfileContext))]
    partial class MozaeekUserProfileContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.QuestionCode", "'QuestionCode', '', '100000', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MozaeekUserProfile.Domain.OtpCode", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MobileNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.HasKey("Id");

                    b.ToTable("OtpCode");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.QuestionState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1500)")
                        .HasMaxLength(1500);

                    b.Property<string>("SerializedEvent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserQuestionId")
                        .HasColumnType("bigint");

                    b.Property<int>("UserQuestionState")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserQuestionId");

                    b.ToTable("QuestionState");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastRefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(11)")
                        .HasMaxLength(11);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserDashboard", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("EntityId")
                        .HasColumnType("bigint");

                    b.Property<int>("EntityType")
                        .HasColumnType("int");

                    b.Property<string>("EntityTypeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserDashboard");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserDashboardCharacteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<int>("UserProfileCharacteristicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserProfileCharacteristicId");

                    b.ToTable("UserDashboardCharacteristic");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserDiscount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DiscountAmount")
                        .HasColumnType("int");

                    b.Property<short>("DiscountPercent")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserDiscount");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserPoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("PointId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserPoint");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserProfileCharacteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("FirstLabelParentId")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstLabelParentTitle")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<long>("LabelId")
                        .HasColumnType("bigint");

                    b.Property<string>("LabelTitle")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<int>("UserProfileCharacteristicOwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileCharacteristicOwnerId");

                    b.ToTable("UserProfileCharacteristic");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserProfileCharacteristicOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserProfileCharacteristicOwner");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastQuestionState")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PriceCurrencyType")
                        .HasColumnType("int");

                    b.Property<string>("QuestionCodeNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(6)")
                        .HasDefaultValueSql("convert(varchar,(NEXT VALUE FOR QuestionCode) )")
                        .HasMaxLength(6);

                    b.Property<string>("QuestionCodePreFix")
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("QuestionTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionType")
                        .HasColumnType("int");

                    b.Property<Guid>("QuestionUniqId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("RequestId")
                        .HasColumnType("bigint");

                    b.Property<string>("RequestTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("SubjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("SubjectTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SystemPriceShare")
                        .HasColumnType("int");

                    b.Property<int>("TechnicianPriceShare")
                        .HasColumnType("int");

                    b.Property<int>("TechnicianType")
                        .HasColumnType("int");

                    b.Property<string>("TextDescription")
                        .HasColumnType("nvarchar(1500)")
                        .HasMaxLength(1500);

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("VoiceFileId")
                        .HasColumnType("bigint");

                    b.Property<string>("VoiceHttpPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserQuestion");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserQuestionAttachment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileHttpAddress")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserQuestionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserQuestionId");

                    b.ToTable("UserQuestionAttachment");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserWallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableAmount")
                        .HasColumnType("int");

                    b.Property<int>("PriceCurrencyId")
                        .HasColumnType("int");

                    b.Property<string>("PriceCurrencyTitle")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserWallets");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserWalletCredit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<int>("UserWalletCreditType")
                        .HasColumnType("int");

                    b.Property<int>("UserWalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserWalletId");

                    b.ToTable("UserWalletCredit");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserWalletDebit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<long>("UserQuestionId")
                        .HasColumnType("bigint");

                    b.Property<int>("UserWalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserQuestionId")
                        .IsUnique();

                    b.HasIndex("UserWalletId");

                    b.ToTable("UserWalletDebit");
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.QuestionState", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.UserQuestion", "UserQuestion")
                        .WithMany("QuestionStates")
                        .HasForeignKey("UserQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserDashboard", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.User", "User")
                        .WithMany("UserDashboards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserDashboardCharacteristic", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.User", "User")
                        .WithMany("UserDashboardCharacteristics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MozaeekUserProfile.Domain.UserProfileCharacteristic", "UserProfileCharacteristic")
                        .WithMany("UserDashboardCharacteristics")
                        .HasForeignKey("UserProfileCharacteristicId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserDiscount", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.User", "User")
                        .WithMany("UserDiscounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserPoint", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.User", "User")
                        .WithMany("UserPoints")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserProfileCharacteristic", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.UserProfileCharacteristicOwner", "UserProfileCharacteristicOwner")
                        .WithMany("UserProfileCharacteristics")
                        .HasForeignKey("UserProfileCharacteristicOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserProfileCharacteristicOwner", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.User", "User")
                        .WithMany("UserProfileCharacteristicOwners")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserQuestion", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.User", "User")
                        .WithMany("UserQuestions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserQuestionAttachment", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.UserQuestion", "UserQuestion")
                        .WithMany("UserQuestionAttachments")
                        .HasForeignKey("UserQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserWallet", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.User", "User")
                        .WithMany("UserWallets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserWalletCredit", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.UserWallet", "UserWallet")
                        .WithMany("UserWalletCredits")
                        .HasForeignKey("UserWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MozaeekUserProfile.Domain.UserWalletDebit", b =>
                {
                    b.HasOne("MozaeekUserProfile.Domain.UserQuestion", "UserQuestions")
                        .WithOne("UserWalletDebit")
                        .HasForeignKey("MozaeekUserProfile.Domain.UserWalletDebit", "UserQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MozaeekUserProfile.Domain.UserWallet", "UserWallet")
                        .WithMany("UserWalletDebits")
                        .HasForeignKey("UserWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
