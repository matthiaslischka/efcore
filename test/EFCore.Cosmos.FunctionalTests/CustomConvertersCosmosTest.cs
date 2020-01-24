﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Cosmos.TestUtilities;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Cosmos
{
    public class CustomConvertersCosmosTest : CustomConvertersTestBase<CustomConvertersCosmosTest.CustomConvertersCosmosFixture>
    {
        public CustomConvertersCosmosTest(CustomConvertersCosmosFixture fixture)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
        }

        public override void Can_perform_query_with_max_length()
        {
            // Over the 2Mb document limit
        }

        [ConditionalTheory(Skip = "Issue #16919")]
        public override Task Can_filter_projection_with_inline_enum_variable(bool async)
        {
            return base.Can_filter_projection_with_inline_enum_variable(async);
        }

        [ConditionalTheory(Skip = "Issue #16919")]
        public override Task Can_filter_projection_with_captured_enum_variable(bool async)
        {
            return base.Can_filter_projection_with_captured_enum_variable(async);
        }

        [ConditionalFact(Skip = "Issue #16919")]
        public override void Can_query_using_any_data_type()
        {
            base.Can_query_using_any_data_type();
        }

        [ConditionalFact(Skip = "Issue #16919")]
        public override void Can_query_using_any_data_type_nullable_shadow()
        {
            base.Can_query_using_any_data_type_nullable_shadow();
        }

        [ConditionalFact(Skip = "Issue #16919")]
        public override void Can_query_using_any_data_type_shadow()
        {
            base.Can_query_using_any_data_type_shadow();
        }

        [ConditionalFact(Skip = "Issue #16919")]
        public override void Can_query_using_any_nullable_data_type()
        {
            base.Can_query_using_any_nullable_data_type();
        }

        [ConditionalFact(Skip = "Issue #16919")]
        public override void Can_query_using_any_nullable_data_type_as_literal()
        {
            base.Can_query_using_any_nullable_data_type_as_literal();
        }

        [ConditionalFact(Skip = "Issue #16919")]
        public override void Can_query_with_null_parameters_using_any_nullable_data_type()
        {
            base.Can_query_with_null_parameters_using_any_nullable_data_type();
        }

        [ConditionalFact(Skip = "Issue #16919")]
        public override void Can_insert_and_read_back_with_string_key()
        {
            base.Can_insert_and_read_back_with_string_key();
        }

        [ConditionalFact(Skip = "Issue #16919")]
        public override void Can_query_and_update_with_conversion_for_custom_struct()
        {
            base.Can_query_and_update_with_conversion_for_custom_struct();
        }

        [ConditionalFact(Skip = "Issue #16919")]
        public override void Can_query_and_update_with_conversion_for_custom_type()
        {
            base.Can_query_and_update_with_conversion_for_custom_type();
        }

        [ConditionalFact(Skip = "Issue #16920")]
        public override void Can_query_and_update_with_nullable_converter_on_primary_key()
        {
            base.Can_query_and_update_with_nullable_converter_on_primary_key();
        }

        [ConditionalFact(Skip = "Issue #16920")]
        public override void Can_insert_and_read_back_with_binary_key()
        {
            base.Can_insert_and_read_back_with_binary_key();
        }

        [ConditionalFact(Skip = "Issue #16920")]
        public override void Can_insert_and_read_back_with_case_insensitive_string_key()
        {
            base.Can_insert_and_read_back_with_case_insensitive_string_key();
        }

        [ConditionalFact(Skip = "Issue #16919")]
        public override void Can_insert_and_query_struct_to_string_converter_for_pk()
        {
            base.Can_insert_and_query_struct_to_string_converter_for_pk();
        }

        [ConditionalFact(Skip = "Issue #17670")]
        public override void Can_read_back_mapped_enum_from_collection_first_or_default()
        {
            base.Can_read_back_mapped_enum_from_collection_first_or_default();
        }

        [ConditionalFact(Skip = "Issue #17246")]
        public override void Can_read_back_bool_mapped_as_int_through_navigation()
        {
            base.Can_read_back_bool_mapped_as_int_through_navigation();
        }

        [ConditionalFact(Skip = "Issue #17246")]
        public override void Value_conversion_is_appropriately_used_for_join_condition()
        {
            base.Value_conversion_is_appropriately_used_for_join_condition();
        }

        [ConditionalFact(Skip = "Issue #17246")]
        public override void Value_conversion_is_appropriately_used_for_left_join_condition()
        {
            base.Value_conversion_is_appropriately_used_for_left_join_condition();
        }

        [ConditionalFact]
        public override void Where_bool_gets_converted_to_equality_when_value_conversion_is_used()
        {
            base.Where_bool_gets_converted_to_equality_when_value_conversion_is_used();

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] IN (""Blog"", ""RssBlog"") AND (c[""IsVisible""] = ""Y""))");
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

        public class CustomConvertersCosmosFixture : CustomConvertersFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => CosmosTestStoreFactory.Instance;
            public override bool StrictEquality => true;

            public override int IntegerPrecision => 53;

            public override bool SupportsAnsi => false;

            public override bool SupportsUnicodeToAnsiConversion => false;

            public override bool SupportsLargeStringComparisons => true;

            public override bool SupportsBinaryKeys => true;

            public override bool SupportsDecimalComparisons => true;

            public override DateTime DefaultDateTime => new DateTime();
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                var shadowJObject = (Property)modelBuilder.Entity<BuiltInDataTypesShadow>().Property("__jObject").Metadata;
                shadowJObject.SetConfigurationSource(ConfigurationSource.Convention);
                var nullableShadowJObject = (Property)modelBuilder.Entity<BuiltInNullableDataTypesShadow>().Property("__jObject").Metadata;
                nullableShadowJObject.SetConfigurationSource(ConfigurationSource.Convention);

                modelBuilder.Entity<SimpleCounter>(b => b.ToContainer("SimpleCounters"));
            }
        }
    }
}
