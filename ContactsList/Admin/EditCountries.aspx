﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.Master" AutoEventWireup="true" CodeBehind="EditCountries.aspx.cs" Inherits="ContactsList.Admin.EditCountries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/admin.edit.countries.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h3>Редактирование стран:</h3>

        <asp:FormView ID="FormView1"
            InsertMethod="AddCountry"
            ClientIDMode="Static"
            DefaultMode="Insert"
            RenderOuterTable="false"
            ItemType="ContactsList.Admin.Models.AddCountryViewModel"
            runat="server">
            <InsertItemTemplate>
                <asp:TextBox ID="txtCountry" Text="<%# BindItem.Name %>" runat="server" />
                <asp:Button ID="Button1" runat="server" CommandName="Insert" Text="Добавить страну" />
            </InsertItemTemplate>
        </asp:FormView>

        <asp:GridView ID="CountriesGridView"
            ClientIDMode="Static"
            runat="server"
            ItemType="ContactsList.Admin.Models.CountryViewModel"
            CssClass="table table-bordered table-striped table-selectable"
            DataKeyNames="ID"
            AutoGenerateColumns="false"
            AutoGenerateDeleteButton="true"
            AutoGenerateEditButton="true"
            SelectMethod="GetCountries"
            DeleteMethod="RemoveCountry"
            OnRowDataBound="CountriesGridView_RowDataBound"
            UpdateMethod="UpdateCountryName">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Страна" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
