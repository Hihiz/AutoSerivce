using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSerivce.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Gender",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Gender", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Manufacturer",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        StartDate = table.Column<DateTime>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Manufacturer", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Service",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Cost = table.Column<decimal>(type: "money", nullable: false),
            //        DurationWork = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Discount = table.Column<double>(type: "float", nullable: true),
            //        MainImagePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Service", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tag",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        Color = table.Column<string>(type: "nchar(6)", fixedLength: true, maxLength: 6, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tag", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Client",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        FirsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Birthday = table.Column<DateTime>(type: "date", nullable: true),
            //        RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
            //        GenderCodeId = table.Column<int>(type: "int", nullable: false),
            //        PhotoPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Client", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Client_Gender",
            //            column: x => x.GenderCodeId,
            //            principalTable: "Gender",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Product",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Cost = table.Column<decimal>(type: "money", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MainImagePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        ManufacturerID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Product", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Product_Manufacturer",
            //            column: x => x.ManufacturerID,
            //            principalTable: "Manufacturer",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ServicePhoto",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ServiceID = table.Column<int>(type: "int", nullable: false),
            //        PhotoPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ServicePhoto", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ServicePhoto_Service",
            //            column: x => x.ServiceID,
            //            principalTable: "Service",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ClientService",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ClientID = table.Column<int>(type: "int", nullable: false),
            //        ServiceID = table.Column<int>(type: "int", nullable: false),
            //        StartTime = table.Column<DateTime>(type: "datetime", nullable: false),
            //        Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ClientService", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ClientService_Client",
            //            column: x => x.ClientID,
            //            principalTable: "Client",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_ClientService_Service",
            //            column: x => x.ServiceID,
            //            principalTable: "Service",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TagOfClient",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ClientID = table.Column<int>(type: "int", nullable: false),
            //        TagID = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TagOfClient", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TagOfClient_Client",
            //            column: x => x.ClientID,
            //            principalTable: "Client",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_TagOfClient_Tag",
            //            column: x => x.TagID,
            //            principalTable: "Tag",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AttachedProduct",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        MainProductID = table.Column<int>(type: "int", nullable: false),
            //        AttachedProductID = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AttachedProduct", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AttachedProduct_Product",
            //            column: x => x.MainProductID,
            //            principalTable: "Product",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_AttachedProduct_Product1",
            //            column: x => x.AttachedProductID,
            //            principalTable: "Product",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProductPhoto",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProductID = table.Column<int>(type: "int", nullable: false),
            //        PhotoPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProductPhoto", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ProductPhoto_Product",
            //            column: x => x.ProductID,
            //            principalTable: "Product",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DocumentByService",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ClientServiceID = table.Column<int>(type: "int", nullable: false),
            //        DocumentPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DocumentByService", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_DocumentByService_ClientService",
            //            column: x => x.ClientServiceID,
            //            principalTable: "ClientService",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProductSale",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        SaleDate = table.Column<DateTime>(type: "datetime", nullable: false),
            //        ProductID = table.Column<int>(type: "int", nullable: false),
            //        Quantity = table.Column<int>(type: "int", nullable: false),
            //        ClientServiceID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProductSale", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ProductSale_ClientService",
            //            column: x => x.ClientServiceID,
            //            principalTable: "ClientService",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_ProductSale_Product",
            //            column: x => x.ProductID,
            //            principalTable: "Product",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AttachedProduct_AttachedProductID",
            //    table: "AttachedProduct",
            //    column: "AttachedProductID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AttachedProduct_MainProductID",
            //    table: "AttachedProduct",
            //    column: "MainProductID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Client_GenderCodeId",
            //    table: "Client",
            //    column: "GenderCodeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ClientService_ClientID",
            //    table: "ClientService",
            //    column: "ClientID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ClientService_ServiceID",
            //    table: "ClientService",
            //    column: "ServiceID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DocumentByService_ClientServiceID",
            //    table: "DocumentByService",
            //    column: "ClientServiceID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Product_ManufacturerID",
            //    table: "Product",
            //    column: "ManufacturerID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductPhoto_ProductID",
            //    table: "ProductPhoto",
            //    column: "ProductID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductSale_ClientServiceID",
            //    table: "ProductSale",
            //    column: "ClientServiceID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductSale_ProductID",
            //    table: "ProductSale",
            //    column: "ProductID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ServicePhoto_ServiceID",
            //    table: "ServicePhoto",
            //    column: "ServiceID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TagOfClient_ClientID",
            //    table: "TagOfClient",
            //    column: "ClientID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TagOfClient_TagID",
            //    table: "TagOfClient",
            //    column: "TagID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachedProduct");

            migrationBuilder.DropTable(
                name: "DocumentByService");

            migrationBuilder.DropTable(
                name: "ProductPhoto");

            migrationBuilder.DropTable(
                name: "ProductSale");

            migrationBuilder.DropTable(
                name: "ServicePhoto");

            migrationBuilder.DropTable(
                name: "TagOfClient");

            migrationBuilder.DropTable(
                name: "ClientService");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "Gender");
        }
    }
}
