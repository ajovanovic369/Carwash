using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWash.Migrations
{
    public partial class newone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarWashes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningHours = table.Column<int>(type: "int", nullable: false),
                    ClosingHours = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarWashOpen = table.Column<bool>(type: "bit", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Earnings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarWashId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    SchedulingId = table.Column<int>(type: "int", nullable: false),
                    Appointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Earnings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedulings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Appointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserReservation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedulings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchedulingEntity",
                columns: table => new
                {
                    SchedulingId = table.Column<int>(type: "int", nullable: false),
                    CarWashEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulingEntity", x => new { x.SchedulingId, x.CarWashEntityId });
                    table.ForeignKey(
                        name: "FK_SchedulingEntity_CarWashes_CarWashEntityId",
                        column: x => x.CarWashEntityId,
                        principalTable: "CarWashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchedulingEntity_Schedulings_SchedulingId",
                        column: x => x.SchedulingId,
                        principalTable: "Schedulings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarWashEntityServices",
                columns: table => new
                {
                    CarWashEntityId = table.Column<int>(type: "int", nullable: false),
                    CarWashServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashEntityServices", x => new { x.CarWashEntityId, x.CarWashServiceId });
                    table.ForeignKey(
                        name: "FK_CarWashEntityServices_CarWashes_CarWashEntityId",
                        column: x => x.CarWashEntityId,
                        principalTable: "CarWashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarWashEntityServices_Services_CarWashServiceId",
                        column: x => x.CarWashServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchedulingServices",
                columns: table => new
                {
                    SchedulingId = table.Column<int>(type: "int", nullable: false),
                    CarWashServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulingServices", x => new { x.SchedulingId, x.CarWashServiceId });
                    table.ForeignKey(
                        name: "FK_SchedulingServices_Schedulings_SchedulingId",
                        column: x => x.SchedulingId,
                        principalTable: "Schedulings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchedulingServices_Services_CarWashServiceId",
                        column: x => x.CarWashServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fab4fac1-c546-41de-aebc-a14da6895711", "1", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "05b41bca-e095-4b12-a84d-5282cbc3f3e1", 0, "7747fe16-130f-4030-8492-f0174b5a1a62", "john@yup.com", false, false, null, "JOHN@YUP.COM", "JOHN", "AQAAAAEAACcQAAAAEFogVlg2qoDoschNq0XXjuen+eeSQzU9OuawqIzPOIrDDJjIdca5FuGxIaHvSLfPgg==", null, false, "2eb98c53-6c5d-4482-b43c-8c4ff9c446d1", false, "john" },
                    { "092ccd3b-1952-4bd0-8c42-349e7f45e947", 0, "eb2e9a4b-2066-49a2-b5a3-0d35afcae0d0", "kenny@omg.com", false, false, null, "KENNY@OMG.COM", "KENNY", "AQAAAAEAACcQAAAAEMy5sPZox5upFOxAeFTIhobXU4rcmVOyWa50i45dQ3v/goV/DcYZ3k3gVAfj7/CYsQ==", null, false, "de5ab130-d2c4-48ce-9d0d-e5d86a038e52", false, "kenny" },
                    { "420611f6-66cf-4cd6-b9d6-e15e0663d284", 0, "748272e5-ed52-4e86-80ff-214e356cf517", "wick@yup.com", false, false, null, "WICK@YUP.COM", "WICK", "AQAAAAEAACcQAAAAEIJ2j7f65E+abKWodx09m2TMyZ98bnfE0iBGk5Z2kRsTeyGRUV+wn6cBqdR+pvm0Rw==", null, false, "b23bf81e-abf5-43c6-9cf7-63a2fbb00865", false, "wick" },
                    { "421ad256-807a-4869-b683-5b0c18d98cc7", 0, "45146ea7-3bee-413e-892b-dad1d9066bbe", "trent@yup.com", false, false, null, "TRENT@YUP.COM", "TRENT", "AQAAAAEAACcQAAAAEE9QPu1l8hRmXsxm1k+BdfN3BYvA/+u0j9LCTOdExMG7vKWETeeUJAE9koydvTvyvQ==", null, false, "776715dd-d334-440f-9d86-b990e4ef3675", false, "trent" },
                    { "6755b339-09fa-4990-b0a4-81c89d23ef5c", 0, "9658efc2-fbe0-426b-9ee3-d9b808b64c13", "cartman@yup.com", false, false, null, "CARTMAN@YUP.COM", "CARTMAN", "AQAAAAEAACcQAAAAEP7FwGgC7agKVpNpAI40VhDAEd/96MHpK6nH19wPBle8mgwug5QGEYKEN0ES/X67DA==", null, false, "4d565e66-ad10-4498-b55d-9323bbe5fa52", false, "cartman" },
                    { "85fbd79d-bc60-4966-916f-24f73a9af1cb", 0, "3e2eda41-42e2-43b1-8c5e-603ecf57a526", "aleksandar@bla.com", false, false, null, "ALEKSANDAR@BLA.COM", "ALEKSANDAR", "AQAAAAEAACcQAAAAEK8Tvffo09h2XqNi2tlZuXLxjBkhoreKIq76GFfiPEEZVs56/H+GN32SqKpExZXdOA==", null, false, "d3abb2de-3238-4d79-8787-4d6aea9573e2", false, "aleksandar" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "2d27b0d7-6026-4a79-9f0f-1a59de81182e", "admin@bla.com", false, false, null, "ADMIN@BLA.COM", "ADMIN", "AQAAAAEAACcQAAAAEFpTtCM1bzuemRnLnThQw7lE6daIUk60fjAOkbq9DoUJ9cwN+BkLXOQi2Dd0puhjog==", null, false, "9d6dd0e7-feec-4726-9435-252c6985e698", false, "admin" },
                    { "e9d21d9e-1bda-4ce7-be80-f688eadb8c2e", 0, "82bcf6e5-7b6e-44c4-afac-f7f68fe3f2d5", "billy@yup.com", false, false, null, "BILLY@YUP.COM", "BILLY", "AQAAAAEAACcQAAAAEKFL3mhQepWwPfOnM/zEX9HY58zg+DLQ72yXhZ2xs5DaXo2HW9MZGBhoNHcIuAsjvA==", null, false, "b7f655ae-c76d-4017-b654-0e15c43b94d9", false, "billy" }
                });

            migrationBuilder.InsertData(
                table: "CarWashes",
                columns: new[] { "Id", "Address", "CarWashOpen", "ClosingHours", "Name", "OpeningHours", "Owner", "Picture" },
                values: new object[,]
                {
                    { 1, "Tamo Daleko 21", false, 17, "Bili CarWash", 9, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_TXU9g0v-DUxTBZwmrybRDWu_4itnIkAEC-mxbhtZN8v0F0AbQTqNutoRf-jwUOrtyPZtSosRucKKOQC_s4GRMTvwAB2rh80Pr2rMQLiLHmQ", "" },
                    { 2, "Fiu 69", false, 16, "Miki Doo Pranje", 8, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof8IO9driRYWCcIxveTLtEhhYA7cHM93UF-6L7bhr2ChysFNmcEqZYLEYD29E8qB-tofkPEJ3FLCJpYXgQRVfOU1_qSyftSt5S4JxQAtCZBBGg", "" },
                    { 3, "Bulevar Puteva 37", false, 18, "Mosa Komerc Wash", 10, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof9jamU5gIBgAAOK_fi5nRl5iN8MBfJTg2c0r2CSPbDK0ShQ4MWrdNA_FcCLJkgdadNuV9NQvS8qYEZ110FawzKkMY3v5VJGxbNuW9QJjWQ7nw", "" },
                    { 4, "Over the rock 123", false, 17, "Carwash Infinity", 9, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_1QNySaHmBASCjgnQ6QcX1haw5bhDDJY4A8b7qEP6whMUh-AiCkNFjOWrYI4YoqYCzJ1dyI5mOmZjiF2h5J4wFKCf7dpzEmXVVjNe8AML6cQ", "" },
                    { 5, "Bulevar Puteva 37", false, 20, "Poor's Man Carwash", 8, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_RCaHYYu9hasbDwmjHjVzPhR3URMhYx57skacDiL4u-OsYaTCka1QHgtskVU8z2WMjA7ktTvahTueMjWhHmH1D-X3ebwaeGwtza23aLGWGYQ", "" },
                    { 6, "Bulevar 37", false, 20, "Something", 8, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_UF5lNvvhqKzGw0Yqty_hR-TKClYYCpRfWeBndcAPO80pZ4aEQ_DVaEGCGkZeyKXJ6ivaRPdb95uI-mJFhp3M8HMI_mNKRGL-jryP8P1OVLw", "" },
                    { 7, "Puteva 37", false, 18, "Carwasha", 10, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof-AdJS5pLmhVnEAHOn3Z5jgYmB3jQdQW2ajs0oXqGx3vMqMWmrR45kZ86JuG1Tt_SdJB6Ip0mj1ERQM_reS8PMqSC3nlANloSMROxYCwK0Wdg", "" },
                    { 8, "Bulevar Necega 37", false, 17, "Carwashb", 9, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_dVks3qu-h41aU55xHF3EQgr8QDg0OoBm9InOkjXKLK_xOlnWdPY6qyoZHELHEnGkkCw4VcJ3cqKfraGDW3RKHlsqyZukK_ZEz78OpXxYQjQ", "" },
                    { 9, "Nesto Tamo 3", false, 17, "Carwashc", 8, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof-H1VQ50f4JCqgq2dEdN3_XJf7VrbY3FTcFxUusLOg9rBIyShw39kN0x5DmaJODB9wnMdA1BHQQvu2zpMVQQMWjn-2i7834XDr9C-BVdCtHag", "" },
                    { 10, "Blafa 21", false, 20, "Carwashq", 8, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof9-4U7_KneKAH-s-BFJ3G6j08lIs_mm4lSCm3cwfJvNLq8mEP3W8MmEeubbKQoN0zYEh_zo-OxCaV8scdxXFZhxes7OKzP7a9Gsk4U2TdDTPA", "" },
                    { 11, "Iha Adresa 9", false, 18, "Carwashw", 6, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof8Nu_eaaw5xNg_ChXBl3K57NDY_KjscLYRz9VSG18wtlkaF_ORSCyy5zJ5kdL4jdKDcFbDW88MHV3Yg0tEw6SYImLMXPiUNxFX5d-797vC8Cw", "" },
                    { 12, "Polet Trg 3", false, 20, "Carwashy", 9, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof-_2Wd19GC5d2sekF_GfXU6gB-bm1UQziRSNPyhuesO2clS-8cVnVNG2XXUL2CYdhb1CVX7L0kJ9Lr50q1MuQChWRHYbmLrQkeWiZSx0HGixg", "" },
                    { 13, "Ada 2", false, 20, "Carwashu", 10, "CfDJ8NuQ7pwwqeNAg-k_ZKtnof8TeSsmMhO0tCVbB3Yx2iOTfunKur_6rDiD4guj6oU8vIS18gX2sQ-tR3bPycKYb2_h5KnDO7OqhhxE1rsiZpJftFKmmUIc7lmC-3z3wbUNmA", "" }
                });

            migrationBuilder.InsertData(
                table: "Schedulings",
                columns: new[] { "Id", "Appointment", "CurrentDate", "Price", "Status", "UserReservation" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 2, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), 500.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof9juYD4flt1EZIucj-fRIKJ7hUa7bIW-ZRBUNNzf6NnxKW-tIpt08SdzNmtVzogAQoilBu3zuQYRYbEA8JyT0ECbf9PvuXrcgbxHyYcvYfGlQ" },
                    { 2, new DateTime(2021, 5, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 19, 11, 0, 0, 0, DateTimeKind.Unspecified), 1000.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof9SnR1-A-YE920OI4oKmATbdfIoSZkxObaugKUuZ-CTXFPRzYRFTQmY_RJ1sVrUuABaJ2n0_v5lbN7kbqs4aK3kquBaoKBBXW_4ora1_iNpYg" },
                    { 3, new DateTime(2022, 5, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 12, 13, 0, 0, 0, DateTimeKind.Unspecified), 2000.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_GQXojHmCLtDHMIEizNp8xB_d_5Zp-I5kgH3hji8Ri3xaKM63DXDbwVRTN0p09cjQppTm-hNw-xlgE48uf2KPp-SE4FzrHi3hyG1dsXLJAOg" },
                    { 4, new DateTime(2022, 5, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), 500.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_1XOO_-6ddo_034SLsig-_KWi8xiaOqKhasAEKkqHOI-CqVir1YbXvk6GCIo2IdNhilmFKU2MBh3XpzMymK1jKDPjBhxZZ_TXrcnOD-ieTOQ" },
                    { 5, new DateTime(2022, 6, 15, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 500.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof9p0PK20uKnRCrZXvb67US-6MylBtvgazel7av1uF1KS4jbKOb_3-jBjW1S2dQTtO0igk5-D14OMYCq3Psmhm_C7bWd_NhjjVTw_RE6pk8r_Q" },
                    { 6, new DateTime(2023, 6, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), 10000.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_ZwaNQclj1g_IaQWCwWSfI872avP4was2LdPxMDAiQncsSQI4hP8F8itxpxVxYL1dsdpkw_WU47Abno1fzrCIvLsD0Bf_n7ambTwqLB4lC1g" },
                    { 7, new DateTime(2023, 7, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), 1000.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_E74cN8nLCQxT08n_3hTcbUtBy38yrlOWrVkSi2x-U-ehleqpzV-nDYdRNBmmLz4whPW6FXB73CPO78oZdo95wLudTTOnIC7h7S4lZiLwv3A" },
                    { 8, new DateTime(2022, 7, 19, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), 100.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof9LxUhUxeDeCp6TKwFeDM9y7_EJEjYD8EWYMwwmsDqykUCTutlertoTW03eBeinyK6T-1aoE41ibHCxRsQesR-RmepJnTtVwAUvLDzZUKSs5w" },
                    { 9, new DateTime(2022, 9, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), 500.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof-gMtCN8YaxKNgPVMaYjJOhMtLkJ9qdYnGPQngIJrMnqiLMVmVpLEr11cb59KFJNeKIxjQaONGJxnqyqOdEZNcg5aP9omt2FVb4r02I21EJwQ" },
                    { 10, new DateTime(2022, 9, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), 1000.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof9Y0IeTohgUSYMeaM1HcN2Jtba3Tq5haE92npQRj6llUN2WuzeOuByJsXSNP-7hXTBjQxGQO27YK8KhpuWkRYhNZ2jD36KAA8HmNueOf9p8Kg" },
                    { 11, new DateTime(2022, 9, 23, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 21, 13, 0, 0, 0, DateTimeKind.Unspecified), 500.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof8_d3nI54mMyfyIF-Cg-sAxudLPQBdolD1j0VrAQ1FljVh1KO74Zw31a5Jm8O2dTPKRY0fxRO3_BFRjuO8bDcGnNfy-81HZlXzRaRBEkwmr4A" },
                    { 12, new DateTime(2022, 9, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), 10000.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof9zQL1tidWRt6-bQWKMOhIIbnVV8s9_6jFGHkBfXsQVAcQeNo5ZutnT01lByfrqOF0ckzruQSOqlwjRf8KFDqpRV0MO29IX7p-yz1fsvmH7iQ" },
                    { 13, new DateTime(2022, 9, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), 1000.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof-zCPWc72VmOUwqd6ZEfRQNUNPmHjQV-10FtotBtVv2LeKu2bovTkfkilo_82KHxZo_V2r1YFvdEqwMKRWLm4bOPEbztew0A8ACAl37YDhlxw" },
                    { 14, new DateTime(2022, 10, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), 100.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_7NHluLcSKlpq6x3JYF5o2VnbkqSnkHQc0uby1kUu61PKD3DbJM-VW-toeO47QSXpImUTLHYfT4l8H5BLvsDbaGNjKR1ajO-ay2qNfAKtqmA" },
                    { 15, new DateTime(2023, 10, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 100.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof-cx3ccClm41RjRNWMB4utdrN_k1WuvEZWVGlhefKSTLwNl580zGXmCeKZ-bpJ5eLYV-I4ZYYMwkwHIiR-PqDwn8ChSx4QQEwcEGvVGZWnWKw" },
                    { 16, new DateTime(2023, 10, 6, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 100.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof8Wu1rH91TBuInFfmhtceUHvVyCqu8mRgVOg4u2F15poaA9JzNcEI4Vqo5us0g5BupM5IarUq-zrRayc_nexqPNHevUFt2AJPRztzB9Gp_8Hg" },
                    { 17, new DateTime(2023, 10, 7, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 100.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof8nDAYa0thiid6RvDUMRnJJmMwd5YOEWVnpfycdJ-VK5XFKzrDuhifE7rfKHmV17rk3S8_jRmiGwgjxFrK0Y5WCeMtMhenRFcBu0CR7VUk_tg" },
                    { 18, new DateTime(2023, 10, 9, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), 100.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_yPFFDWubUuCui7Jl3FGgSGLp0mOOABGo2QWschNwkdYEfwXOtGeWap2N_HB9ZJBRzXuxKIO2LoQMQTxvS4n8qcpu0Swm6NOiftDig_MhwYw" },
                    { 19, new DateTime(2023, 10, 10, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), 100.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof8XfP0ZxIG6I6kyz5XDlsxmAYqtJse2fFteViIVaQ44mERIorAlALhj8kN3KMuxbpBhxAyR2MUGYT9cK1MD2LsNM_unDhLuOHyjai50Ry_urQ" },
                    { 20, new DateTime(2023, 10, 11, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 2, 13, 0, 0, 0, DateTimeKind.Unspecified), 100.00m, "accepted", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_PMzqetMjtZUXkdDqDmaCXdHi1Q3KXGc8Uf5kZidX0fsXZ8IpvdAonXFEa33dzP7lQZLqkDD_1yw814s7qYa5J-xkifZGmsxDfrj-jqkMiLg" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "Owner", "Price" },
                values: new object[,]
                {
                    { 1, "Regular", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof8W4t4Qreao6Gm5m9OZ4zGLqVPyLlodIkUCPsNVB1IHeMLsobDkhmGOxWT8SzJbqc_m2zye285Xd7_d3ILw3HwH8wN9OBOLXwXL2Dnl1Bc25g", 500.0m },
                    { 2, "Extended", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof8w4ynmJabSMgMZkn-aKgg97pkifbObXzHRmdaVkl6zyLD20ZgpEj2D6nsHBxTcapM0Ltgic5gRc6IFhwlTHZpczax__dshJezFeXKocK_LFA", 1000.0m },
                    { 3, "Premium", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof95-JB-wWPIiSSbtZ9irXOzN-M_k5Ik5R3p8e8rYfx6fVHPwg4mhGBAMiLUFWnM90cBDDM4qqrDjv5Q0jw6oNJeAkV-qZBm2hirfaYf8EShMw", 2000.0m },
                    { 4, "PoorService", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof9OQuV7dB8EseqC4Gm_oTbYevlgrLjjY2wudg3-evOLXfePSep2JjcYAy10vYml_b1L52oLYL8swxCWVZJ_RXj_sw5FOh-VzAcYrhib1LbkGA", 100.0m },
                    { 5, "GlobalOne", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof8qNxmh04BuZT3eBBhkPN8hi3NTZZntqK3RI6aNPAQG-4AIp94J7gz0B_w-mdD6eFu_KZBC4ZraCag4BD-yqvvaYaSLaJbAkxfLIaNBwehvhg", 10000.0m },
                    { 6, "GlobalTwo", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof_6TM5n4i-eqFqwWygvfrlpyKpFlujufQQk-PJixdERVG3ZX-H8t1FZ4f55jpm8W40A_8qGWjy02i95wPe1qrEqhm_GGnQJ8CdGeRDIb82g2g", 10000.0m },
                    { 7, "GlobalThree", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof-3qzkLhm3BYlTyejt4PAoRz4P14cDEXdyUBGHIEHwRn5F4IMkI-ftw6alA0O0dhKaGZfNRLk3W55YCVae4HTQZ3KMsm22EvBgjVp-Ang4BMw", 10000.0m },
                    { 8, "GlobalFour", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof-HlU6NH1lylvYF5jPq1QKD8RgVCejFHFV3aIc-59os-PK3CtCI6zf2g9STctEBsuIcn1Ap6hsz0qx3hHX_noX9nlLr-5LPtEpyHO_deIzq8g", 10000.0m },
                    { 9, "GlobalFive", "CfDJ8NuQ7pwwqeNAg-k_ZKtnof-zmQ6oxUSK5_s-3-RmG4BwwpnTRPdf0eVnhoX08NNJHJQsu0tG0ndH_QZ_N5vyv5-bZzxZpsAVguZrWA8iFqil59zlyy_UIzAacmYRGMX0Zw", 10000.0m }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "fab4fac1-c546-41de-aebc-a14da6895711", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.InsertData(
                table: "CarWashEntityServices",
                columns: new[] { "CarWashEntityId", "CarWashServiceId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 5 },
                    { 4, 2 },
                    { 4, 3 },
                    { 5, 4 },
                    { 6, 1 },
                    { 6, 3 },
                    { 7, 5 },
                    { 8, 6 },
                    { 9, 7 },
                    { 10, 8 },
                    { 11, 9 },
                    { 12, 1 },
                    { 12, 2 },
                    { 13, 1 },
                    { 13, 2 },
                    { 13, 3 }
                });

            migrationBuilder.InsertData(
                table: "SchedulingEntity",
                columns: new[] { "CarWashEntityId", "SchedulingId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 5 },
                    { 3, 6 },
                    { 4, 7 },
                    { 5, 8 },
                    { 1, 9 },
                    { 2, 10 },
                    { 2, 11 },
                    { 3, 12 },
                    { 4, 13 },
                    { 5, 14 },
                    { 5, 15 },
                    { 5, 16 },
                    { 1, 17 },
                    { 1, 18 },
                    { 1, 19 },
                    { 1, 20 }
                });

            migrationBuilder.InsertData(
                table: "SchedulingServices",
                columns: new[] { "CarWashServiceId", "SchedulingId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 5, 6 },
                    { 2, 7 },
                    { 4, 8 },
                    { 1, 9 },
                    { 2, 10 },
                    { 1, 11 },
                    { 5, 12 },
                    { 2, 13 },
                    { 4, 14 },
                    { 4, 15 },
                    { 4, 16 },
                    { 2, 17 },
                    { 3, 18 },
                    { 2, 19 },
                    { 1, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CarWashEntityServices_CarWashServiceId",
                table: "CarWashEntityServices",
                column: "CarWashServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingEntity_CarWashEntityId",
                table: "SchedulingEntity",
                column: "CarWashEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingServices_CarWashServiceId",
                table: "SchedulingServices",
                column: "CarWashServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CarWashEntityServices");

            migrationBuilder.DropTable(
                name: "Earnings");

            migrationBuilder.DropTable(
                name: "SchedulingEntity");

            migrationBuilder.DropTable(
                name: "SchedulingServices");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CarWashes");

            migrationBuilder.DropTable(
                name: "Schedulings");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
