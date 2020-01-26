﻿using CatFactory.ObjectRelationalMapping;
using Xunit;

namespace CatFactory.Tests
{
    public class DatabaseNamingConventionTests
    {
        [Fact]
        public void TestConstraintNames()
        {
            // Arrange
            var database = new Database();

            var productCategoryTable = new Table
            {
                Schema = "dbo",
                Name = "ProductCategory",
                Columns =
                {
                    new Column { Name = "ProductCategoryID", Type = "int", Nullable = false },
                    new Column { Name = "ProductCategoryName", Type = "varchar", Length = 100, Nullable = false }
                }
            };

            database.Tables.Add(productCategoryTable);

            var productTable = new Table
            {
                Schema = "dbo",
                Name = "Product",
                Columns =
                {
                    new Column
                    {
                        Name = "ProductID",
                        Type = "int",
                        Nullable = false
                    },
                    new Column { Name = "ProductName", Type = "varchar", Length = 100, Nullable = false },
                    new Column { Name = "ProductCategoryID", Type = "int", Nullable = false }
                }
            };

            database.Tables.Add(productTable);

            // Act
            var primaryKeyName = database.NamingConvention.GetPrimaryKeyConstraintName(productTable, new string[] { "ProductID" });
            var uniqueName = database.NamingConvention.GetUniqueConstraintName(productTable, new string[] { "ProductName" });
            var foreignKeyName = database.NamingConvention.GetForeignKeyConstraintName(productTable, new string[] { "ProductCategoryID" }, productCategoryTable);

            // Assert
            Assert.True(primaryKeyName == "PK_dbo_Product");
            Assert.True(uniqueName == "UQ_dbo_Product_ProductName");
            Assert.True(foreignKeyName == "FK_dbo_Product_ProductCategoryID_dbo_ProductCategory");
        }
    }
}
