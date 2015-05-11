﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kenmark_Consumer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KenmarkTestDBEntities : DbContext
    {
        public KenmarkTestDBEntities()
            : base("name=KenmarkTestDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<address> addresses { get; set; }
        public DbSet<address_to_collections> address_to_collections { get; set; }
        public DbSet<address_to_country> address_to_country { get; set; }
        public DbSet<admin_values> admin_values { get; set; }
        public DbSet<AdminExceptionDecision> AdminExceptionDecisions { get; set; }
        public DbSet<BBX_Order_Log> BBX_Order_Log { get; set; }
        public DbSet<career> careers { get; set; }
        public DbSet<collection_codes> collection_codes { get; set; }
        public DbSet<Collection_POP> Collection_POP { get; set; }
        public DbSet<collection> collections { get; set; }
        public DbSet<companyProfile> companyProfiles { get; set; }
        public DbSet<content> contents { get; set; }
        public DbSet<country> countries { get; set; }
        public DbSet<customer_coll_buy> customer_coll_buy { get; set; }
        public DbSet<Customer_Inventory> Customer_Inventory { get; set; }
        public DbSet<Customer_Note> Customer_Note { get; set; }
        public DbSet<Customer_Price> Customer_Price { get; set; }
        public DbSet<customer_purchased> customer_purchased { get; set; }
        public DbSet<customer_shiptos> customer_shiptos { get; set; }
        public DbSet<customer> customers { get; set; }
        public DbSet<customersTest> customersTests { get; set; }
        public DbSet<customersTEST2> customersTEST2 { get; set; }
        public DbSet<designer_image_order> designer_image_order { get; set; }
        public DbSet<Drawing> Drawings { get; set; }
        public DbSet<dtproperty> dtproperties { get; set; }
        public DbSet<employee> employees { get; set; }
        public DbSet<@event> events { get; set; }
        public DbSet<Event1> Events1 { get; set; }
        public DbSet<exception_order_items> exception_order_items { get; set; }
        public DbSet<fcdFrameColor> fcdFrameColors { get; set; }
        public DbSet<fcdFrame> fcdFrames { get; set; }
        public DbSet<fcdFrameUPC> fcdFrameUPCs { get; set; }
        public DbSet<Frame_Feedback> Frame_Feedback { get; set; }
        public DbSet<frame_parts> frame_parts { get; set; }
        public DbSet<image_test> image_test { get; set; }
        public DbSet<import_customer_coll_buy> import_customer_coll_buy { get; set; }
        public DbSet<import_customers> import_customers { get; set; }
        public DbSet<import_frame_parts> import_frame_parts { get; set; }
        public DbSet<import_inv> import_inv { get; set; }
        public DbSet<import_inv_admin> import_inv_admin { get; set; }
        public DbSet<import_Inventory_Admin_Info> import_Inventory_Admin_Info { get; set; }
        public DbSet<import_inventoryBestSellers> import_inventoryBestSellers { get; set; }
        public DbSet<import_inventorySale> import_inventorySale { get; set; }
        public DbSet<import_invstock> import_invstock { get; set; }
        public DbSet<import_ob_inv> import_ob_inv { get; set; }
        public DbSet<import_ob_qty_breaks> import_ob_qty_breaks { get; set; }
        public DbSet<import_Prospect_Customers> import_Prospect_Customers { get; set; }
        public DbSet<import_shipto> import_shipto { get; set; }
        public DbSet<import_ShipVia_Allowed> import_ShipVia_Allowed { get; set; }
        public DbSet<import_stmt_aging> import_stmt_aging { get; set; }
        public DbSet<import_stmts> import_stmts { get; set; }
        public DbSet<import_VSB_inv> import_VSB_inv { get; set; }
        public DbSet<inventory> inventories { get; set; }
        public DbSet<inventory_admin> inventory_admin { get; set; }
        public DbSet<Inventory_Admin_Info> Inventory_Admin_Info { get; set; }
        public DbSet<inventory_info> inventory_info { get; set; }
        public DbSet<inventory_temp> inventory_temp { get; set; }
        public DbSet<inventoryBestSeller> inventoryBestSellers { get; set; }
        public DbSet<inventoryBestSellersImport> inventoryBestSellersImports { get; set; }
        public DbSet<inventorySale> inventorySales { get; set; }
        public DbSet<Kenmark_Collections> Kenmark_Collections { get; set; }
        public DbSet<Kenmark_Collections_like> Kenmark_Collections_like { get; set; }
        public DbSet<Kenmark_Collections_Sub> Kenmark_Collections_Sub { get; set; }
        public DbSet<login> logins { get; set; }
        public DbSet<lookup_type> lookup_type { get; set; }
        public DbSet<Mobile_Feedback> Mobile_Feedback { get; set; }
        public DbSet<new_registrations> new_registrations { get; set; }
        public DbSet<ob_inv> ob_inv { get; set; }
        public DbSet<ob_qty_breaks> ob_qty_breaks { get; set; }
        public DbSet<order_cases> order_cases { get; set; }
        public DbSet<order_items> order_items { get; set; }
        public DbSet<order_items_deleted> order_items_deleted { get; set; }
        public DbSet<order_note> order_note { get; set; }
        public DbSet<order_number> order_number { get; set; }
        public DbSet<order_POP> order_POP { get; set; }
        public DbSet<order_POP_Combination> order_POP_Combination { get; set; }
        public DbSet<order_status> order_status { get; set; }
        public DbSet<order> orders { get; set; }
        public DbSet<outofstock_skus> outofstock_skus { get; set; }
        public DbSet<payment_logs> payment_logs { get; set; }
        public DbSet<payment_options> payment_options { get; set; }
        public DbSet<payment> payments { get; set; }
        public DbSet<press_releases> press_releases { get; set; }
        public DbSet<PressReleasePreview> PressReleasePreviews { get; set; }
        public DbSet<Promo_Codes> Promo_Codes { get; set; }
        public DbSet<Prospect_Customers> Prospect_Customers { get; set; }
        public DbSet<Prospect_CustomersBAK> Prospect_CustomersBAK { get; set; }
        public DbSet<Publicity> Publicities { get; set; }
        public DbSet<publicity_items> publicity_items { get; set; }
        public DbSet<regional_manager> regional_manager { get; set; }
        public DbSet<Rep_Map_Remove> Rep_Map_Remove { get; set; }
        public DbSet<role> roles { get; set; }
        public DbSet<SaleFrame> SaleFrames { get; set; }
        public DbSet<secret_question> secret_question { get; set; }
        public DbSet<security> securities { get; set; }
        public DbSet<securityBAK> securityBAKs { get; set; }
        public DbSet<Shipping_Methods> Shipping_Methods { get; set; }
        public DbSet<ShipVia_Allowed> ShipVia_Allowed { get; set; }
        public DbSet<Show_Codes> Show_Codes { get; set; }
        public DbSet<Show_ScansVEW2013> Show_ScansVEW2013 { get; set; }
        public DbSet<staff> staffs { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<statement_register_log> statement_register_log { get; set; }
        public DbSet<StatementCheck> StatementChecks { get; set; }
        public DbSet<StatementOpenItem> StatementOpenItems { get; set; }
        public DbSet<status> status { get; set; }
        public DbSet<stmt_aging> stmt_aging { get; set; }
        public DbSet<stmt_details90> stmt_details90 { get; set; }
        public DbSet<survey> surveys { get; set; }
        public DbSet<team> teams { get; set; }
        public DbSet<Temporary_Customers> Temporary_Customers { get; set; }
        public DbSet<Temporary_ShipTo> Temporary_ShipTo { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<TextAlert_Numbers> TextAlert_Numbers { get; set; }
        public DbSet<Used_Promo_Codes> Used_Promo_Codes { get; set; }
        public DbSet<User_Tracking> User_Tracking { get; set; }
        public DbSet<virtualbag_user_values> virtualbag_user_values { get; set; }
        public DbSet<VSB_inventory> VSB_inventory { get; set; }
        public DbSet<Zip_Map_Location> Zip_Map_Location { get; set; }
        public DbSet<zz_fcdFrameColor> zz_fcdFrameColor { get; set; }
        public DbSet<zz_fcdFrameLensColor> zz_fcdFrameLensColor { get; set; }
        public DbSet<zz_fcdFrames> zz_fcdFrames { get; set; }
        public DbSet<zz_fcdFrameSize> zz_fcdFrameSize { get; set; }
        public DbSet<zz_fcdFrameUPC> zz_fcdFrameUPC { get; set; }
    }
}
